using AutoMapper;
using Restaurant.Application.Features.Commerce.Carts.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.UpdateItemQuantity;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Domain.Repositories.Guest;
using System.Net;

namespace Restaurant.Infrastructure.Services.Commerce
{
    internal class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<CartResponse>> GetAsync(
            GetCartQuery query,
            GetCartSpecification specification,
            CancellationToken cancellationToken = default)
        {
            if (query.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedCartAsync(
                    query.UserId.Value, query.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                    return Result<CartResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);
            }
            else
            {
                await ResolveGuestCartAsync(query.SessionId!, cancellationToken);
            }

            var processedCart = await _cartRepository.FindAsync(specification, cancellationToken);
            if (processedCart is null)
                return Result<CartResponse>.Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<CartResponse>(processedCart);
            return Result<CartResponse>
                .Succeed(response, Success<Cart>.Retrieved);
        }

        public async Task<Result<CartResponse>> AddItemAsync(
            AddCartItemCommand command,
            AddCartItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Cart cart;

            if (command.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedCartAsync(
                    command.UserId.Value, command.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                    return Result<CartResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);

                cart = resolveResult.Data!;
            }
            else
            {
                cart = await ResolveGuestCartAsync(command.SessionId!, cancellationToken);
            }

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem is null)
            {
                cartItem = new CartItem(cart.Id, product.Id);
                _cartItemRepository.Add(cartItem);
            }
            else
            {
                cartItem.UpdateQuantity(cartItem.Quantity + 1);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var processedCart = await _cartRepository.FindAsync(specification, cancellationToken);
            if (processedCart is null)
                return Result<CartResponse>.Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<CartResponse>(processedCart);
            return Result<CartResponse>
                .Succeed(response, Success<CartItem>.Added);
        }

        public async Task<Result<CartResponse>> RemoveItemAsync(
            RemoveCartItemCommand command,
            RemoveCartItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Cart cart;

            if (command.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedCartAsync(
                    command.UserId.Value, command.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                    return Result<CartResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);

                cart = resolveResult.Data!;
            }
            else
            {
                cart = await ResolveGuestCartAsync(command.SessionId!, cancellationToken);
            }

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem is null)
            {
                return Result<CartResponse>
                    .Fail("This product has not been added to your cart.", HttpStatusCode.NotFound);
            }

            _cartItemRepository.Remove(cartItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var processedCart = await _cartRepository.FindAsync(specification, cancellationToken);
            if (processedCart is null)
                return Result<CartResponse>.Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<CartResponse>(processedCart);
            return Result<CartResponse>
                .Succeed(response, "Cart Item removed successfully.");
        }

        private async Task<Result<Cart>> ResolveAuthenticatedCartAsync(
            Guid userId,
            string? sessionId,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByUserIdAsync(userId, cancellationToken);
            if (customer is null)
            {
                return Result<Cart>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var guestCart = sessionId != null
                ? await _cartRepository.FindBySessionIdAsync(sessionId, cancellationToken)
                : null;

            var customerCart = await _cartRepository.FindByCustomerIdAsync(customer.Id, cancellationToken);

            Cart cart;

            if (customerCart is null)
            {
                cart = new Cart(customer.Id);
                _cartRepository.Add(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (guestCart is not null)
                {
                    cart.Merge(guestCart);
                    _cartRepository.Remove(guestCart);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                cart = customerCart;

                if (guestCart is not null)
                {
                    cart.Merge(guestCart);
                    _cartRepository.Remove(guestCart);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            return Result<Cart>.Succeed(cart, Success<Cart>.Retrieved);
        }
        private async Task<Cart> ResolveGuestCartAsync(
            string sessionId,
            CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.FindBySessionIdAsync(sessionId, cancellationToken);
            if (cart is null)
            {
                cart = new Cart(sessionId);
                _cartRepository.Add(cart);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return cart;
        }

        public async Task<Result<CartResponse>> UpdateItemAsync(
            UpdateCartItemQuantityCommand command,
            UpdateCartItemQuantitySpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Cart cart;

            if (command.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedCartAsync(
                    command.UserId.Value, command.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                {
                    return Result<CartResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);
                }

                cart = resolveResult.Data!;
            }
            else
            {
                cart = await ResolveGuestCartAsync(command.SessionId!, cancellationToken);
            }

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem is null)
            {
                cartItem = new CartItem(cart.Id, product.Id);
                _cartItemRepository.Add(cartItem);
            }
            else
            {
                cartItem.UpdateQuantity(command.Body.Quantity);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var processedCart = await _cartRepository.FindAsync(specification, cancellationToken);
            if (processedCart is null)
            {
                return Result<CartResponse>
                    .Fail(Error<Cart>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<CartResponse>(processedCart);
            return Result<CartResponse>
                .Succeed(response, "Cart Item quantity updated successfully.");
        }
    }
}

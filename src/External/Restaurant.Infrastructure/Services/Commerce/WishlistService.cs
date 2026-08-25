using AutoMapper;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetByCustomerId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetBySessionId;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Commerce;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Identity;
using System.Net;

namespace Restaurant.Infrastructure.Services.Commerce
{
    internal class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WishlistService(
            IWishlistRepository wishlistRepository,
            IWishlistItemRepository wishlistItemRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<WishlistResponse>> GetByCustomerIdAsync(
            GetWishlistByCustomerIdQuery query,
            GetWishlistByCustomerIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.FindByIdAsync(query.CustomerId, cancellationToken);
            if (customer is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var wishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (wishlist is null)
            {
                wishlist = new Wishlist(customer.Id);
                _wishlistRepository.Add(wishlist);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var response = _mapper.Map<WishlistResponse>(wishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }

        public async Task<Result<WishlistResponse>> GetBySessionIdAsync(
            GetWishlistBySessionIdQuery query,
            GetWishlistBySessionIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var wishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (wishlist is null)
            {
                wishlist = new Wishlist(query.SessionId);
                _wishlistRepository.Add(wishlist);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var response = _mapper.Map<WishlistResponse>(wishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }

        public async Task<Result<WishlistResponse>> GetAsync(
            GetWishlistQuery query,
            GetWishlistSpecification specification,
            CancellationToken cancellationToken = default)
        {
            if (query.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedWishlistAsync(
                    query.UserId.Value, query.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                {
                    return Result<WishlistResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);
                }
            }
            else
            {
                await ResolveGuestWishlistAsync(query.SessionId, cancellationToken);
            }

            var processedWishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (processedWishlist is null)
                return Result<WishlistResponse>.Fail(Error<Wishlist>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<WishlistResponse>(processedWishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<Wishlist>.Retrieved);
        }

        public async Task<Result<WishlistResponse>> AddItemAsync(
            AddWishlistItemCommand command,
            AddWishlistItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Wishlist wishlist;

            if (command.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedWishlistAsync(
                    command.UserId.Value, command.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                {
                    return Result<WishlistResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);
                }

                wishlist = resolveResult.Data!;
            }
            else
            {
                wishlist = await ResolveGuestWishlistAsync(command.SessionId, cancellationToken);
            }

            if (!wishlist.WishlistItems.Any(x => x.ProductId == product.Id))
            {
                var wishlistItem = new WishlistItem(wishlist.Id, product.Id);
                _wishlistItemRepository.Add(wishlistItem);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            var processedWishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (processedWishlist is null)
                return Result<WishlistResponse>.Fail(Error<Wishlist>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<WishlistResponse>(processedWishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<WishlistItem>.Added);
        }

        public async Task<Result<WishlistResponse>> RemoveItemAsync(
            RemoveWishlistItemCommand command,
            RemoveWishlistItemSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.FindByIdAsync(command.Body.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<WishlistResponse>
                    .Fail(Error<Product>.NotFound, HttpStatusCode.NotFound);
            }

            Wishlist wishlist;

            if (command.UserId != null)
            {
                var resolveResult = await ResolveAuthenticatedWishlistAsync(
                    command.UserId.Value, command.SessionId, cancellationToken);

                if (!resolveResult.IsSucceed)
                    return Result<WishlistResponse>
                        .Fail(resolveResult.Message, (HttpStatusCode)resolveResult.StatusCode);

                wishlist = resolveResult.Data!;
            }
            else
            {
                wishlist = await ResolveGuestWishlistAsync(command.SessionId, cancellationToken);
            }

            var wishlistItem = wishlist.WishlistItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (wishlistItem is null)
            {
                return Result<WishlistResponse>
                    .Fail("This product has not been added to your wishlist.", HttpStatusCode.NotFound);
            }

            _wishlistItemRepository.Remove(wishlistItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var processedWishlist = await _wishlistRepository.FindAsync(specification, cancellationToken);
            if (processedWishlist is null)
                return Result<WishlistResponse>.Fail(Error<Wishlist>.NotFound, HttpStatusCode.NotFound);

            var response = _mapper.Map<WishlistResponse>(processedWishlist);
            return Result<WishlistResponse>
                .Succeed(response, Success<WishlistItem>.Deleted);
        }

        private async Task<Result<Wishlist>> ResolveAuthenticatedWishlistAsync(
            Guid userId,
            string sessionId,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByUserIdAsync(userId, cancellationToken);
            if (customer is null)
            {
                return Result<Wishlist>
                    .Fail(Error<Customer>.NotFound, HttpStatusCode.NotFound);
            }

            var guestWishlist = await _wishlistRepository
                .FindBySessionIdAsync(sessionId, cancellationToken);
            var customerWishlist = await _wishlistRepository
                .FindByCustomerIdAsync(customer.Id, cancellationToken);

            Wishlist wishlist;

            if (customerWishlist is null)
            {
                wishlist = new Wishlist(customer.Id);
                _wishlistRepository.Add(wishlist);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (guestWishlist is not null)
                {
                    wishlist.Merge(guestWishlist);
                    _wishlistRepository.Remove(guestWishlist);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                wishlist = customerWishlist;

                if (guestWishlist is not null)
                {
                    wishlist.Merge(guestWishlist);
                    _wishlistRepository.Remove(guestWishlist);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            return Result<Wishlist>.Succeed(wishlist, Success<Wishlist>.Retrieved);
        }

        private async Task<Wishlist> ResolveGuestWishlistAsync(
            string sessionId,
            CancellationToken cancellationToken)
        {
            var wishlist = await _wishlistRepository.FindBySessionIdAsync(sessionId, cancellationToken);
            if (wishlist is null)
            {
                wishlist = new Wishlist(sessionId);
                _wishlistRepository.Add(wishlist);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return wishlist;
        }
    }
}

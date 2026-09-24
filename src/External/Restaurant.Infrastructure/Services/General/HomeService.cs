using AutoMapper;
using Restaurant.Application.Features.General.Homes.Queries.Get;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.General;
using Restaurant.Contract.DTOs.Catalog.Brands;
using Restaurant.Contract.DTOs.Catalog.ProductCategories;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.Commerce.Carts;
using Restaurant.Contract.DTOs.Commerce.Wishlists;
using Restaurant.Contract.DTOs.General;
using Restaurant.Contract.DTOs.Territory.Branches;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Commerce;
using Restaurant.Domain.Repositories.Guest;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Infrastructure.Repositories.Commerce;
namespace Restaurant.Infrastructure.Services.General
{
    internal class HomeService : IHomeService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HomeService(
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IBranchRepository branchRepository,
            IMapper mapper,
            IBrandRepository brandRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IWishlistRepository wishlistRepository,
            IWishlistItemRepository wishlistItemRepository,
            IUnitOfWork unitOfWork,
            ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<Result<HomeResponse>> GetAsync(
            GetHomeQuery query,
            CancellationToken cancellationToken = default)
        {
            var popularProducts = await _productRepository.ToListPopularProductsAsync(query.ProductLimit, cancellationToken);
            var productCategories = await _productCategoryRepository.ToListWithImagesAsync(cancellationToken);
            var brands = await _brandRepository.ToListWithImagesAsync(cancellationToken);
            var branches = await _branchRepository.ToListAsync(cancellationToken);

            var cart = await ResolveCartAsync(query.UserId, query.SessionId, cancellationToken);
            var wishlist = await ResolveWishlistAsync(query.UserId, query.SessionId, cancellationToken);

            var cartItemCount = cart is not null
                ? await _cartItemRepository.CountAsync(cart.Id, cancellationToken)
                : 0;

            var wishlistItemCount = wishlist is not null
                ? await _wishlistItemRepository.CountAsync(wishlist.Id, cancellationToken)
                : 0;

            var response = new HomeResponse()
            {
                PopularProducts = _mapper.Map<IEnumerable<PopularProductResponse>>(popularProducts),
                ProductCategories = _mapper.Map<IEnumerable<ProductCategoryResponse>>(productCategories),
                Brands = _mapper.Map<IEnumerable<BrandResponse>>(brands),
                Branches = _mapper.Map<IEnumerable<BranchResponse>>(branches),
                WishlistItemCount = wishlistItemCount,
                CartItemCount = cartItemCount,
            };

            return Result<HomeResponse>
                .Succeed(response, Success.Retrieved("Home Data"));
        }

        private async Task<Cart?> ResolveCartAsync(
            Guid? userId,
            string? sessionId,
            CancellationToken cancellationToken)
        {
            if (userId.HasValue)
            {
                var customer = await _customerRepository.FindByUserIdAsync(userId.Value, cancellationToken);
                if (customer is null)
                {
                    return null;
                }

                var guestCart = !string.IsNullOrWhiteSpace(sessionId)
                    ? await _cartRepository.FindBySessionIdAsync(sessionId, cancellationToken)
                    : null;

                var customerCart = await _cartRepository.FindByCustomerIdAsync(customer.Id, cancellationToken);

                if (guestCart is not null)
                {
                    if (customerCart is null)
                    {
                        customerCart = new Cart(customer.Id);
                        _cartRepository.Add(customerCart);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }

                    customerCart.Merge(guestCart);
                    _cartRepository.Remove(guestCart);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return customerCart;
            }

            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                return await _cartRepository.FindBySessionIdAsync(sessionId, cancellationToken);
            }

            return null;
        }

        private async Task<Wishlist?> ResolveWishlistAsync(
            Guid? userId,
            string? sessionId,
            CancellationToken cancellationToken)
        {
            if (userId.HasValue)
            {
                var customer = await _customerRepository.FindByUserIdAsync(userId.Value, cancellationToken);
                if (customer is null)
                {
                    return null;
                }

                var guestWishlist = !string.IsNullOrWhiteSpace(sessionId)
                    ? await _wishlistRepository.FindBySessionIdAsync(sessionId, cancellationToken)
                    : null;

                var customerWishlist = await _wishlistRepository.FindByCustomerIdAsync(customer.Id, cancellationToken);

                if (guestWishlist is not null)
                {
                    if (customerWishlist is null)
                    {
                        customerWishlist = new Wishlist(customer.Id);
                        _wishlistRepository.Add(customerWishlist);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }

                    customerWishlist.Merge(guestWishlist);
                    _wishlistRepository.Remove(guestWishlist);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return customerWishlist;
            }

            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                return await _wishlistRepository.FindBySessionIdAsync(sessionId, cancellationToken);
            }

            return null;
        }
    }
}

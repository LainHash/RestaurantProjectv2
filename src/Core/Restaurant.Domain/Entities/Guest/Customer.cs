using NanoidDotNet;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Guest
{
    public partial class Customer : SoftDeletableEntity
    {
        public string CustomerCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",20);

        public int UserId { get; private set; }

        public int? AvatarImageId { get; private set; }

        public Image? AvatarImage { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public Wallet? Wallet { get; private set; }
        public Wishlist? Wishlist { get; private set; } = null!;
        public Cart? Cart { get; private set; } = null!;
        public ICollection<DiscountCustomer> DiscountCustomers { get; private set; } = [];
    }

    public partial class Customer
    {
        public Customer() { }

        public Customer(int userId)
        {
            UserId = userId;
        }

        public Customer SetUser(int userId)
        {
            UserId = userId;
            return this;
        }

        public Customer SetAvatar(int imageId)
        {
            AvatarImageId = imageId;
            return this;
        }
        public Customer ClearAvatar()
        {
            AvatarImageId = null;
            return this;
        }
    }
}

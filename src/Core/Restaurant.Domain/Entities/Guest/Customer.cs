using NanoidDotNet;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Entities.Schedule;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Guest
{
    public partial class Customer : SoftDeletableEntity
    {
        public string CustomerCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",20);

        public long UserId { get; private set; }

        public long? AvatarImageId { get; private set; }

        public Image? AvatarImage { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public Wallet? Wallet { get; private set; }
        public Wishlist? Wishlist { get; private set; } = null!;
        public Cart? Cart { get; private set; } = null!;
        public ICollection<DiscountCustomer> DiscountCustomers { get; private set; } = [];
        public ICollection<Order> Orders { get; private set; } = [];
        public ICollection<Reservation> Reservations { get; private set; } = [];
    }

    public partial class Customer
    {
        public Customer() { }

        public Customer(long userId)
        {
            UserId = userId;
        }

        public Customer SetUser(long userId)
        {
            UserId = userId;
            return this;
        }

        public Customer SetAvatar(long imageId)
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

namespace Restaurant.Contract.DTOs.Guest.Customers
{
    public class CustomerResponse
    {   
        public Guid Id { get; set; }
        public string CustomerCode { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }
    }
}

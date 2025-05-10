namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Reference { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }
    }
}

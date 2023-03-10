using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }
    }
}

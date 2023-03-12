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

    /// <summary>
    /// A single product that has been added to the users
    /// shopping cart cookie
    /// </summary>
    public class CartProductViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
    }
}

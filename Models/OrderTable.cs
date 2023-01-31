using MVCCoreApplication.Constants;

namespace MVCCoreApplication.Models
{
    public class OrderTable
    {
        public int Id { get; set; }
        // [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //[Required]
        public string ProductDescription { get; set; }
        //[Required]
        public string ImageFile { get; set; }

        public ProductCategory category { get; set; }
        //[Required]
        public int NumberOfProduct { get; set; }
        // [Required]
        public float ProductPrice { get; set; }
        public int UserId { get; set; }

        public int AddressId { get; set; }
    }
}

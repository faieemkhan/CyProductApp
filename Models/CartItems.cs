using System.ComponentModel.DataAnnotations;
using MVCCoreApplication.Constants;
namespace MVCCoreApplication.Models
{
    public class CartItems
    {
       
        public int Id { get; set; }
        public int ProductId { get; set; }
        // [Required]
        public string ProductName { get; set; }
        //[Required]
        public string ProductDescription { get; set; }
        //[Required]
        public string ImageFile { get; set; }

        public  ProductCategory category { get;set;}

       
        //[Required]
        public int NumberOfProduct { get; set; }
        // [Required]
        public float ProductPrice { get; set; }

        public int UserId { get; set; }


        public int Quanitity { get; set; } = 1;
        public bool IsOrdered { get; set; }=false;

        
    }
}

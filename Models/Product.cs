using MVCCoreApplication.Constants;
using System.ComponentModel.DataAnnotations;

namespace MVCCoreApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
       // [Required]
        public string ProductName { get; set; }
        //[Required]
        public string ProductDescription { get; set; }
        //[Required]
        public string ImageFile { get; set; }

        public  ProductCategory  category{ get; set; }
        //[Required]
        public int  NumberOfProduct { get; set; }
       // [Required]
        public float ProductPrice { get; set; }
       
        
    }
}

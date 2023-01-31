using System.ComponentModel.DataAnnotations;

namespace MVCCoreApplication.Models
{
    public class UserAddress
    {
        [Key]
        public int Id { get;set; }
        public string FullName { get; set; }
        [Required]
       
        public string Email { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]

        public string PaymentMode { get; set; }

        public int UserId { get; set; }
       

    }
}

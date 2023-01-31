using System.ComponentModel.DataAnnotations;

namespace MVCCoreApplication.Models
{
    public class AdminLoginCredentials
    {
        [Key]
      public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
       
    }
}

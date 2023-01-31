using System.ComponentModel.DataAnnotations;

namespace MVCCoreApplication.Models
{
    public class UserLoginCredential
    {
        [Key]
        
        public string Name { get; set;}
        public string Password { get; set; }
       
        
       

    }
}

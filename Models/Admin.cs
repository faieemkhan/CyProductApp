using System.ComponentModel.DataAnnotations;

namespace MVCCoreApplication.Models
{
    public class Admin
    {
        //Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
          public string Location { get; set; }

    }
}

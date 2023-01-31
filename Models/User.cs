using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCoreApplication.Models
{
    //Domain Entities in user
    public class User
    {
        #region property
        // Attribute or Decorator
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        
        [Required(ErrorMessage ="Name field should be morethan 3  characters")]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
       
        [DataType(DataType.Password)]
        //[RegularExpression("[A-z][a-z]{2,}[@][0-9]$", ErrorMessage = "Invalid Password ")]
        public string Password { get; set; } 
         [Required]
        public string Location { get; set; }
        public bool IsRegular { get; set; } = true;

        public bool IsBlocked { get; set; } = false;
      

        #endregion
    }
}

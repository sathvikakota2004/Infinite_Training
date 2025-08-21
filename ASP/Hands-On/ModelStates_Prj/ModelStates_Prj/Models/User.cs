using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ModelStates_Prj.Models
{
    public class User
    {
        [Required(ErrorMessage = "FirstName Required")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "First Name Cannot be more than 20 characters")]
        public string Fname { get; set; }

        [DisplayName("Last Name")]
        public string Lname { get; set; }

        [Display(Name = "Users Age")]
        [Range(21, 45, ErrorMessage = "Age has to be between 21 and 45 only")]
        public int age { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$",
            ErrorMessage = "Invalid Format")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 100 characters")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Display(Name = "Surame")]
        [Required(ErrorMessage = "Surame is required")]
        public string SurName { get; set; }
        [Display(Name = "Middlename")]
        public string MiddleName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, ErrorMessage = "Passowrd should be in the range 3-15 characters", MinimumLength = 3)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, ErrorMessage = "Passowrd should be in the range 3-15 characters", MinimumLength = 3)]
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Do not Match")]
        public string ConfirmPassword { get; set; }
    }
}

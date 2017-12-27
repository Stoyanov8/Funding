using Funding.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Funding.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = UserConst.EmailDisplay)]
        public string Email { get; set; }

        [Required]
        [StringLength(UserConst.PasswordMaxLength, ErrorMessage = UserConst.Password, MinimumLength = UserConst.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = UserConst.PasswordDisplay)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = UserConst.ConfirmPasswordDisplay)]
        [Compare(UserConst.PasswordDisplay, ErrorMessage = UserConst.ConfirmPassword)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = UserConst.FirstNameDisplay)]
        [StringLength(UserConst.UserMaxLenght, ErrorMessage = UserConst.FirstName, MinimumLength = UserConst.UserMinLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = UserConst.LastNameDisplay)]
        [StringLength(UserConst.UserMaxLenght, ErrorMessage = UserConst.LastName, MinimumLength = UserConst.UserMinLength)]
        public string LastName { get; set; }

        [Required]
        [Range(UserConst.MinAge, UserConst.MaxAge, ErrorMessage = UserConst.Age)]
        public int Age { get; set; }
    }
}
using Funding.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Funding.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

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
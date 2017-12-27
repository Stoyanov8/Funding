namespace Funding.Services.Models.AdminViewModels
{
    using Funding.Common.Constants;
    using Funding.Data.Models;
    using Funding.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class UserEditViewModel : IMapFrom<User>
    {
        public string userId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = UserConst.EmailDisplay)]
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

        [Required]
        [Display(Name = UserConst.UserNameDisplay)]
        [StringLength(UserConst.UserMaxLenght, ErrorMessage = UserConst.UserName, MinimumLength = UserConst.UserMinLength)]
        public string UserName { get; set; }
    }
}
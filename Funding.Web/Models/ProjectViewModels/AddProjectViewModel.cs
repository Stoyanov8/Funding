using Funding.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Funding.Web.Models.ProjectViewModels
{
    public class AddProjectViewModel
    {
        [Required(ErrorMessage = ProjectConst.ProjectNameRequired)]
        [StringLength(ProjectConst.NameМaxLength, MinimumLength = ProjectConst.NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ProjectConst.ProjectDescriptionRequired)]
        [StringLength(ProjectConst.DescriptionMaxLenght, MinimumLength = ProjectConst.DescriptionMinLenght)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = ProjectConst.ProjectImageUrlRequired)]
        [Display(Name = ProjectConst.ImageUrlDisplay)]
        [MinLength(ProjectConst.ImageUrlMinLenght)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = ProjectConst.GoalRequired)]
        [Range(ProjectConst.MinimumGoal, double.MaxValue, ErrorMessage = ProjectConst.GoalNegativeMessage)]
        public decimal? Goal { get; set; }

        [Required(ErrorMessage =ProjectConst.StartDateRequired)]
        [Display(Name = ProjectConst.StartDateDisplay)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = ProjectConst.EndDateRequired)]
        [Display(Name = ProjectConst.EndDateDisplay)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = ProjectConst.TagRequired)]
        public string Tags { get; set; }
    }
}

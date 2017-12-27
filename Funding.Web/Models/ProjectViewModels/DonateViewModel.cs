using Funding.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Funding.Web.Models.ProjectViewModels
{

    public class DonateViewModel
    {
        [Required(ErrorMessage = ProjectConst.DonateMessageRequired)]
        public string Message { get; set; }

        [Range(ProjectConst.MinimumAmountToDonate, double.MaxValue, ErrorMessage = ProjectConst.DonateAmountRequired)]
        public decimal Amount { get; set; }

        public int ProjectId { get; set; }
    }
}

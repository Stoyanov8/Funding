namespace Funding.Web.Models.InboxViewModels
{
    using Funding.Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class SendMessageViewModel
    {
        [Required(ErrorMessage = InboxConst.TitleRequired)]
        [StringLength(InboxConst.TitleMaxLength, MinimumLength = InboxConst.TitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = InboxConst.ContentRequired)]
        [StringLength(InboxConst.ContentMaxLength, MinimumLength = InboxConst.ContentMinLength)]
        public string Content { get; set; }

        [Required(ErrorMessage = InboxConst.ReceiverNameRequired)]
        [Display(Name = InboxConst.ReceiverDisplay)]
        [StringLength(InboxConst.ReceiverNameMaxLength, MinimumLength = InboxConst.ReceiverNameMinLength)]
        public string ReceiverName { get; set; }
    }
}
namespace Funding.Services.Models.ProjectViewModels
{
    using Funding.Data.Models;
    using Funding.Services.Mapping;
    using System;
    using AutoMapper;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string User { get; set; }       

        public DateTime SentDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.User, cfg => cfg.MapFrom(a => a.User.Email));
        }
    }
}

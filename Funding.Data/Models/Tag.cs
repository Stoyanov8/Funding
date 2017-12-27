namespace Funding.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Funding.Common.Constants;
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TagConst.MaxLenght, MinimumLength = TagConst.MinLength)]
        public string Name { get; set; }

        public IList<ProjectsTags> Projects { get; set; } = new List<ProjectsTags>();
    }
}
using Funding.Common.Constants;
using System.Collections.Generic;

namespace Funding.Services.Models.ProjectViewModels
{
    public class ProjectsSearchViewModel
    {
        private string previous;
        private string next;

        public ProjectsSearchViewModel()
        {
            string previous = Page == 1 ? ProjectConst.Disabled : string.Empty;
            string next = Page == NumberOfPages ? ProjectConst.Disabled : string.Empty;

            this.previous = previous;
            this.next = next;
        }

        public List<ProjectsHome> Projects { get; set; }

        public int Page { get; set; }

        public int NumberOfPages { get; set; }

        public string Previous
        {
            get
            {
                return this.previous;
            }
            set
            {
                this.previous = value;
            }
        }

        public string Next
        {
            get
            {
                return this.next;
            }
            set
            {
                this.next = value;
            }
        }
    }
}

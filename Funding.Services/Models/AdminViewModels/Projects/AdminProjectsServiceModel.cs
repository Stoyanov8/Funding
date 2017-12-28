namespace Funding.Services.Models.AdminViewModels.Projects
{
    using Funding.Common.Constants;
    using System.Collections.Generic;

    public class AdminProjectsServiceModel
    {
        private string previous;
        private string next;

        public AdminProjectsServiceModel()
        {
            string previous = Page == 1 ? ProjectConst.Disabled : string.Empty;
            string next = Page == NumberOfPages ? ProjectConst.Disabled : string.Empty;

            this.previous = previous;
            this.next = next;
        }

        public List<ProjectsListingViewModel> Projects { get; set; }

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
using Funding.Data.Models;
using Funding.Services.Mapping;

namespace Funding.Services.Models.AdminViewModels
{
    public class UserListingViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public bool isDeleted { get; set; }
    }
}
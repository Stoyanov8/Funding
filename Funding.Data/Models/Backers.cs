namespace Funding.Data.Models
{
    public class Backers
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
namespace Funding.Data
{
    using Funding.Data.Builder;
    using Funding.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FundingDbContext : IdentityDbContext<User>
    {
        public FundingDbContext(DbContextOptions<FundingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProjectsTags> ProjectTags { get; set; }

        public DbSet<Backers> Backers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UserBuilder();

            builder.ProjectBuilder();

            builder.MappingTablesBuilder();

            base.OnModelCreating(builder);
        }
    }
}
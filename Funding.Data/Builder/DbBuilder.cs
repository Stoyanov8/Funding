using Funding.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Funding.Data.Builder
{
    public static class DbBuilder
    {
        public static void UserBuilder(this ModelBuilder builder)
        {
            builder.Entity<User>()
           .HasIndex(u => u.Email)
           .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.MessagesReceived)
                .WithOne(m => m.Sender)
                .HasForeignKey(u => u.ReceiverId);

            builder.Entity<User>()
                .HasMany(u => u.MessagesSent)
                .WithOne(m => m.Receiver)
                .HasForeignKey(u => u.SenderId);

            builder.Entity<User>()
                .HasMany(p => p.ProjectsCreated)
                .WithOne(x => x.Creator)
                .HasForeignKey(a => a.CreatorId);

            builder.Entity<User>()
                 .HasMany(x => x.ProjectsFunded)
                 .WithOne(x => x.User)
                 .HasForeignKey(x => x.UserId);

            builder.Entity<User>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
        public static void ProjectBuilder(this ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasMany(x => x.Backers)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);

            builder.Entity<Project>()
               .HasMany(x => x.Tags)
               .WithOne(x => x.Project)
               .HasForeignKey(x => x.ProjectId);

            builder.Entity<Project>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);

            builder.Entity<Project>()
            .HasMany(x => x.Tags)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId);

        }
        public static void MappingTablesBuilder(this ModelBuilder builder)
        {
            builder.Entity<Backers>()
                .HasKey(x => new { x.ProjectId, x.UserId });

            builder.Entity<ProjectsTags>()
                  .HasKey(x => new { x.ProjectId, x.TagId });
        }
    }
}

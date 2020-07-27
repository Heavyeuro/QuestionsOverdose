using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuestionOverdose.Domain.Entities;

namespace QuestionOverdose.DAL.EF
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Role> Badges { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<QuestionTag> QuestionTags { get; set; }

        public DbSet<UserTag> UserTags { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<UserQuestion> UserQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<QuestionTag>().HasKey(qt => new { qt.TagId, qt.QuestionId });
            builder.Entity<UserTag>().HasKey(tu => new { tu.TagId, tu.UserId });
            builder.Entity<Answer>().HasOne(x => x.Question).WithMany(y => y.Answers)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserAnswer>().HasOne(x => x.Answer).WithMany(y => y.Voters)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserQuestion>().HasOne(x => x.Question).WithMany(y => y.Voters)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Comment>().HasOne(x => x.Answer).WithMany(y => y.Comments)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserAnswer>().HasKey(qt => new { qt.UserId, qt.AnswerId });
            builder.Entity<UserQuestion>().HasKey(qt => new { qt.UserId, qt.QuestionId });
            builder.Entity<Question>().Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Entity<Answer>().Property(p => p.IsDeleted).HasDefaultValue(false);

            base.OnModelCreating(builder);

            var roles = new List<Role>()
            {
                new Role { RoleName = "User", Id = 1 },
                new Role { RoleName = "Admin", Id = 2 },
                new Role { RoleName = "Redactor", Id = 3 }
            };

            var users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    Password = "Admin",
                    Username = "Admin",
                    UserRoleId = roles[1].Id,
                    Email = "Admin",
                    IsEmailVerified = true
                },
                new User
                {
                    Id = 2,
                    Password = "Redactor",
                    Username = "Redactor",
                    UserRoleId = roles[2].Id,
                    Email = "Redactor",
                    IsEmailVerified = true
                }
            };

            var tags = new List<Tag>()
            {
                new Tag
                {
                   Id = 1,
                   DateOfCreation = DateTime.Now,
                   TagName = ".Net"
                },
                new Tag
                {
                    Id = 2,
                    DateOfCreation = DateTime.Now,
                    TagName = "Angular"
                }
            };

            var userTags = new List<UserTag>()
            {
                new UserTag
                {
                    TagId = tags[0].Id,
                    UserId = users[0].Id
                },
                new UserTag
                {
                    TagId = tags[1].Id,
                    UserId = users[0].Id
                }
            };

            builder.Entity<Tag>().HasData(tags);
            builder.Entity<Role>().HasData(roles);
            builder.Entity<User>().HasData(users);
            builder.Entity<UserTag>().HasData(userTags);
        }
    }
}
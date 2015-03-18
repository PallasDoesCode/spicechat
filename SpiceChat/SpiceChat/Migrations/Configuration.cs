using SpiceChat.Models;

namespace SpiceChat.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpiceChat.Models.SpiceChatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpiceChatContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, UserID = 1, FirstName = "System", LastName = "Admin", DisplayName = "SysAdmin", Role = "Administrator"},
                new User() { Id = 2, UserID = 2, FirstName = "Mark", LastName = "Habanero", DisplayName = "Mark", Role = "Helpdesk Admin"},
                new User() { Id = 3, UserID = 3, FirstName = "Noelle", LastName = "Cayenne", DisplayName = "Noelle", Role = "Helpdesk Tech"},
                new User() { Id = 4, UserID = 4, FirstName = "Alex", LastName = "Bell", DisplayName = "Alex", Role = "Helpdesk Admin"}
            );

            context.Conversations.AddOrUpdate(x => x.Id,
                new Conversation() { Id = 1, CreatedAt = new DateTime(year: 2015, month: 1, day: 28, hour: 16, minute: 12, second: 21), CreatedBy =  4}
            );

            context.Messages.AddOrUpdate(x => x.Id,
                new Message() { Id = 1, Body = "Looking for a work around", CreatedAt = new DateTime(year:2015, month:1, day:28, hour:16, minute:12, second:21), ConversationID = 1, CreatedBy = 1},
                new Message() { Id = 1, Body = "That sounds spicy!", CreatedAt = new DateTime(year: 2015, month: 1, day: 28, hour: 16, minute: 12, second: 21), ConversationID = 1, CreatedBy = 2},
                new Message() { Id = 1, Body = "Yeah that happens sometimes", CreatedAt = new DateTime(year: 2015, month: 1, day: 30, hour: 16, minute: 12, second: 21), ConversationID = 1, CreatedBy = 3},
                new Message() { Id = 1, Body = "Still looking for a work around", CreatedAt = new DateTime(year: 2015, month: 1, day: 30, hour: 16, minute: 12, second: 21), ConversationID = 1, CreatedBy = 4}
            );
        }
    }
}

using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // TODO Check why no data is seeded on startup
            builder.HasData(
                new User
                {
                    Id = 1,
                    UserName = "User1"
                },
                new User
                {
                    Id = 2,
                    UserName = "User2"
                }, 
                new User
                {
                    Id = 3,
                    UserName = "User3"
                }
            );
        }
    }
}

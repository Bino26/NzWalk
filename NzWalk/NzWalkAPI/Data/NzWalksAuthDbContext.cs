using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalkAPI.Data
{
    public class NzWalksAuthDbContext : IdentityDbContext
    {
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "715436ed-afc0-4b9d-b8f1-859aa6f73edb";
            var writerRoleId = "e54e233a-069b-4827-a33f-2e822e668054";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId ,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper()

                },
                new IdentityRole
                {
                    Id = writerRoleId ,
                   ConcurrencyStamp = writerRoleId,
                   Name = "Writer",
                   NormalizedName = "Writer".ToUpper()

                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }



    }
}

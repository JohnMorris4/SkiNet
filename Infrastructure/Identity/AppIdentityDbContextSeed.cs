using System.Threading.Tasks;
using System.Linq;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Jones",
                        Street = "123 Main Street",
                        City = "Anytown",
                        State = "US",
                        ZipCode = "12345"
                    }

                };

                await userManager.CreateAsync(user, "Pa#@12word");
            }
        }
    }
}
using E_commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Data.Config
{
    public static class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                // Create New User

                var user = new AppUser()
                {
                    DisplayName = "Mohamed Mosaad",
                    Email = "mohamed@gmail.com",
                    UserName = "mohamed",
                    Address = new Address()
                    {
                        FirstName = "Mohamed",
                        LastName = "Mosaad",
                        Street = "Ahmed Qamha",
                        City = "Alexandria",
                        State = "Alexandria",
                        ZipCode = "21500"
                    }
                };

                await userManager.CreateAsync(user, "P@$$w0rd");

            }
        }
    }
}

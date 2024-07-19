using Microsoft.AspNetCore.Identity;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUsers> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUsers
                {
                    DisplayName = "WonderDeveloper",
                    Email = "WonderDeveloper@gmail.com",
                    UserName = "WonderDeveloper@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Wonder",
                        LastName = "Developer",
                        City = "Cairo",
                        State = "El-Marg",
                        ZipCode = "00000",
                        Street = "Alaa El-Dein"
                    }
                };
                await userManager.CreateAsync(user, "P@$$w0rd");
                return;
            }
        }
    }
}

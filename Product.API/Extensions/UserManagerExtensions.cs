using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product.Core.Entities;
using System.Security.Claims;

namespace Product.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUsers> FindUserByClaimPrincipalWithAddress(this UserManager<AppUsers> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUsers> FindEmailByClaimPrincipal(this UserManager<AppUsers> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.SingleOrDefaultAsync();
        }
    }
}

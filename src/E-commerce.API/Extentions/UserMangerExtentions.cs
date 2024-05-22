using E_commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_commerce.API.Extentions
{
    public static class UserMangerExtentions
    {
        public static async Task<AppUser> FindUserAddressByClaimsPrincipalAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var user = await userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x=>x.NormalizedEmail == email.ToUpper());

            return user;
        }

        public static async Task<AppUser> FindUserEmailByClaimsPrincipalAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());

            return user;
        }
    }
}

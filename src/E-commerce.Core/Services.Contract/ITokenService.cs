using E_commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Services.Contract
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

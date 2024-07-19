using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Services
{
    public interface ITokenServices
    {
        string CreateToken(AppUsers appUsers);
    }
}

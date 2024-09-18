using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encyption;

public class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(securityKey);
        if (keyBytes.Length < 32) // 32 bytes = 256 bits
        {
            throw new ArgumentException("Security key must be at least 256 bits (32 bytes) long.");
        }
        return new SymmetricSecurityKey(keyBytes);
    }
}
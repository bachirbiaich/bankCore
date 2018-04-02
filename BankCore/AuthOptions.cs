using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCore
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // editeur de token
        public const string AUDIENCE = "http://localhost:4492/"; // project @ token
        const string KEY = "mysupersecret_secretkey!123";   // clé secret
        public const int LIFETIME = 1; // la durée de vie du token est de 1 minute
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

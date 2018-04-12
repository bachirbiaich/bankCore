using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BankCore.Models;
using BankCore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BankCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankCoreContext _context;

        public AccountController(BankCoreContext context)
        {
            _context = context;
        }
        
        //Création 2 users
        private List<User> people = new List<User>
        {
            new User { lastname="Frfr", firstname="Frfr", email="fr@fr.fr", password="12345678" }
        };

        [Authorize(Roles = "Administrator")]
        [HttpGet("/isAnAdminLoggedIn")]
        public async Task isAnAdminLoggedIn()
        {
            Response.StatusCode = 200;
            var response = new
            {
                message = "OK"
            };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [HttpPost("/login")]
        public async Task Token([FromBody] User userBody)
        {
            var email = userBody.email;
            var password = userBody.password.Encrypt();

            var user = await _context.Users.SingleOrDefaultAsync(m => m.email == email && m.password == password);

            if (user == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid email or password.");
                return;
            }

            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;
            // Création d'un token JWT
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                user = user
            };

            // Response sérialisation
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimsIdentity.DefaultNameClaimType, user._id.ToString()),
            };
            if (user.email == "admin@ynov.com")
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
using Entities;
using Entities.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.Common
{
    public class GeneralService : IGeneralService
    {
        private readonly IConfiguration configuration;

        public GeneralService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateJwtToken(Account user, bool isPersistence)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ApplicationConstants.UserId, user.AccountGuid.ToString())
                }),
                Expires = isPersistence ? DateTime.Now.AddDays(1) : DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["Jwt:Audience"],
                Issuer= configuration["Jwt:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public string GenerateJwtToken(Account user, bool isPersistence)
        //{
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        //    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //    var tokenOptions = new JwtSecurityToken(
        //        issuer: configuration["Jwt:Issuer"],
        //        audience: configuration["Jwt:Audience"],
        //        claims: new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Username),
        //            new Claim(ClaimTypes.Email, user.Email)
        //        },
        //        expires:isPersistence?DateTime.Now.AddDays(1) : DateTime.Now.AddMinutes(30),
        //        signingCredentials: signingCredentials);
        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //    return tokenString;
        //}

    }
}


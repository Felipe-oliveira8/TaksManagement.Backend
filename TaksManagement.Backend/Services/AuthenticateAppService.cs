using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaksManagement.Backend.Entities;
using TaksManagement.Backend.Entities.MongoEntities;
using TaksManagement.Backend.Models;

namespace TaksManagement.Backend.Services
{
    public class AuthenticateAppService
    {
        private readonly IMongoCollection<UserEntity> users;
        private readonly string key;

        public AuthenticateAppService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("TaskManagement"));
            var database = client.GetDatabase("TaskManagement");

            users = database.GetCollection<UserEntity>("Users");
            this.key = configuration.GetSection("JwtKey").ToString();
        }
        public string Authenticate(LoginModel login)
        {
            var user = this.users.Find(x => x.User == login.Username && x.Password == login.Password).FirstOrDefault();

            if(user is null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

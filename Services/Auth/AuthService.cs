
using MediatR.NotificationPublishers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagement.Services.Auth
{
    public class AuthService : IAuthService
    {
        private const string SecretToken = "x4p8z7Kk1AzpGUZLt/7Bpe0TVI9k2fYenUQvzXOlzXiHugkE3HQY2lCqyMqBA39YoHGPufNqNEFH7MRo6MPoM6gie60yjbjrZJ5b3YTw08ZFbmi0x5yvZlvCK/+NiOXftdWWKmwHCsuGTl0HE47Kaw1Ods9VhzoWLLdBEFbgvMsN1UjoajBZ1/vhch2UKw/MVAgferF0e5CT8Auvsm742eEKcuON8HLCm1guxkRS9xERNYFiVyGdZxthSQ9qGtX2U3SGj03OCl+ZiUgI87e/e16xBNmAZ8gndG3248Dbgm4kzDq6kzezW3gmkQksfhnBKUalg4JVSM1zNFoaRdzzCLpqa00F5NCgiUvzjICwDSM=\r\n";
        private static readonly DateTime TokenLifeTime = DateTime.UtcNow.Add(TimeSpan.FromHours(1));
        private JwtSecurityTokenHandler TokenHandler = new();

        public string GetToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "hospital.uz",
                "hospital.uz",
                claims: [
                    new Claim("Doctor", ""),
                    new Claim("Patient", ""),
                    new Claim("Username", username)
                    ],
                expires: TokenLifeTime,
                signingCredentials: credentials
            );

            return TokenHandler.WriteToken(token);
        }
    }
}

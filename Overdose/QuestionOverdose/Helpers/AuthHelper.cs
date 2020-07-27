using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuestionOverdose.DTO;

namespace QuestionOverdose.Helpers
{
    public class AuthHelper
    {
        private readonly IOptions<AppSettings> _config;

        public AuthHelper(IOptions<AppSettings> config)
        {
            _config = config;
        }

        // Pass should be in range between 5 and 12,
        // add more filters if need.
        public bool IsPassStrong(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass))
            {
                return false;
            }

            return pass.Length > 4 && pass.Length <= 12;
        }

        public string GetJwtoken(UserDTO user)
        {
            AppSettings appSettings = _config.Value;

            // configure strongly typed settings objects
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.UserRole.RoleName)
                    };

            var tokeOptions = new JwtSecurityToken(
                issuer: appSettings.UrlSettings,
                audience: appSettings.UrlSettings,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return token;
        }
    }
}

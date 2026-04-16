using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Options;
using SurveyBasket.Presistence.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SurveyBasket.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public (string token, int Expirein) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
        {
            Claim[] claims =
            {
                new (Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub  , user.Id),
                new (Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email  , user.Email!),
                new (Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Name  , user.FirstName),
                new (Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.FamilyName  , user.LastName),
                new (Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti  ,Guid.NewGuid().ToString()),

                 new(nameof(roles), JsonSerializer.Serialize(roles) , Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes.JsonArray),
                 new(nameof(permissions), JsonSerializer.Serialize(permissions) , Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes.JsonArray)
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpireTime)
             );
            return (token: new JwtSecurityTokenHandler().WriteToken(token), Expirein: _options.ExpireTime);
        }

        public string ValidateToken(string token)
        {

            var TokenHandler = new JwtSecurityTokenHandler();
            var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            try
            {


                TokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = IssuerSigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var JwtToken = validatedToken as JwtSecurityToken;
                return JwtToken!.Claims.First(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub).Value;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}

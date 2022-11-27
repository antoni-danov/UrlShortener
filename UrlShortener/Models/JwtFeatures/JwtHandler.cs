using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener.Models.UserDtos.Login;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace UrlShortener.Models.JwtFeatures
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfigurationSection _goolgeSettings;
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("uid", user.Id ),
                new Claim("email", user.Email)
            };

            return claims;
        }
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);
                
            return tokenOptions;
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalProviderDto externalAuth)
        {
            try
            {
                ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings() {
                    Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
                };
                Payload payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
               
                return payload;
            }
            catch(Exception ex)
            {
                new NullReferenceException();
                return null;
            }
        }
    }
}

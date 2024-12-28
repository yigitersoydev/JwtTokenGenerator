using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtTokenGenerator
{
    public class TokenManager
    {
        public Token CreateAccessToken(string issuer, string audience, string secretKey, int expirationMinutes)
        {
            try
            {
                Token tokenInstance = new Token();

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                tokenInstance.Expiration = DateTime.Now.AddMinutes(expirationMinutes);

                JwtSecurityToken securityToken = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: tokenInstance.Expiration,
                    notBefore: DateTime.Now,
                    signingCredentials: signingCredentials
                );

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

                AppendTokenToFile(tokenInstance.AccessToken);

                return tokenInstance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private void AppendTokenToFile(string token)
        {
            string filePath = "tokens.txt";

            try
            {
                File.AppendAllText(filePath, $"{token}{Environment.NewLine}");
                Console.WriteLine("Token added to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}

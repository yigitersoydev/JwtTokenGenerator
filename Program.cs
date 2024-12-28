namespace JwtTokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the Issuer: ");
            string issuer = Console.ReadLine();

            Console.Write("Enter the Audience: ");
            string audience = Console.ReadLine();

            Console.Write("Enter the Secret Key (e.g., 'your-strong-secret-key-here'): ");
            string secretKey = Console.ReadLine();

            Console.Write("Enter the token expiration time in minutes: ");
            if (!int.TryParse(Console.ReadLine(), out int expirationMinutes))
            {
                Console.WriteLine("Invalid input for expiration time. Setting default expiration time to 30 minutes.");
                expirationMinutes = 30;  
            }

            TokenManager tokenManager = new TokenManager();

            Token token = tokenManager.CreateAccessToken(issuer, audience, secretKey, expirationMinutes);

            if (token != null)
            {
                Console.WriteLine("Generated JWT Token:");
                Console.WriteLine(token.AccessToken);
                Console.WriteLine($"Token Expiration: {token.Expiration}");
            }
            else
            {
                Console.WriteLine("Failed to generate token.");
            }
        }
    }
}

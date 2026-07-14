using CerGlazerAPI.Data;
using CerGlazerAPI.Entities;
using CerGlazerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CerGlazerAPI.Services
{
    public class AuthService(UserDbContext context, IConfiguration appConfiguration) 
        : IAuthService
    {
        public Task<string?> LoginUserAsync(UserDTO userLoginRequest)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName.ToLower() == userLoginRequest.UserName.ToLower());

            if (userLoginRequest.UserName != user?.UserName ||
                new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userLoginRequest.Password)
                    == PasswordVerificationResult.Failed)
            {
                return null;
            };

            return Task.FromResult<string?>(GenerateToken(user));
        }

        public async Task<User?> RegisterUserAsync(UserDTO userRegistrationRequest)
        {
            // Verify that user does not already exist in the database.
            if (await context.Users.AnyAsync(u => u.UserName.ToLower() 
                == userRegistrationRequest.UserName.ToLower()))
            {
                return null; // User already exists
            }

            // Create a new user entity and hash the password
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userRegistrationRequest.UserName,
                Email = userRegistrationRequest.Email
            };

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, userRegistrationRequest.Password);

            user.PasswordHash = hashedPassword;

            // Add the new user to the database
            context.Users.Add(user);

            // Save changes to the database
            await context.SaveChangesAsync();

            return user;
        }

        private string GenerateToken(User user)
        {
            // Implement your token generation logic here
            // For example, use JWT to generate a token based on the user's information
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appConfiguration.GetValue<string>("AuthSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: appConfiguration.GetValue<string>("AuthSettings:Issuer"),
                audience: appConfiguration.GetValue<string>("AuthSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}

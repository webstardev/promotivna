using MarkomPos.DataInterface;
using MarkomPos.DataInterface.Common.Response;
using MarkomPos.Entities;
using MarkomPos.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repositories
{
    public class UserRepositories : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly IResponseDTO _response;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepositories(
            IConfiguration configuration,
            ApplicationContext context,
            UserManager<User> userManager,
            IResponseDTO responseDTO,
            IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _ctx = context;
            _userManager = userManager;
            _response = responseDTO;
            _passwordHasher = passwordHasher;
        }


        public async Task<IResponseDTO> Login(string username, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(username);

            if (appUser == null)
            {
                _response.Message = "Email is not found";
                _response.IsPassed = false;
                return _response;
            }

            if (!appUser.Active)
            {
                _response.Message = "Your Account is not Active, Please contact the administration";
                _response.IsPassed = false;
                return _response;
            }

            if(_passwordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                _response.Message = "Invalid password.";
                _response.IsPassed = false;
                return _response;
            }
            // authentication successful so generate jwt token
            var token = GenerateJSONWebToken(appUser.ID, appUser.Email);
            _response.IsPassed = true;
            _response.Message = "You are logged in successfully.";
            _response.Data = new
            {
                token,
                userData = appUser
            };

            return _response;
        }

        public string GenerateJSONWebToken(int userId, string userName)
        {
            var signingKey = Convert.FromBase64String(_configuration["Jwt:Key"]);
            //var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);
            var expiryDuration = 24 * 60;

            var claims = new List<Claim>
            {
                new Claim("userid", userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };

            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var item in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, item);
                claims.Add(roleClaim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }

    }
}

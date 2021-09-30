using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleCRM.Data.Repository;
using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.API.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Check user's email and password if correct
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Authenticate(string email, string password);

        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> Add(User user);

        /// <summary>
        /// Get user details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetById(string id);

        /// <summary>
        /// Get user details by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetByEmail(string email);

        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> Update(User user);

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string id, string password);

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TokenWithExpireDate GenerateToken(string id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthSettings _authSettings;
        public UserService(IUserRepository userRepository, IOptions<AuthSettings> authSettingsAccessor)
        {
            _userRepository = userRepository;
            _authSettings = authSettingsAccessor.Value;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if(user == null)
            {
                return false;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return false;
            }
            return true;
        }

        public async Task<User> Add(User user)
        {
            // hash user password
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await _userRepository.Add(user);
        }

        public async Task<bool> UpdatePassword(string id, string password)
        {
            password = BCrypt.Net.BCrypt.HashPassword(password);
            return await _userRepository.UpdatePassword(id, password);
        }

        public async Task<bool> Update(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }
        
        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public TokenWithExpireDate GenerateToken(string Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.AppSecret);
            var expires = DateTime.UtcNow.AddDays(30);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenWithExpireDate {
                Value = tokenHandler.WriteToken(token),
                Expires = expires.ToString()
            };
        }
    }
}

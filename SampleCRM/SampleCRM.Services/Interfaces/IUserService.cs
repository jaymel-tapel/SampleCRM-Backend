using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Services.Interfaces
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
}

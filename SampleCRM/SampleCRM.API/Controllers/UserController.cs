using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleCRM.API.DTOs;
using SampleCRM.API.Services;
using SampleCRM.Entities;
using SampleCRM.Helpers;
using SampleCRM.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SampleCRM.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        // Login
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            _logger.LogInformation("User log in attempt");
            User user = await _userService.GetByEmail(userLogin.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email not found.");
            }

            if(ModelState.IsValid)
            {
                // Authenticate if model is valid
                if (await _userService.Authenticate(userLogin.Email, userLogin.Password))
                {
                    var token = _userService.GenerateToken(user.Id);
                    _logger.LogInformation("User log in successful");
                    return Ok(new UserWithToken
                    {
                        Token = token,
                        User = new User {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email
                        }
                    });
                }
                else
                {
                    _logger.LogInformation("User log in failed");
                    return Unauthorized("Invalid email or password.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Register
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            _logger.LogInformation("User register attempt");

            User user = await _userService.GetByEmail(userRegister.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already existing.");
            }

            if (ModelState.IsValid)
            {
                User newUser = new ()
                {
                    Email = userRegister.Email,
                    LastName = userRegister.LastName,
                    FirstName = userRegister.FirstName,
                    Password = userRegister.Password
                };
                await _userService.Add(newUser);

                _logger.LogInformation("User register successful");
                // remove password from response body
                newUser.Password = null;
                return Ok(newUser);
            }
            else
            {
                _logger.LogInformation("User register failed");
                return BadRequest(ModelState);
            }
        }

        // Update Profile
        [Authorize]
        [HttpPost("api/update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdate userUpdate)
        {
            _logger.LogInformation("User update attempt");
            if (ModelState.IsValid)
            {
                User user = new ()
                {
                    Id = userUpdate.Id.ToString(),
                    Email = userUpdate.Email,
                    FirstName = userUpdate.FirstName,
                    LastName = userUpdate.LastName
                };

                _logger.LogInformation("User update success");
                return Ok(await _userService.Update(user));
            }
            else
            {
                _logger.LogInformation("User update failed");
                return BadRequest(ModelState);
            }
        }

        // Change Password
        [Authorize]
        [HttpPost("api/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePassword userChangePassword)
        {
            _logger.LogInformation("User change password attempt");
            if (ModelState.IsValid)
            {
                _logger.LogInformation("User change password success");
                return Ok(await _userService.UpdatePassword(userChangePassword.Id, userChangePassword.Password));
            }
            else
            {
                _logger.LogInformation("User change password failed");
                return BadRequest(ModelState);
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleCRM.API.DTOs;
using SampleCRM.API.Services;
using SampleCRM.Entities;
using SampleCRM.Helpers;
using SampleCRM.Services.Interfaces;
using System.Threading.Tasks;

namespace SampleCRM.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // Login
        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
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

                // remove password from response body
                newUser.Password = null;
                return Ok(newUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Update Profile
        [Authorize]
        [HttpPost("api/update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdate userUpdate)
        {
            if (ModelState.IsValid)
            {
                User user = new ()
                {
                    Id = userUpdate.Id.ToString(),
                    Email = userUpdate.Email,
                    FirstName = userUpdate.FirstName,
                    LastName = userUpdate.LastName
                };
                return Ok(await _userService.Update(user));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Change Password
        [Authorize]
        [HttpPost("api/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePassword userChangePassword)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.UpdatePassword(userChangePassword.Id, userChangePassword.Password));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

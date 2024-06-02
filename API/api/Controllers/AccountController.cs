using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Authenticates a user with the login and password.
        /// </summary>
        /// <param name="login">The login credentials containing username and password.</param>
        /// <returns>Returns user information and token upon successful login.</returns>
        /// <response code="200">Returns the user's information and token.</response>
        /// <response code="400">If the provided request data is invalid.</response>
        /// <response code="401">If the username or password is invalid.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == login.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid Username or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid Username or password");
            }

            return Ok(new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">Login, email and password</param>
        /// <returns>Returns the user information and token if registration is successful.</returns>
        /// <response code="200">Returns the newly registered user's information and token.</response>
        /// <response code="400">If the provided request data is invalid.</response>
        /// <response code="500">If an error occurs during user creation or role assignment.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, register.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            });
                    }
                    return StatusCode(500, roleResult.Errors);
                }
                return StatusCode(500, createdUser.Errors);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssetwiseApi.Models;
// using AssetwiseApi.Services;
using AssetwiseApi.Authentication;
using Microsoft.AspNetCore.Authorization;
// using AssetwiseApi.Authentication;
// using WebApiAuthentication.Authentication;
// using WebApiAuthentication.DataAccess.Entities;

namespace AssetwiseApi.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<User> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Username);

            if (existingUser != null)
                return Conflict("User already exists.");

            var newUser = new User
            {
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            
            var result = await _userManager.CreateAsync(newUser, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError,
                       $"Failed to create user: {string.Join(" ", result.Errors.Select(e => e.Description))}"); 
  
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))  
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));  
            if (!await _roleManager.RoleExistsAsync(UserRoles.Tenant))  
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Tenant));  
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))  
            {  
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);  
            }  
  
            return Ok("User created successfully!");  
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login ([FromBody]LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if(user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var userRoles = await _userManager.GetRolesAsync(user); 
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)  
            {  
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
            }  

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new LoginResponse
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Roles = userRoles.ToList()
            });
        }
    }
}
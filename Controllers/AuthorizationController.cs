using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DataClass;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class AuthorizationController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ILogger<AuthorizationController> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;
		public AuthorizationController(UserManager<IdentityUser> userManager, IConfiguration configuration, ILogger<AuthorizationController> logger, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_configuration = configuration;
			_logger = logger;
			_roleManager = roleManager;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model) 
		{ 
			var userExists = await _userManager.FindByNameAsync(model.Username);
			if (userExists != null) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
			}  
			IdentityUser user = new() { SecurityStamp = Guid.NewGuid().ToString(), UserName = model.Username,  EmailConfirmed = true};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (model.IsAdmin)
			{
				var claim = new Claim(ClaimTypes.Role, "IsAdmin");
				await _userManager.AddClaimAsync(user, claim);
			}

			if (!result.Succeeded) 
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
			}

			return Ok(new Response { Status = "Success", Message = "User created successfully!" });
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var authClaims = new List<Claim>();

				var userRoles = await _userManager.GetRolesAsync(user);
				//foreach (var userRole in userRoles)
				//{
				//	authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				//}


				var userClaim = await _userManager.GetClaimsAsync(user);
				authClaims.AddRange(userClaim);

				authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

				//authClaims.AddRange(userRoles);

				//var claim = new Claim(ClaimTypes.Role, "IsAdmin");

				//authClaims.Add(claim);

				var token = GetToken(authClaims);
				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}
			return Unauthorized();
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
			var token = new JwtSecurityToken(
			issuer: _configuration["JWT:ValidIssuer"],
			audience: _configuration["JWT:ValidAudience"],
			expires: DateTime.Now.AddHours(3),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);
			return token;
		}
	}
}

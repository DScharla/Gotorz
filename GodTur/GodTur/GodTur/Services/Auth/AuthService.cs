using GodTur.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GodTur.Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace GodTur.Services.Auth
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly TokenValidationParameters _tokenValidationParameters;
		private readonly UserDbContext _context;
		private readonly IConfiguration _configuration;
		public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserDbContext context, TokenValidationParameters tokenValidationParameters)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_tokenValidationParameters = tokenValidationParameters;
			_context = context;
		}

		public async Task<AuthResponseDTO> GetAuthResponse(string username, string password)
		{
			var userExists = await _userManager.FindByEmailAsync(username);
			if (userExists != null)
			{
				var canLogin = await _userManager.CheckPasswordAsync(userExists, password);
				if (canLogin)
				{
					var tokenValue = await GenerateJWTTokenAsync(userExists, null);
					return tokenValue;
				}
			}
			return null;
		}

		public async Task<AuthResponseDTO> VerifyAndGenerateTokenAsync(AuthResponseDTO authResponseDTO)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == authResponseDTO.RefreshToken);
			var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

			try
			{
				var tokenCheckResult = jwtTokenHandler.ValidateToken(authResponseDTO.Token, _tokenValidationParameters, out var validatedToken);

				return await GenerateJWTTokenAsync(dbUser, storedToken);
			}
			catch (SecurityTokenExpiredException)
			{
				if (storedToken.DateExpire >= DateTime.UtcNow)
				{
					return await GenerateJWTTokenAsync(dbUser, storedToken);
				}
				else
				{
					return await GenerateJWTTokenAsync(dbUser, null);
				}
			}
		}

		private async Task<AuthResponseDTO> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken rToken)
		{
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var userRoles = await _userManager.GetRolesAsync(user);

			foreach (var userRole in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}


			var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				expires: DateTime.UtcNow.AddMinutes(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

			var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			if (rToken != null)
			{
				var rTokenResponse = new AuthResponseDTO()
				{
					Token = jwtToken,
					RefreshToken = rToken.Token
				};
				return rTokenResponse;
			}

			var refreshToken = new RefreshToken()
			{
				JwtId = token.Id,
				IsRevoked = false,
				UserId = user.Id,
				DateAdded = DateTime.UtcNow,
				DateExpire = DateTime.UtcNow.AddMonths(6),
				Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
			};
			await _context.RefreshTokens.AddAsync(refreshToken);
			await _context.SaveChangesAsync();


			var response = new AuthResponseDTO()
			{
				Token = jwtToken,
				RefreshToken = refreshToken.Token
			};

			return response;

		}
	}
}

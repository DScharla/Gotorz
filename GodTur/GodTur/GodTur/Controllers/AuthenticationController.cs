using GodTur.Models.Auth;
using GodTur.Models.Context;
using GodTur.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;

namespace GodTur.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthenticationController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Please, provide all required fields");
			}

			var result = await _authService.GetAuthResponse(loginDTO.EmailAddress, loginDTO.Password);
			
			if (result != null)
			{
				return Ok(result);
			}
			return Unauthorized();
		}

		[HttpPost("Refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] AuthResponseDTO authResponseDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Please, provide all required fields");
			}

			var result = await _authService.VerifyAndGenerateTokenAsync(authResponseDTO);
			return Ok(result);
		}
	}
}

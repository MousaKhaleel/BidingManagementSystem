using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BidingManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto registerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Failed: " + ModelState);
			}
			try
			{
				var result = await _authService.RegisterAsync(registerDto);

				if (result.Success)
				{
					return Ok("Registered successfully");
				}

				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			try
			{
				var result = await _authService.LoginAsync(loginDto);
				if (result.Success)
				{
					var tokinString = _authService.GenerateJwtTokenString(result.user);
					return Ok("Login successful, tokin:" + tokinString);
				}
				return BadRequest(result.ErrorMessage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await _authService.LogoutAsync();
				return Ok("Succses");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			try
			{
				var result = await _authService.GetProfileAsync();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Authorize]
		[HttpPut("ChangePassword")]
		public async Task<IActionResult> ChangePassword(string password)
		{
			if (password == null)
			{
				return BadRequest("Password can not be empty");
			}
			try
			{
				var result = await _authService.ChangePasswordAsync(password);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		//TODO reset password
	}
}

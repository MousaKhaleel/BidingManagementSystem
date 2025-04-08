using BidingManagementSystem.Application.Commands.Auth.ChangeUserPasswordAsync;
using BidingManagementSystem.Application.Commands.Auth.LoginUserAsync;
using BidingManagementSystem.Application.Commands.Auth.LogoutUserAsync;
using BidingManagementSystem.Application.Commands.Auth.RegisterUserAsync;
using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Queries.Auth.GenerateJwtTokenStringAsync;
using BidingManagementSystem.Application.Queries.Auth.GetUserProfileAsync;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BidingManagementSystem.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
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
				var command = new RegisterUserCommand(registerDto);

				var result = await _mediator.Send(command);
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
				var command = new LoginUserCommand(loginDto);

				var result = await _mediator.Send(command);
				if (result.Success)
				{
					var tokinCommand = new GenerateJwtTokenStringQuery(result.user);

					var tokinString = await _mediator.Send(tokinCommand);
					return Ok("Login successful, tokin: " + tokinString);
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
				var command = new LogoutUserCommand();

				await _mediator.Send(command);
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
				var query = new GetUserProfileQuery();

				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Authorize]
		[HttpPut("ResetPassword")]
		public async Task<IActionResult> ChangePassword(string password)
		{
			if (password == null)
			{
				return BadRequest("Password can not be empty");
			}
			try
			{
				var command = new ChangeUserPasswordCommand(password);

				var result = await _mediator.Send(command);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

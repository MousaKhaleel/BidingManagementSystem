using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Services
{
	public class AuthService : IAuthService
	{//TODO use cqrs
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;
		public AuthService(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
		}

		public async Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(string password)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var user = await _userManager.FindByIdAsync(userId);
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, password!);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
			return (true, string.Empty);
		}

		public async Task<string> GenerateJwtTokenString(User user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			IEnumerable<Claim> claims = new List<Claim>
	{
		new Claim(ClaimTypes.Email, user.UserName),
		new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
    };

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(600),
				issuer: _configuration.GetSection("Jwt:Issuer").Value,
				audience: _configuration.GetSection("Jwt:Audience").Value,
				signingCredentials: new SigningCredentials(
					key,
					SecurityAlgorithms.HmacSha256Signature)
			);

			string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenString;
		}

		public async Task<User> GetProfileAsync()
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var result = await _unitOfWork.userRepository.GetByIdAsync(userId);
			return result;
		}

		public async Task<(bool Success, string ErrorMessage, User user)> LoginAsync(LoginDto loginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result), null);
			}
			var userFind = await _userManager.FindByNameAsync(loginDto.UserName);
			return (true, string.Empty, userFind);
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterDto registerDto)
		{
			var user = new User
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email,
			};
			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
			var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role.ToString());
			if (!roleResult.Succeeded)
			{
				return (false, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
			}
			await _unitOfWork.userRepository.AddAsync(user);
			await _unitOfWork.SaveChangesAsync();
			return (true, string.Empty);
		}
	}
}

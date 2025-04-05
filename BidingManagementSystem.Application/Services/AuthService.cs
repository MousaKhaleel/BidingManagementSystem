using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Services
{
	public class AuthService : IAuthService
	{//TODO
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AuthService(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
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

		public async Task<User> GetProfileAsync()
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var result = await _unitOfWork.userRepository.GetByIdAsync(userId);
			return result;
		}

		public async Task<(bool Success, string ErrorMessage)> LoginAsync(LoginDto loginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result));
			}
			return (true, string.Empty);
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterDto registerDto)
		{
			throw new NotImplementedException();//TODO
		}
	}
}

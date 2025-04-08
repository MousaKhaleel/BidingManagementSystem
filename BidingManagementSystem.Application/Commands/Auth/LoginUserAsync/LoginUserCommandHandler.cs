using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Auth.LoginUserAsync
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, (bool Success, string ErrorMessage, User user)>
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		public LoginUserCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}
		public async Task<(bool Success, string ErrorMessage, User user)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			var result = await _signInManager.PasswordSignInAsync(request.loginDto.UserName, request.loginDto.Password, false, false);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result), null);
			}
			var userFind = await _userManager.FindByNameAsync(request.loginDto.UserName);
			return (true, string.Empty, userFind);
		}
	}
}

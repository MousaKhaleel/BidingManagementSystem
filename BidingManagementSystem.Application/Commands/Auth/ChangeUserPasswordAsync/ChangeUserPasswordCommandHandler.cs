using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Auth.ChangeUserPasswordAsync
{
	public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, (bool Success, string ErrorMessage)>
	{
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ChangeUserPasswordCommandHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var user = await _userManager.FindByIdAsync(userId);
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, request.password!);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
			return (true, string.Empty);
		}
	}
}

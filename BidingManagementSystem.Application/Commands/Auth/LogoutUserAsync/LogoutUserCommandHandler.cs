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

namespace BidingManagementSystem.Application.Commands.Auth.LogoutUserAsync
{
	public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
	{
		private readonly SignInManager<User> _signInManager;
		public LogoutUserCommandHandler(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;

		}
		public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
		{
			await _signInManager.SignOutAsync();
		}
	}
}

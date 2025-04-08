using BidingManagementSystem.Application.Dtos;
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

namespace BidingManagementSystem.Application.Commands.Auth.RegisterUserAsync
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RegisterUserCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = new User
			{
				UserName = request.registerDto.UserName,
				Email = request.registerDto.Email,
			};
			var result = await _userManager.CreateAsync(user, request.registerDto.Password);
			if (!result.Succeeded)
			{
				return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
			}
			var roleResult = await _userManager.AddToRoleAsync(user, request.registerDto.Role.ToString());
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

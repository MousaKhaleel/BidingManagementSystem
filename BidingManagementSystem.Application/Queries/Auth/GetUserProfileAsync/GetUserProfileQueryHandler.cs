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

namespace BidingManagementSystem.Application.Queries.Auth.GetUserProfileAsync
{
	public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public GetUserProfileQueryHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
		{
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var result = await _unitOfWork.userRepository.GetByIdAsync(userId);
			var userDto = new UserDto
			{
				Email = result.Email,
				UserName = result.UserName,
				Role = (await _userManager.GetRolesAsync(result)).FirstOrDefault()
			};
			return userDto;
		}
	}
}

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

namespace BidingManagementSystem.Application.Commands.Bid.UpdateBidAsync
{
	public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public UpdateBidCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UpdateBidCommand request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return (false, "Bid not found");
			}
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			if (bid.BidderId != userId)
			{
				return (false, "You are not authorized to update this bid");
			}

			bid.Amount = request.bidDto.Amount;

			await _unitOfWork.bidRepository.UpdateAsync(bid);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

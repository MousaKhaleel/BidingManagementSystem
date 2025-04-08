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

namespace BidingManagementSystem.Application.Commands.Bid.SubmitBid
{
	public class SubmitBidCommandHandler : IRequestHandler<SubmitBidCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public SubmitBidCommandHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(SubmitBidCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.bidDto == null)
					return (false, "Bid data is required.");

				var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
				var bid = new Domain.Models.Bid
				{
					BidderId = userId,
					TenderId = request.tenderId,
					Amount = request.bidDto.Amount,
				};

				await _unitOfWork.bidRepository.AddAsync(bid);
				await _unitOfWork.SaveChangesAsync();

				return (true, string.Empty);
			}
			catch (Exception ex)
			{
				return (false, $"Error creating tender: {ex.Message}");
			}
		}
	}
}

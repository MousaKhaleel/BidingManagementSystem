using BidingManagementSystem.Domain.Interfaces;
using MediatR;
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

		public SubmitBidCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(SubmitBidCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.bidDto == null)
					return (false, "Bid data is required.");
				var bid = new Domain.Models.Bid
				{
					BidderId = request.bidDto.BidderId,
					TenderId = request.bidDto.TenderId,
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

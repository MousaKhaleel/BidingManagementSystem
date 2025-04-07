using BidingManagementSystem.Domain.Interfaces;
using MediatR;
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

		public UpdateBidCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UpdateBidCommand request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return (false, "Bid not found");
			}
			var bidUpdate = new Domain.Models.Bid
			{
				BidId = request.bidId,
				BidderId = request.bidDto.BidderId,
				TenderId = request.bidDto.TenderId,
				Amount = request.bidDto.Amount,
			};

			await _unitOfWork.bidRepository.UpdateAsync(bid);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

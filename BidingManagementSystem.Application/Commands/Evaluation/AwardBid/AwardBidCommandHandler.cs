using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Evaluation.AwardBid
{
	public class AwardBidCommandHandler : IRequestHandler<AwardBidCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AwardBidCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(AwardBidCommand request, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.bidRepository.AwardBidAsync(request.TenderId, request.BidId);
			if (result)
			{
				return (true, null);
			}
			else
			{
				return (false, "Failed to award the bid.");
			}
		}
	}
}

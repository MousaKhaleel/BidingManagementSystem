using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Evaluation.GetAwardedBidAsync
{
	public class GetAwardedBidQueryHandler : IRequestHandler<GetAwardedBidQuery, BidDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAwardedBidQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<BidDto> Handle(GetAwardedBidQuery request, CancellationToken cancellationToken)
		{
			var awardedBid = await _unitOfWork.bidRepository.GetAwardedBidAsync(request.tenderId);
			if (awardedBid == null)
			{
				throw new Exception("No awarded bid found for the given tender ID.");
			}
			var bidDto = new BidDto
			{
				TenderId = awardedBid.TenderId,
				BidderId = awardedBid.BidderId,
				Amount = awardedBid.Amount,
				Status = awardedBid.Status,
			};
			return bidDto;
		}
	}
}

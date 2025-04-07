using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Bid.GetBidsByTenderIdAsync
{
	public class GetBidsByTenderIdQueryHandler : IRequestHandler<GetBidsByTenderIdQuery, IEnumerable<BidDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBidsByTenderIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<BidDto>> Handle(GetBidsByTenderIdQuery request, CancellationToken cancellationToken)
		{
			var bids = await _unitOfWork.bidRepository.GetBidsByTenderIdAsync(request.tenderId);
			return bids.Select(b => new BidDto
			{
				TenderId = b.TenderId,
				BidderId = b.BidderId,
				Amount = b.Amount,
				Status = b.Status,
			}).ToList();
		}
	}
}

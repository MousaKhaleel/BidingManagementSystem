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
	public class GetBidsByTenderIdQueryHandler : IRequestHandler<GetBidsByTenderIdQuery, IEnumerable<GetBidDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBidsByTenderIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<GetBidDto>> Handle(GetBidsByTenderIdQuery request, CancellationToken cancellationToken)
		{
			var bids = await _unitOfWork.bidRepository.GetBidsByTenderIdAsync(request.tenderId);
			return bids.Select(b => new GetBidDto
			{
				TenderId = b.TenderId,
				BidderId = b.BidderId,
				Amount = b.Amount,
				Status = b.Status,
			}).ToList();
		}
	}
}

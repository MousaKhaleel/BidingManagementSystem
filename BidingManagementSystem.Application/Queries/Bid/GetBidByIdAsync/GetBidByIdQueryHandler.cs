﻿using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Bid.GetBidByIdAsync
{
	public class GetBidByIdQueryHandler : IRequestHandler<GetBidByIdQuery, GetBidDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBidByIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<GetBidDto> Handle(GetBidByIdQuery request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return null;
			}

			var bidDto = new GetBidDto
			{
				BidderId = bid.BidderId,
				TenderId = bid.TenderId,
				Amount = bid.Amount,
				//SubmissionDate = bid.SubmissionDate
			};

			return bidDto;
		}
	}
}

using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Evaluation.GetBidScoreAsync
{
	public class GetBidScoreQueryHandler : IRequestHandler<GetBidScoreQuery, decimal>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBidScoreQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<decimal> Handle(GetBidScoreQuery request, CancellationToken cancellationToken)
		{
			var evaluation = await _unitOfWork.evaluationRepository.GetEvaluationByBidIdAsync(request.bidId);
			if (evaluation == null)
			{
				return (-1);
			}
			return (evaluation.Score);
		}
	}
}

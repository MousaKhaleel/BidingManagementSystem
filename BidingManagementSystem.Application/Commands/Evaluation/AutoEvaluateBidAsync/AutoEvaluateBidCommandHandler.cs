using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Evaluation.AutoEvaluateBidAsync
{
	public class AutoEvaluateBidCommandHandler : IRequestHandler<AutoEvaluateBidCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AutoEvaluateBidCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool Success, string ErrorMessage)> Handle(AutoEvaluateBidCommand request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return (false, "Bid not found");
			}
			var bids = await _unitOfWork.bidRepository.GetBidsByTenderIdAsync(bid.TenderId);
			var lowestBid = bids.OrderBy(b => b.Amount).FirstOrDefault();
			if (lowestBid == null)
			{
				return (false, "No bids available for evaluation");
			}
			var evaluation = await _unitOfWork.evaluationRepository.GetEvaluationByBidIdAsync(request.bidId);
			if (evaluation != null)
			{
				return (false, "Bid has already been evaluated");
			}
			decimal score = (lowestBid.Amount / bid.Amount) * 100;

			var evaluationResult = new Domain.Models.Evaluation
			{
				BidId = request.bidId,
				Score = score,
				Criteria = Domain.Models.Enums.EvaluationCriteria.LowestBid
			};
			await _unitOfWork.evaluationRepository.AddAsync(evaluationResult);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

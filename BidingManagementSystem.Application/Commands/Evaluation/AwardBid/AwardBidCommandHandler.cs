using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Domain.Models.Enums;
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
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.BidId);
			var award = new Award
			{
				TenderId = bid.TenderId,
				WinningBidId = request.BidId,
			};
			await _unitOfWork.awardRepository.AddAsync(award);

				bid.Status = BidStatus.Accepted;
				await _unitOfWork.bidRepository.UpdateAsync(bid);

			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(bid.TenderId);
			tender.Status = TenderStatus.Awarded;
			await _unitOfWork.tenderRepository.UpdateAsync(tender);

			await _unitOfWork.SaveChangesAsync();
			return (true, "");
		}
	}
}

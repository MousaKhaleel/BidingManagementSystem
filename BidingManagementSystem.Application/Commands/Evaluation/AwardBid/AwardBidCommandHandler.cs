using BidingManagementSystem.Application.Notifications;
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
		private readonly INotificationService _notificationService;

		public AwardBidCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
		{
			_unitOfWork = unitOfWork;
			_notificationService = notificationService;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(AwardBidCommand request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetBidByIdAsync(request.BidId);
			var award = new Award
			{
				TenderId = bid.TenderId,
				WinningBidId = request.BidId,
			};
			await _unitOfWork.awardRepository.AddAsync(award);

			bid.Status = BidStatus.Accepted;
			await _unitOfWork.bidRepository.UpdateAsync(bid);
			await _notificationService.SendEmailAsync(bid.Bidder.Email, "Bid Awarded", $"Congratulations! Your bid for tender {bid.Tender.Title} has been awarded.");

			var bids = await _unitOfWork.bidRepository.GetBidsByTenderIdAsync(bid.TenderId);
			foreach (var b in bids)
			{
				if (b.BidderId != bid.BidderId)
				{
					b.Status = BidStatus.Rejected;
					await _unitOfWork.bidRepository.UpdateAsync(b);
					await _notificationService.SendEmailAsync(b.Bidder.Email, "Bid Rejected", $"Unfortunately, your bid for tender {bid.Tender.Title} has been rejected.");
				}
			}

			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(bid.TenderId);
			tender.Status = TenderStatus.Awarded;
			await _unitOfWork.tenderRepository.UpdateAsync(tender);

			await _unitOfWork.SaveChangesAsync();
			return (true, "");
		}
	}
}

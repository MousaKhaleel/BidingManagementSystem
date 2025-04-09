using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using BidingManagementSystem.Domain.Models.Enums;

namespace BidingManagementSystem.Application.Commands.Evaluation.EvaluateBid
{
	public class EvaluateBidCommandHandler : IRequestHandler<EvaluateBidCommand, (bool, string)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public EvaluateBidCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool, string)> Handle(EvaluateBidCommand request, CancellationToken cancellationToken)
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.BidId);
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

			var autoEvaluation = await _unitOfWork.evaluationRepository.GetEvaluationByBidIdAsync(request.BidId);
			if (autoEvaluation != null)
			{
				autoEvaluation.Score = request.EvaluationDto.Score;
				autoEvaluation.Criteria = request.EvaluationDto.Criteria;
				autoEvaluation.EvaluatorId = userId;
			}
			var evaluation = new Domain.Models.Evaluation
			{
				TenderId = bid.TenderId,
				BidId = request.BidId,
				Score = request.EvaluationDto.Score,
				Criteria = request.EvaluationDto.Criteria,
				EvaluatorId = userId
			};
			await _unitOfWork.evaluationRepository.AddAsync(evaluation);

			bid.Status = BidStatus.Accepted;
			await _unitOfWork.bidRepository.UpdateAsync(bid);

			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(bid.TenderId);
			tender.Status = TenderStatus.Awarded;
			await _unitOfWork.tenderRepository.UpdateAsync(tender);

			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

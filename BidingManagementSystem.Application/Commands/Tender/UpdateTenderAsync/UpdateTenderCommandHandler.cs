using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UpdateTender
{
	public class UpdateTenderCommandHandler : IRequestHandler<UpdateTenderCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public UpdateTenderCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UpdateTenderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var tender = await _unitOfWork.tenderRepository.GetByIdAsync(request.tenderId);
				if (tender == null)
				{
					return (false, "Tender not found");
				}


				var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
				if (userId != tender.UserId)
				{
					return (false, "not authorized to edit this tender.");
				}

				tender.ReferenceNumber = request.tenderDto.ReferenceNumber;
				tender.Title = request.tenderDto.Title;
				tender.Description = request.tenderDto.Description;
				tender.DeadLine = request.tenderDto.DeadLine;
				tender.Budget = request.tenderDto.Budget;
				tender.EligibilityCriteria = request.tenderDto.EligibilityCriteria;

				await _unitOfWork.tenderRepository.UpdateAsync(tender);
				await _unitOfWork.SaveChangesAsync();
				return (true, null);
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}
	}
}

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

namespace BidingManagementSystem.Application.Commands.Tender.CreateTender
{
	public class CreateTenderCommandHandler : IRequestHandler<CreateTenderCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public CreateTenderCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.tenderDto == null)
					return (false, "Tender data is required.");

				var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

				var tender = new Domain.Models.Tender
				{
					Title = request.tenderDto.Title,
					ReferenceNumber = request.tenderDto.ReferenceNumber,
					Description = request.tenderDto.Description,
					DeadLine = request.tenderDto.DeadLine,
					Budget = request.tenderDto.Budget,
					UserId = userId,
					EligibilityCriteria = request.tenderDto.EligibilityCriteria,
				};
				await _unitOfWork.tenderRepository.AddAsync(tender);
				await _unitOfWork.SaveChangesAsync();
				return (true, string.Empty);
			}
			catch (Exception ex)
			{
				return (false, $"Error creating tender: {ex.Message}");
			}
		}
	}
}

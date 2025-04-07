using BidingManagementSystem.Domain.Interfaces;
using MediatR;
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

		public UpdateTenderCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
				var updatedTender = new Domain.Models.Tender
				{
					TenderId = request.tenderId,
					ReferenceNumber = request.tenderDto.ReferenceNumber,
					Title = request.tenderDto.Title,
					Description = request.tenderDto.Description,
					DeadLine = request.tenderDto.DeadLine,
					Budget = request.tenderDto.Budget,
					EligibilityCriteria = request.tenderDto.EligibilityCriteria,
				};
				await _unitOfWork.tenderRepository.UpdateAsync(updatedTender);
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

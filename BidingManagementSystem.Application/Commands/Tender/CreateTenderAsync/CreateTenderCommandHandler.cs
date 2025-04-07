using BidingManagementSystem.Domain.Interfaces;
using MediatR;
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

		public CreateTenderCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.tenderDto == null)
					return (false, "Tender data is required.");
				var tender = new Domain.Models.Tender
				{
					Title = request.tenderDto.Title,
					ReferenceNumber = request.tenderDto.ReferenceNumber,
					Description = request.tenderDto.Description,
					DeadLine = request.tenderDto.DeadLine,
					Budget = request.tenderDto.Budget,
					EligibilityCriteria = request.tenderDto.EligibilityCriteria,
				};
				await _unitOfWork.tenderRepository.AddAsync(tender);
				return (true, string.Empty);
			}
			catch (Exception ex)
			{
				return (false, $"Error creating tender: {ex.Message}");
			}
		}
	}
}

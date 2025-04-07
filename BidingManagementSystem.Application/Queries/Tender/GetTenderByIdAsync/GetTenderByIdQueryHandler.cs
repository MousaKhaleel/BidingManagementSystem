using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetTenderByIdAsync
{
	public class GetTenderByIdQueryHandler : IRequestHandler<GetTenderByIdQuery, TenderDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetTenderByIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<TenderDto> Handle(GetTenderByIdQuery request, CancellationToken cancellationToken)
		{
			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(request.tenderId);
			if (tender == null)
			{
				return null;
			}

			var tenderDto = new TenderDto
			{
				Title = tender.Title,
				ReferenceNumber = tender.ReferenceNumber,
				Description = tender.Description,
				DeadLine = tender.DeadLine,
				Budget = tender.Budget,
				EligibilityCriteria = tender.EligibilityCriteria,
			};

			return tenderDto;
		}
	}
}

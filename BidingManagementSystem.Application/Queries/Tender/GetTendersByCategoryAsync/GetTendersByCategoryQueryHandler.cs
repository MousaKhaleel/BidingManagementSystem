using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetTendersByCategoryAsync
{
	public class GetTendersByCategoryQueryHandler : IRequestHandler<GetTendersByCategoryQuery, IEnumerable<TenderDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetTendersByCategoryQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<TenderDto>> Handle(GetTendersByCategoryQuery request, CancellationToken cancellationToken)
		{
			var tenders = await _unitOfWork.tenderRepository.GetTendersByCategoryAsync(request.categoryId);
			var tenderDtos = tenders.Select(t => new TenderDto
			{
				Title = t.Title,
				ReferenceNumber = t.ReferenceNumber,
				Description = t.Description,
				DeadLine = t.DeadLine,
				Budget = t.Budget,
				EligibilityCriteria = t.EligibilityCriteria,
			}).ToList();

			return tenderDtos;
		}
	}
}

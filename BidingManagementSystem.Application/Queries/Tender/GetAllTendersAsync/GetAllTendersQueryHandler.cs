using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetAllTendersAsync
{
	public class GetAllTendersQueryHandler : IRequestHandler<GetAllTendersQuery, IEnumerable<TenderDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllTendersQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<TenderDto>> Handle(GetAllTendersQuery request, CancellationToken cancellationToken)
		{
			var tenders = await _unitOfWork.tenderRepository.GetAllAsync();
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

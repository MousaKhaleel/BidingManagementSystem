using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetOpenTendersAsync
{
	public class GetOpenTendersQueryHandler : IRequestHandler<GetOpenTendersQuery, IEnumerable<TenderDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetOpenTendersQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<TenderDto>> Handle(GetOpenTendersQuery request, CancellationToken cancellationToken)
		{
			var openTenders = await _unitOfWork.tenderRepository.GetOpenTendersAsync();
			var openTendersDtos = openTenders.Select(t => new TenderDto
			{
				Title = t.Title,
				ReferenceNumber = t.ReferenceNumber,
				Description = t.Description,
				DeadLine = t.DeadLine,
				Budget = t.Budget,
				EligibilityCriteria = t.EligibilityCriteria,
			}).ToList();
			return openTendersDtos;
		}
	}
}

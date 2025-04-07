using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Tender.GetTenderDocumentsAsync
{
	public class GetTenderDocumentsQueryHandler : IRequestHandler<GetTenderDocumentsQuery, IEnumerable<TenderDocumentDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetTenderDocumentsQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<TenderDocumentDto>> Handle(GetTenderDocumentsQuery request, CancellationToken cancellationToken)
		{
			var documents = await _unitOfWork.tenderDocumentRepository.GetTenderDocumentsAsync(request.tenderId);
			var documentDtos = documents.Select(d => new TenderDocumentDto
			{
				TenderId = d.TenderId,
				DocumentName = d.DocumentName,
				DocumentPath = d.DocumentPath,
			}).ToList();

			return documentDtos;
		}
	}
}

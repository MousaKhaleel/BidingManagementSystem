using BidingManagementSystem.Application.Dtos;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Queries.Bid.GetBidDocumentsAsync
{
	public class GetBidDocumentsQueryHandler : IRequestHandler<GetBidDocumentsQuery, IEnumerable<BidDocumentDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetBidDocumentsQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<BidDocumentDto>> Handle(GetBidDocumentsQuery request, CancellationToken cancellationToken)
		{
			var documents = await _unitOfWork.bidDocumentRepository.GetBidDocumentsAsync(request.bidId);
			var documentDtos = documents.Select(d => new BidDocumentDto
			{
				BidId = d.BidId,
				DocumentName = d.DocumentName,
				DocumentPath = d.DocumentPath,
			}).ToList();
			return documentDtos;
		}
	}
}

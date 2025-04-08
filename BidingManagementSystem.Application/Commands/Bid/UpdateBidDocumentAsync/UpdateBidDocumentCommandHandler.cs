using BidingManagementSystem.Application.Commands.Tender.UpdateTenderDocumentAsync;
using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UpdateBidDocumentAsync
{
	public class UpdateBidDocumentCommandHandler : IRequestHandler<UpdateBidDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateBidDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UpdateBidDocumentCommand request, CancellationToken cancellationToken)
		{
			var document = await _unitOfWork.bidDocumentRepository.GetByIdAsync(request.docId);
			if (document == null)
			{
				return (false, "Tender document not found");
			}

			var filePath = Path.Combine("BidDocuments", $"{document.BidDocumentId}_{request.file.FileName}");
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await request.file.CopyToAsync(stream);
			}

			document.DocumentPath = filePath;
			await _unitOfWork.bidDocumentRepository.UpdateAsync(document);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

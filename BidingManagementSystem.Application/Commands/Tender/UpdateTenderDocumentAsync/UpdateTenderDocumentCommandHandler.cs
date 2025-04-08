using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UpdateTenderDocumentAsync
{
	public class UpdateTenderDocumentCommandHandler : IRequestHandler<UpdateTenderDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateTenderDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UpdateTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			var document = await _unitOfWork.tenderDocumentRepository.GetByIdAsync(request.docId);
			if (document == null)
			{
				return (false, "Tender document not found");
			}
			var filePath = Path.Combine("TenderDocuments", $"{document.TenderDocumentId}_{request.file.FileName}");
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await request.file.CopyToAsync(stream);
			}

			document.DocumentPath = filePath;
			await _unitOfWork.tenderDocumentRepository.UpdateAsync(document);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

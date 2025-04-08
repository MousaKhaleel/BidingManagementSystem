using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync
{
	public class UploadTenderDocumentCommandHandler : IRequestHandler<UploadTenderDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UploadTenderDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UploadTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(request.tenderId);
			if (tender == null)
			{
				return (false, "Tender not found");
			}

			var filePath = Path.Combine("TenderDocuments", $"{tender.TenderId}_{request.file.FileName}");
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await request.file.CopyToAsync(stream);
			}

			var document = new TenderDocument
			{
				DocumentPath = filePath,
				DocumentName = request.file.FileName,
				TenderId = request.tenderId
			};

			await _unitOfWork.tenderDocumentRepository.AddAsync(document);
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

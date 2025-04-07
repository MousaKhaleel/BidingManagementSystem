using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.DeleteTenderDocumentAsync
{
	public class DeleteTenderDocumentCommandHandler : IRequestHandler<DeleteTenderDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteTenderDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(DeleteTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			var tenderDocument = await _unitOfWork.tenderDocumentRepository.GetByIdAsync(request.docId);
			if (tenderDocument == null)
			{
				return (false, "Tender document not found");
			}

			await _unitOfWork.tenderDocumentRepository.DeleteAsync(tenderDocument);
			await _unitOfWork.SaveChangesAsync();

			return (true, string.Empty);
		}
	}
}

using BidingManagementSystem.Domain.Interfaces;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.DeleteBidDocumentAsync
{
	public class DeleteBidDocumentCommandHandler : IRequestHandler<DeleteBidDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteBidDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool Success, string ErrorMessage)> Handle(DeleteBidDocumentCommand request, CancellationToken cancellationToken)
		{
			var bidDocument = await _unitOfWork.bidDocumentRepository.GetByIdAsync(request.docId);

			if (bidDocument == null)
			{
				return (false, "Bid document not found");
			}

			if (File.Exists(bidDocument.DocumentPath))
			{
				try
				{
					File.Delete(bidDocument.DocumentPath);
				}
				catch (Exception ex)
				{
					return (false, $"Error deleting file: {ex.Message}");
				}
			}
			else
			{
				return (false, "Document file not found on the server");
			}

			await _unitOfWork.bidDocumentRepository.DeleteAsync(bidDocument);
			await _unitOfWork.SaveChangesAsync();

			return (true, string.Empty);
		}
	}
}

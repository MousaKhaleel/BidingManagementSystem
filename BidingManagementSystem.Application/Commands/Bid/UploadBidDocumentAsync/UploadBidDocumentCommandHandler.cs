using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Bid.UploadBidDocumentAsync
{
	public class UploadBidDocumentCommandHandler : IRequestHandler<UploadBidDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UploadBidDocumentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UploadBidDocumentCommand request, CancellationToken cancellationToken)
		{//TODO
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return (false, "Bid not found");
			}

			foreach (var file in request.files)
			{
				if (file.Length > 0)
				{
					var filePath = Path.Combine("uploads", file.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}
					var bidDocument = new BidDocument
					{
						BidId = request.bidId,
						DocumentName = file.FileName,
						DocumentPath = filePath,
					};
					await _unitOfWork.bidDocumentRepository.AddAsync(bidDocument);
				}
			}

			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

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
		{
			var bid = await _unitOfWork.bidRepository.GetByIdAsync(request.bidId);
			if (bid == null)
			{
				return (false, "Bid not found");
			}

			foreach (var file in request.files)
			{
				if (file.Length > 0)
				{
					// Create the directory if it doesn't exist
					var directoryPath = Path.Combine("BidDocuments");
					if (!Directory.Exists(directoryPath))
					{
						Directory.CreateDirectory(directoryPath);
					}

					// Construct the file path
					var filePath = Path.Combine(directoryPath, $"{bid.BidId}_{file.FileName}");

					// Save the file to the specified path
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}

					// Create a new BidDocument object and save it to the database
					var bidDocument = new BidDocument
					{
						BidId = request.bidId,
						DocumentName = file.FileName,
						DocumentPath = filePath,
					};
					await _unitOfWork.bidDocumentRepository.AddAsync(bidDocument);
				}
			}

			// Save changes to the database
			await _unitOfWork.SaveChangesAsync();

			return (true, null);
		}
	}
}

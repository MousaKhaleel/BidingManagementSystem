using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync
{
	public class UploadTenderDocumentCommandHandler : IRequestHandler<UploadTenderDocumentCommand, (bool Success, string ErrorMessage)>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public UploadTenderDocumentCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public async Task<(bool Success, string ErrorMessage)> Handle(UploadTenderDocumentCommand request, CancellationToken cancellationToken)
		{
			var tender = await _unitOfWork.tenderRepository.GetByIdAsync(request.tenderId);
			if (tender == null)
			{
				return (false, "Tender not found");
			}
			var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			if (tender.UserId != userId)
			{
				return (false, "You are not authorized to upload the document.");
			}
			var directoryPath = Path.Combine("TenderDocuments");

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			var filePath = Path.Combine(directoryPath, $"{tender.TenderId}_{request.file.FileName}");
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

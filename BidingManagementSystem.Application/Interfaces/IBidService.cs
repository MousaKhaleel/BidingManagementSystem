using BidingManagementSystem.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Interfaces
{
	public interface IBidService
	{
		Task<(bool Success, string ErrorMessage)> SubmitBidAsync(int bidderId, BidDto bidDto);
		Task<IEnumerable<BidDto>> GetBidsByTenderIdAsync(int tenderId);
		Task<BidDto> GetBidByIdAsync(int tenderId, int bidId);
		Task<(bool Success, string ErrorMessage)> UpdateBidAsync(int tenderId, int bidId, BidDto bidDto);
		Task<(bool Success, string ErrorMessage)> DeleteBidAsync(int tenderId, int bidId);
		Task<(bool Success, string ErrorMessage)> UploadBidDocumentsAsync(int tenderId, int bidId, IFormFileCollection files);
		Task<BidDocumentDto> GetBidDocumentsAsync(int tenderId, int bidId);
		Task<(bool Success, string ErrorMessage)> DeleteBidDocumentAsync(int tenderId, int bidId, int docId);
	}
}

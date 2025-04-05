using BidingManagementSystem.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Interfaces
{
	public interface ITenderService
	{
		Task<(bool Success, string ErrorMessage)> CreateTenderAsync(TenderDto tenderDto);
		Task<IEnumerable<TenderDto>> GetAllTendersAsync();
		Task<IEnumerable<TenderDto>> GetOpenTendersAsync();
		Task<TenderDto> GetTenderByIdAsync(int id);
		Task<(bool Success, string ErrorMessage)> UpdateTenderAsync(int tenderId, TenderDto tenderDto);
		Task<(bool Success, string ErrorMessage)> DeleteTenderAsync(int tenderId);
		Task<(bool Success, string ErrorMessage)> GetTendersByCategoryAsync(string category);
		Task<(bool Success, string ErrorMessage)> UploadTenderDocumentAsync(int tenderId, IFormFile file);
		Task<TenderDocumentDto> GetTenderDocumentsAsync(int tenderId);
		Task<(bool Success, string ErrorMessage)> DeleteTenderDocumentAsync(int tenderId, int docId);
		//Task GetTenderCategoriesAsync();
	}
}

using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Repositories
{
	public class TenderDocumentRepository : GenericRepository<TenderDocument>, ITenderDocumentRepository
	{
		private readonly ApplicationDbContext _context;

		public TenderDocumentRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TenderDocument>> GetTenderDocumentsAsync(int tenderId)
		{
			return await _context.TenderDocuments.Where(x=> x.TenderId == tenderId).ToListAsync();
		}
	}
}

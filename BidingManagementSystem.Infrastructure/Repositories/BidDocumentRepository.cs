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
	public class BidDocumentRepository : GenericRepository<BidDocument>, IBidDocumentRepository
	{
		private readonly ApplicationDbContext _context;

		public BidDocumentRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<BidDocument>> GetBidDocumentsAsync(int bidId)
		{
			return await _context.BidDocuments.Where(x => x.BidId == bidId).ToListAsync();
		}
	}
}

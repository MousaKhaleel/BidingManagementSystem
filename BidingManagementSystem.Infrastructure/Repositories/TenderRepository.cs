using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Domain.Models.Enums;
using BidingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Repositories
{
	public class TenderRepository : GenericRepository<Tender>, ITenderRepository
	{
		private readonly ApplicationDbContext _context;

		public TenderRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Tender>> GetOpenTendersAsync()
		{
			return await _context.Tenders.Where(x=>x.Status == TenderStatus.Open).ToListAsync();
		}

		public async Task<Tender> GetTenderByIdAsync(int id)
		{
			return await _context.Tenders
						.Include(t => t.Documents)
						.Include(t => t.Bids)
						.ThenInclude(b => b.Documents)
						.FirstOrDefaultAsync(t => t.TenderId == id);
		}

		public async Task<IEnumerable<Tender>> GetTendersByCategoryAsync(int categoryId)
		{
			return await _context.Tenders
						.Include(t => t.Documents)
						.Include(t => t.Bids)
						.ThenInclude(b => b.Documents)
						.Where(t => t.TenderCategories.Any(tc => tc.CategoryId == categoryId))
						.ToListAsync();
		}
	}
}

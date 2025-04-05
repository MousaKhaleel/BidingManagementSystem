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
	internal class TenderRepository : ITenderRepository
	{
		private readonly ApplicationDbContext _context;

		public TenderRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Tender> GetTenderByIdAsync(int id)
		{
			return await _context.Tenders
						.Include(t => t.Documents)
						.Include(t => t.Bids)
						.ThenInclude(b => b.Documents)
						.FirstOrDefaultAsync(t => t.TenderId == id);
		}
	}
}

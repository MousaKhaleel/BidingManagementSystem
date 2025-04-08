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
	public class BidRepository : GenericRepository<Bid>, IBidRepository
	{
		private readonly ApplicationDbContext _context;

		public BidRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Bid> GetAwardedBidAsync(int tenderId)
		{
			var award = await _context.Awards.Where(x => x.TenderId == tenderId).FirstOrDefaultAsync();
			return await _context.Bids.Where(x => x.BidId == award.WinningBidId).FirstOrDefaultAsync();
		}

		public async Task<Bid> GetBidByIdAsync(int id)
		{
			return await _context.Bids.Include(x => x.Tender).Include(b => b.Documents).Include(b => b.Bidder).FirstOrDefaultAsync(x => x.BidId == id);
		}

		public async Task<IEnumerable<Bid>> GetBidsByTenderIdAsync(int tenderId)
		{
			return await _context.Bids.Include(x => x.Tender).Include(b => b.Documents).Include(b => b.Bidder).Where(b => b.TenderId == tenderId).ToListAsync();
		}

		public async Task<Bid> GetLowestBidByTenderIdAsync(int tenderId)
		{
			return await _context.Bids
								.Include(b => b.Documents)
								.Include(b => b.Bidder)
								.Where(x => x.TenderId == tenderId)
								.OrderBy(x => x.Amount)
								.FirstOrDefaultAsync();
		}
	}
}

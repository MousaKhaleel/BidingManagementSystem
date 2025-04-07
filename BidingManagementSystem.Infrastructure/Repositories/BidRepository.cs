using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Infrastructure.Data;
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
		//TODO
		public Task<bool> AwardBidAsync(int tenderId, int bidId)
		{
			throw new NotImplementedException();
		}

		public Task<Bid> GetAwardedBidAsync(int tenderId)
		{
			throw new NotImplementedException();
		}

		public Task<List<BidDocument>> GetBidDocumentsAsync(int bidId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<object>> GetBidsByTenderIdAsync(int tenderId)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Bid>> IBidRepository.GetBidsByTenderIdAsync(int tenderId)
		{
			throw new NotImplementedException();
		}
	}
}

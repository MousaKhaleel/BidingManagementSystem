using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface IBidRepository : IGenericRepository<Bid>
	{
		Task<bool> AwardBidAsync(int tenderId, int bidId);
		Task<Bid> GetAwardedBidAsync(int tenderId);
		Task<List<BidDocument>> GetBidDocumentsAsync(int bidId);
		Task<IEnumerable<Bid>> GetBidsByTenderIdAsync(int tenderId);
	}
}

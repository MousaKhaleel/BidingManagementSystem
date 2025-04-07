using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface IBidDocumentRepository : IGenericRepository<BidDocument>
	{
		Task<IEnumerable<BidDocument>> GetBidDocumentsAsync(int bidId);
	}
}

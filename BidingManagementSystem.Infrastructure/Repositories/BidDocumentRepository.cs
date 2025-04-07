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
	public class BidDocumentRepository : GenericRepository<BidDocument>, IBidDocumentRepository
	{
		private readonly ApplicationDbContext _context;

		public BidDocumentRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public Task<IEnumerable<BidDocument>> GetBidDocumentsAsync(int bidId)
		{
			throw new NotImplementedException();
		}
	}
}

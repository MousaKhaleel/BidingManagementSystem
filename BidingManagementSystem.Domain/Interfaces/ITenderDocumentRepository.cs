using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface ITenderDocumentRepository : IGenericRepository<TenderDocument>
	{
		Task<IEnumerable<TenderDocument>> GetTenderDocumentsAsync(int tenderId);
	}
}

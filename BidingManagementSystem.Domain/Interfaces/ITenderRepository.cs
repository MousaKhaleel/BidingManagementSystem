using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface ITenderRepository
	{
		Task<Tender> GetTenderByIdAsync(int id);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class Award
    {
		public int AwardId { get; set; }
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		public int WinningBidId { get; set; }
		public Bid WinningBid { get; set; }
	}
}

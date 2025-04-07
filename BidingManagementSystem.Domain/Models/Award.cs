using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class Award
    {
		public int AwardId { get; set; }
		[ForeignKey("TenderId")]
		public int TenderId { get; set; }
		public Tender Tender { get; set; }

		[ForeignKey("WinningBidId")]
		public int WinningBidId { get; set; }
		public Bid WinningBid { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidingManagementSystem.Domain.Models.Enums;

namespace BidingManagementSystem.Domain.Models
{
	public class Bid
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BidId { get; set; }
		public decimal Amount { get; set; }
		public DateTime CreateDate { get; set; }
		public Status Status { get; set; } = Status.UnderReview;
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		public string BidderId { get; set; }
		public User Bidder { get; set; }
		public List<BidDocument> Documents { get; set; }

	}
}

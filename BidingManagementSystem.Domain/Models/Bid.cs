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
		[Column(TypeName = "decimal(10,2)")]
		public decimal Amount { get; set; }
		public DateTime CreateDate { get; set; }
		public BidStatus Status { get; set; } = BidStatus.UnderReview;
		[ForeignKey("TenderId")]
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		[ForeignKey("BidderId")]
		public string BidderId { get; set; }
		public User Bidder { get; set; }
		public List<BidDocument>? Documents { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class Tender
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TenderId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime ClosingDate { get; set; }
		public string Category { get; set; }
		public decimal Budget { get; set; }
		//public string EligibilityCriteria { get; set; }
		public TenderStatus Status { get; set; } = TenderStatus.Open;
		public List<TenderDocument> Documents { get; set; }
		public List<Bid>? Bids { get; set; }
	}

	public enum TenderStatus //TODO:move
	{
		Open,
		Closed,
		UnderReview,
		Cancelled
	}

}

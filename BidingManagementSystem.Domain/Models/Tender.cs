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
	public class Tender
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TenderId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ClosingDate { get; set; }
		public decimal Budget { get; set; }
		public Status Status { get; set; } = Status.Open;

		public string UserId { get; set; }
		public User User { get; set; }
		public int? EvaluationId { get; set; }
		public Evaluation? Evaluation { get; set; }

		public List<TenderCategory>? TenderCategories { get; set; }
		public List<TenderDocument> Documents { get; set; }
		public List<Bid>? Bids { get; set; }
	}
}

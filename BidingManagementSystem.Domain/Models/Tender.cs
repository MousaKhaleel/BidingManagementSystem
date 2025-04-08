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
		public string? ReferenceNumber { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime IssueDate { get; set; }
		[Required]
		public DateTime DeadLine { get; set; }
		[Column(TypeName = "decimal(10,2)")]
		public decimal? Budget { get; set; }
		public TenderStatus Status { get; set; } = TenderStatus.Open;

		public string EligibilityCriteria { get; set; }//TODO fix temp

		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("EvaluationId")]
		public int? EvaluationId { get; set; }
		public Evaluation? Evaluation { get; set; }

		public List<TenderCategory>? TenderCategories { get; set; }
		public List<TenderDocument>? Documents { get; set; }
		public List<Bid>? Bids { get; set; }
	}
}

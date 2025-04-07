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
	public class Evaluation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EvaluationId { get; set; }
		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Score { get; set; }
		public DateTime EvaluationDate { get; set; }
		public EvaluationCriteria Criteria { get; set; }

		[ForeignKey("BidId")]
		public int BidId { get; set; }
		public Bid Bid { get; set; }
		[ForeignKey("TenderId")]
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		[ForeignKey("EvaluatorId")]
		public string EvaluatorId { get; set; }
		public User Evaluator { get; set; }
	}
}

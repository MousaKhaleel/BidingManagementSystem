using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Bid> Bids { get; set; }
		public DbSet<BidDocument> BidDocuments { get; set; }
		public DbSet<Tender> Tenders { get; set; }
		public DbSet<TenderDocument> TenderDocuments { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<TenderCategory> TenderCategories { get; set; }
		public DbSet<Evaluation> Evaluations { get; set; }
		public DbSet<Award> Awards { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TenderCategory>()
				.HasKey(tc => new { tc.TenderId, tc.CategoryId });

			modelBuilder.Entity<Bid>()
			.Property(e => e.CreateDate)
			.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<BidDocument>()
						.Property(e => e.CreateDate)
						.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<Tender>()
						.Property(e => e.IssueDate)
						.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<TenderDocument>()
						.Property(e => e.CreateDate)
						.HasDefaultValueSql("GETDATE()");


			modelBuilder.Entity<Bid>()
				.HasOne(b => b.Bidder)
				.WithMany(u => u.Bids)
				.HasForeignKey(b => b.BidderId)
				.OnDelete(DeleteBehavior.Restrict); ;

			modelBuilder.Entity<Bid>()
				.HasOne(b => b.Tender)
				.WithMany(t => t.Bids)
				.HasForeignKey(b => b.TenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<TenderDocument>()
				.HasOne(td => td.Tender)
				.WithMany(t => t.Documents)
				.HasForeignKey(td => td.TenderId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<BidDocument>()
				.HasOne(bd => bd.Bid)
				.WithMany(b => b.Documents)
				.HasForeignKey(bd => bd.BidId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Tender>()
				.HasOne(t => t.Evaluation)
				.WithOne(e => e.Tender)
				.HasForeignKey<Tender>(t => t.EvaluationId);

			modelBuilder.Entity<User>().ToTable("Users");

			modelBuilder.Entity<User>().Ignore(v => v.EmailConfirmed);
			modelBuilder.Entity<User>().Ignore(v => v.SecurityStamp);
			modelBuilder.Entity<User>().Ignore(v => v.ConcurrencyStamp);
			modelBuilder.Entity<User>().Ignore(v => v.PhoneNumber);
			modelBuilder.Entity<User>().Ignore(v => v.PhoneNumberConfirmed);
			modelBuilder.Entity<User>().Ignore(v => v.TwoFactorEnabled);
			modelBuilder.Entity<User>().Ignore(v => v.LockoutEnd);
			modelBuilder.Entity<User>().Ignore(v => v.LockoutEnabled);
			modelBuilder.Entity<User>().Ignore(v => v.AccessFailedCount);
		}
	}
}

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
		public DbSet<User> Users { get; set; }
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

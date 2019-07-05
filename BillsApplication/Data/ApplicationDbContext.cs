using System;
using System.Collections.Generic;
using System.Text;
using BillsData;
using BillsData.Enums;
using BillsData.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static BillsData.PaymentType;

namespace BillsApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<TransactionElement> TransactionElements { get; set; }
        public DbSet<TransactionTag> TransactionTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionTag>()
                .HasKey(x => new { x.TransactionId, x.TagId });

            modelBuilder
               .Entity<PaymentType>()
               .HasData( EntitiesFromEnum
               .BuildEntityObjectsFromEnum<PaymentTypeEnum>());
            modelBuilder
              .Entity<Unit>()
              .HasData(EntitiesFromEnum
              .BuildEntityObjectsFromEnum<UnitEnum>());
        }
    }
}

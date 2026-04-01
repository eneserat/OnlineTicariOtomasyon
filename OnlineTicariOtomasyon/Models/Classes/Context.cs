using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Context : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoicePen> InvoicePens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesTransaction> SalesTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product-Category ilişkisi
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .WillCascadeOnDelete(false);

            // Employee-Department ilişkisi
            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Department)
                .WithMany(d => d.Employees)
                .WillCascadeOnDelete(false);

            // SalesTransaction-Current ilişkisi
            modelBuilder.Entity<SalesTransaction>()
                .HasRequired(s => s.Current)
                .WithMany(c => c.SalesTransactions)
                .HasForeignKey(s => s.CurrentID)
                .WillCascadeOnDelete(false);

            // SalesTransaction-Employee ilişkisi
            modelBuilder.Entity<SalesTransaction>()
                .HasRequired(s => s.Employee)
                .WithMany(e => e.SalesTransactions)
                .HasForeignKey(s => s.EmployeeID)
                .WillCascadeOnDelete(false);

            // SalesTransaction-Product ilişkisi
            modelBuilder.Entity<SalesTransaction>()
                .HasRequired(s => s.Product)
                .WithMany(p => p.SalesTransactions)
                .HasForeignKey(s => s.ProductID)
                .WillCascadeOnDelete(false);

            // InvoicePen-Invoice ilişkisi
            modelBuilder.Entity<InvoicePen>()
                .HasRequired(ip => ip.Invoice)  
                .WithMany(i => i.InvoicePens)
                .WillCascadeOnDelete(false);

        }
    }
}
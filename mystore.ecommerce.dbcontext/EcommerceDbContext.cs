﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace mystore.ecommerce.dbcontext.Models
{
    public partial class EcommercedbContext : DbContext
    {
        public EcommercedbContext()
        {
        }

        public EcommercedbContext(DbContextOptions<EcommercedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(builder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Customer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
      
    }
}
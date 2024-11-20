﻿using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData([
                    new() { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 10 },
                    new() { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 15 }
                ]);
        }
    }
}
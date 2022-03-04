﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes>ProductTypes { get; set; }

        public DbSet<SpecialTag> SpecialTags { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Orders>orders { get; set; }

        public DbSet<OrderDetails>orderDetails { get; set; }

    }
}
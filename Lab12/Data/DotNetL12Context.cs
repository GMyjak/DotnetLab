using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab12.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab12.Data
{
    public class DotNetL12Context : IdentityDbContext
    {
        public DotNetL12Context (DbContextOptions<DotNetL12Context> options)
            : base(options)
        {
        }

        public DbSet<Lab12.Models.Category> Category { get; set; }

        public DbSet<Lab12.Models.Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

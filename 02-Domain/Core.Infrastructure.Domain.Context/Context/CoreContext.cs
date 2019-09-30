using System;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Domain.Context.Context
{
    public class CoreContext : IdentityDbContext<IdentityUser>
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefType> RefType { get; set; }
        public virtual DbSet<RefValue> RefValue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=TRISTICTL46W10;Initial Catalog=Core.Infrastructure; Persist Security Info=True;User ID=sa;Password=User123**;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibrarySystem.Data.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Member> Members { get; set; } = default!;
        public DbSet<Loan> Loans { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source=library.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Member>().HasKey(m => m.MemberId);

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.HasOne(l => l.Book)
                      .WithMany(b => b.Loans)
                      .HasForeignKey(l => l.BookId);

                entity.HasOne(l => l.Member)
                      .WithMany() // byt till .WithMany(m => m.Loans) om du lägger till Loans i Member
                      .HasForeignKey(l => l.MemberId)
                      .HasPrincipalKey(m => m.MemberId);
            });
        }
    }
}
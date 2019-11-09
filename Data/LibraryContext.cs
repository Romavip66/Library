using LibraryCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Queue> Queue { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Library_Card> Library_Card { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //User to Library Card One to One
            modelBuilder.Entity<User>()
                .HasOne(p => p.Library_Card)
                .WithOne(i => i.User)
                .HasForeignKey<Library_Card>(b => b.UserId);
          //User to Requests One to One  
            modelBuilder.Entity<User>()
                  .HasOne(p => p.User_Requests)
                  .WithOne(i => i.User)
                  .HasForeignKey<Requests>(b => b.UserId);
          //User to Roles One to Many  
            modelBuilder.Entity<User>()
                  .HasOne(p => p.User_Role)
                  .WithMany(i => i.User);
            //Book to Queue, Card to Queue Many to Many
            modelBuilder.Entity<Queue>()
           .HasKey(pt => new { pt.CardId, pt.BookId });

            modelBuilder.Entity<Queue>()
                .HasOne(pt => pt.Library_Card)
                .WithMany(p => p.Queue)
                .HasForeignKey(pt => pt.CardId);

            modelBuilder.Entity<Queue>()
                .HasOne(pt => pt.Books)
                .WithMany(t => t.Queue)
                .HasForeignKey(pt => pt.BookId);
            //History Many to Many
            modelBuilder.Entity<History>()
           .HasKey(pt => new { pt.CardId, pt.BookId });

            modelBuilder.Entity<History>()
                .HasOne(pt => pt.Library_Card)
                .WithMany(p => p.History)
                .HasForeignKey(pt => pt.CardId);

            modelBuilder.Entity<History>()
                .HasOne(pt => pt.Books)
                .WithMany(t => t.History)
                .HasForeignKey(pt => pt.BookId);

        }



    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_bookss.Data.Models;

namespace my_bookss.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        //this file will be as bridge between c# and db

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //configure the many to many relationship book author
        //define the relation between book and bookauthor 
        //and author with bookauthor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
              .HasOne(b => b.Book)
              .WithMany(ba => ba.Book_Authors)
              .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
              .HasOne(b => b.Author)
              .WithMany(ba => ba.Book_Authors)
              .HasForeignKey(bi => bi.AuthorId);

            
            base.OnModelCreating(modelBuilder);

        }
       
        //define table names for c# model

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
    }
}

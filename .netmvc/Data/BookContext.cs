using Microsoft.EntityFrameworkCore;

namespace _netmvc.Models
{
    public class BookContext : DbContext
    {
        public BookContext(){}
        public BookContext(DbContextOptions<BookContext> options) : base(options){}

        public DbSet<Book> Books{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Blogging;Trusted_Connection=True;");
        }
    }
}
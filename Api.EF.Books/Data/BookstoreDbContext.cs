using Api.EF.Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.EF.Books.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}

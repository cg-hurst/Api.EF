using Api.EF.Books.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.EF.Books.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

        public DbSet<BookData> Books { get; set; }

        public DbSet<AuthorData> Authors { get; set; }
    }
}

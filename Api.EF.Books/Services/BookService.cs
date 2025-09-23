using Api.EF.Books.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.EF.Books.Services;

public class BookService
{
    public BookService(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly BookstoreDbContext _dbContext;

    public async Task<IEnumerable<Models.Book>> GetAllBooksAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public async Task<Models.Book?> GetBookByIdAsync(int id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<bool> AddBookAsync(Models.Book book)
    {
        var newBook = await _dbContext.Books.AddAsync(book);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateBookAsync(Models.Book book)
    {
        var updated = _dbContext.Books.Update(book);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }
        _dbContext.Books.Remove(book);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}

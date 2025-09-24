using Api.EF.Books.Data;
using Api.EF.Books.Data.Models;
using Api.EF.Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.EF.Books.Services;

public class BookService
{
    public BookService(BookstoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly BookstoreDbContext _dbContext;

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .Select(b => MapBookDataToBook(b)!)
            .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        var book = await _dbContext.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        return MapBookDataToBook(book);
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        // Map the model to the EF data model
        var bookData = MapBookToBookData(book);

        // Check if the author exists
        var existingAuthor = await _dbContext.Authors.FindAsync(bookData.AuthorId);
        if (existingAuthor == null)
        {
            // Add the author if it doesn't exist
            _dbContext.Authors.Add(bookData.Author);
        }
        else
        {
            // Associate the book with the existing author
            bookData.Author = existingAuthor;
            bookData.AuthorId = existingAuthor.Id;
        }

        // Add the book
        _dbContext.Books.Add(bookData);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        // Map the model to the EF data model
        var bookData = MapBookToBookData(book);

        // Check if the author exists
        var existingAuthor = await _dbContext.Authors.FindAsync(bookData.AuthorId);
        if (existingAuthor == null)
        {
            // Add the author if it doesn't exist
            _dbContext.Authors.Add(bookData.Author);
        }
        else
        {
            // Associate the book with the existing author
            bookData.Author = existingAuthor;
            bookData.AuthorId = existingAuthor.Id;
        }

        // Update the book
        _dbContext.Books.Update(bookData);
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

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _dbContext.Authors
            .Select(a => MapAuthorDataToAuthor(a)!)
            .ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return MapAuthorDataToAuthor(await _dbContext.Authors.FindAsync(id));
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
    {
        var books = await _dbContext.Books
            .Include(a => a.Author)
            .Where(b => b.AuthorId == authorId)
            .Select(b => MapBookDataToBook(b))
            .ToListAsync();

        return books.Where(b => b != null)!;
    }

    public async Task<bool> AddAuthorAsync(Author author)
    {
        // Map the DTO to the data model
        var authorData = new AuthorData
        {
            Id = author.Id,
            Name = author.Name
        };
        // Add the author
        _dbContext.Authors.Add(authorData);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAuthorAsync(Author author)
    {
        // Map the DTO to the data model
        var authorData = new AuthorData
        {
            Id = author.Id,
            Name = author.Name
        };
        // Update the author
        _dbContext.Authors.Update(authorData);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author == null)
        {
            return false;
        }
        _dbContext.Authors.Remove(author);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    private static Author? MapAuthorDataToAuthor(AuthorData? authorData)
    {
        if (authorData == null)
        {
            return null;
        }

        return new Author
        {
            Id = authorData.Id,
            Name = authorData.Name
        };
    }

    private static Book? MapBookDataToBook(BookData? bookData)
    {
        if (bookData == null)
        {
            return null;
        }

        return new Book
        {
            Id = bookData.Id,
            Title = bookData.Title,
            YearPublished = bookData.YearPublished,
            Genre = bookData.Genre,
            AuthorId = bookData.AuthorId,
            AuthorName = bookData.Author?.Name ?? string.Empty
        };
    }

    private static BookData MapBookToBookData(Book book)
    {
        return new BookData
        {
            Id = book.Id,
            Title = book.Title,
            YearPublished = book.YearPublished,
            Genre = book.Genre,
            AuthorId = book.AuthorId,
            Author = new AuthorData { Id = book.AuthorId, Name = book.AuthorName }
        };
    }
}

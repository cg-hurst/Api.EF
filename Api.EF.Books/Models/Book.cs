namespace Api.EF.Books.Models;

public record Book(int Id, string Title, string Author, int YearPublished, string Genre);

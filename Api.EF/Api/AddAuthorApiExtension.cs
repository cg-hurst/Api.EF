using Api.EF.Books.Data.Models;
using Api.EF.Books.Models;
using Api.EF.Books.Services;

namespace Api.EF.Api;

public static class AddAuthorApiExtension
{
    public static WebApplication AddAuthorApi(this WebApplication app)
    {
        app
            .MapGet("/authors", async (BookService service) =>
            {
                var Authors = await service.GetAllAuthorsAsync();
                return Results.Ok(Authors);
            })
            .WithName("GetAllAuthors");

        app
            .MapGet("/authors/{id:int}", async (int id, BookService service) =>
            {
                var Author = await service.GetAuthorByIdAsync(id);
                return Author is not null ? Results.Ok(Author) : Results.NotFound();
            })
            .WithName("GetAuthorById");

        app
            .MapPost("/authors", async (Author Author, BookService service) =>
            {
                var added = await service.AddAuthorAsync(Author);
                return added ? Results.Created($"/Authors/{Author.Id}", Author) : Results.Conflict("A Author with the same ID already exists.");
            })
            .WithName("AddAuthor");

        app
            .MapPut("/authors", async (Author updatedAuthor, BookService service) =>
            {
                var updated = await service.UpdateAuthorAsync(updatedAuthor);
                return updated ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateAuthor");

        app
            .MapDelete("/authors/{id:int}", async (int id, BookService service) =>
            {
                var deleted = await service.DeleteAuthorAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteAuthor");

        return app;
    }
}

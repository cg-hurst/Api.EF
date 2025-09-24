using System.ComponentModel.DataAnnotations.Schema;

namespace Api.EF.Books.Data.Models;

[Table("Books")]
public class BookData
{
    public required int Id { get; set; }
    public required string Title { get; set; } 
    public required int AuthorId { get; set; }
    public required AuthorData Author { get; set; }
    public required int YearPublished { get; set; }
    public required string Genre { get; set; } 
}

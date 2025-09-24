using System.ComponentModel.DataAnnotations.Schema;

namespace Api.EF.Books.Data.Models;

[Table("Authors")]
public class AuthorData
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<BookData> Books { get; set; } = new List<BookData>();
}
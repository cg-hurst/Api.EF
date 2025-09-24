namespace Api.EF.Books.Models
{
    public class Book
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required int AuthorId { get; set; }
        public required string AuthorName { get; set; }
        public required int YearPublished { get; set; }
        public required string Genre { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.EF.Books.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDefaultBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Books"" (""Title"", ""Author"", ""YearPublished"", ""Genre"") VALUES
                ('To Kill a Mockingbird', 'Harper Lee', 1960, 'Fiction'),
                ('1984', 'George Orwell', 1949, 'Dystopian'),
                ('The Great Gatsby', 'F. Scott Fitzgerald', 1925, 'Classic'),
                ('Moby Dick', 'Herman Melville', 1851, 'Adventure'),
                ('Pride and Prejudice', 'Jane Austen', 1813, 'Romance'),
                ('The Catcher in the Rye', 'J.D. Salinger', 1951, 'Fiction'),
                ('The Hobbit', 'J.R.R. Tolkien', 1937, 'Fantasy'),
                ('War and Peace', 'Leo Tolstoy', 1869, 'Historical'),
                ('Crime and Punishment', 'Fyodor Dostoevsky', 1866, 'Psychological'),
                ('The Lord of the Rings', 'J.R.R. Tolkien', 1954, 'Fantasy');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Books\" WHERE Id BETWEEN 1 AND 10;");
        }
    }
}

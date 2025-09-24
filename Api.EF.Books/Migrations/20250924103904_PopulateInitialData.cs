using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.EF.Books.Migrations
{
    /// <inheritdoc />
    public partial class PopulateInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Authors"" (""Name"") VALUES
                ('George Orwell'),
                ('Harper Lee'),
                ('J.R.R. Tolkien'),
                ('F. Scott Fitzgerald'),
                ('Jane Austen');
                INSERT INTO ""Books"" (""Title"", ""AuthorId"", ""YearPublished"", ""Genre"") VALUES
                ('1984', 1, 1949, 'Dystopian'),
                ('To Kill a Mockingbird', 2, 1960, 'Fiction'),
                ('The Hobbit', 3, 1937, 'Fantasy'),
                ('The Great Gatsby', 4, 1925, 'Fiction'),
                ('Pride and Prejudice', 5, 1813, 'Romance');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""Books"" WHERE ""Title"" IN 
                ('1984', 'To Kill a Mockingbird', 'The Hobbit', 'The Great Gatsby', 'Pride and Prejudice');
                DELETE FROM ""Authors"" WHERE ""Name"" IN 
                ('George Orwell', 'Harper Lee', 'J.R.R. Tolkien', 'F. Scott Fitzgerald', 'Jane Austen');
            ");
        }
    }
}

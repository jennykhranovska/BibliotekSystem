using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Tests
{
    public class LibraryContextTests
    {
        private LibraryContext CreateContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddBook_ShouldSaveBookInDatabase()
        {
            // Arrange
            using var context = CreateContext(nameof(AddBook_ShouldSaveBookInDatabase));

            var book = new Book
            {
                ISBN = "111",
                Title = "Test Book",
                Author = "Test Author",
                PublishedYear = 2024
            };

            // Act
            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Assert
            var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "111");

            Assert.NotNull(savedBook);
            Assert.Equal("Test Book", savedBook.Title);
        }

        [Fact]
        public async Task GetBooks_ShouldReturnBooksFromDatabase()
        {
            // Arrange
            using var context = CreateContext(nameof(GetBooks_ShouldReturnBooksFromDatabase));

            context.Books.AddRange(
                new Book
                {
                    ISBN = "111",
                    Title = "Book One",
                    Author = "Author One",
                    PublishedYear = 2020
                },
                new Book
                {
                    ISBN = "222",
                    Title = "Book Two",
                    Author = "Author Two",
                    PublishedYear = 2021
                });

            await context.SaveChangesAsync();

            // Act
            var books = await context.Books.ToListAsync();

            // Assert
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task RemoveBook_ShouldDeleteBookFromDatabase()
        {
            // Arrange
            using var context = CreateContext(nameof(RemoveBook_ShouldDeleteBookFromDatabase));

            var book = new Book
            {
                ISBN = "333",
                Title = "Delete Me",
                Author = "Author",
                PublishedYear = 2023
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            // Act
            context.Books.Remove(book);
            await context.SaveChangesAsync();

            // Assert
            var deletedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "333");
            Assert.Null(deletedBook);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BookRepositoryIntegrationTests
    {
        private LibraryContext CreateContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddAsync_ThenGetAllAsync_ShouldReturnSavedBook()
        {
            // Arrange
            using var context = CreateContext(nameof(AddAsync_ThenGetAllAsync_ShouldReturnSavedBook));
            var repository = new BookRepository(context);

            var book = new Book
            {
                ISBN = "100",
                Title = "Integration Test Book",
                Author = "Test Author",
                PublishedYear = 2024
            };

            // Act
            await repository.AddAsync(book);
            var books = await repository.GetAllAsync();

            // Assert
            Assert.Single(books);
            Assert.Equal("100", books.First().ISBN);
            Assert.Equal("Integration Test Book", books.First().Title);
        }

        [Fact]
        public async Task AddAsync_ThenSearchAsync_ShouldFindBookByTitle()
        {
            // Arrange
            using var context = CreateContext(nameof(AddAsync_ThenSearchAsync_ShouldFindBookByTitle));
            var repository = new BookRepository(context);

            await repository.AddAsync(new Book
            {
                ISBN = "101",
                Title = "CSharp Basics",
                Author = "Anna",
                PublishedYear = 2023
            });

            await repository.AddAsync(new Book
            {
                ISBN = "102",
                Title = "Java Basics",
                Author = "Erik",
                PublishedYear = 2022
            });

            // Act
            var result = await repository.SearchAsync("CSharp");

            // Assert
            Assert.Single(result);
            Assert.Equal("101", result.First().ISBN);
            Assert.Equal("CSharp Basics", result.First().Title);
        }

        [Fact]
        public async Task AddMultipleBooks_ThenGetAllAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            using var context = CreateContext(nameof(AddMultipleBooks_ThenGetAllAsync_ShouldReturnCorrectCount));
            var repository = new BookRepository(context);

            await repository.AddAsync(new Book
            {
                ISBN = "201",
                Title = "Book One",
                Author = "Author One",
                PublishedYear = 2020
            });

            await repository.AddAsync(new Book
            {
                ISBN = "202",
                Title = "Book Two",
                Author = "Author Two",
                PublishedYear = 2021
            });

            await repository.AddAsync(new Book
            {
                ISBN = "203",
                Title = "Book Three",
                Author = "Author Three",
                PublishedYear = 2022
            });

            // Act
            var books = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, books.Count());
        }
    }
}
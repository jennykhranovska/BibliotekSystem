using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class BookRepositoryTests
{
    private LibraryContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new LibraryContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldSaveBookToDatabase()
    {
        // Arrange
        using var context = CreateContext(nameof(AddAsync_ShouldSaveBookToDatabase));
        var repository = new BookRepository(context);

        var book = new Book
        {
            ISBN = "123",
            Title = "Test",
            Author = "Author",
            PublishedYear = 2024
        };

        // Act
        await repository.AddAsync(book);

        // Assert
        var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");

        Assert.NotNull(savedBook);
        Assert.Equal("Test", savedBook!.Title);
        Assert.Equal("Author", savedBook.Author);
        Assert.Equal(2024, savedBook.PublishedYear);
    }

    [Fact]
    public async Task SearchAsync_ShouldFindBooksByTitle()
    {
        // Arrange
        using var context = CreateContext(nameof(SearchAsync_ShouldFindBooksByTitle));
        var repository = new BookRepository(context);

        context.Books.AddRange(
            new Book
            {
                ISBN = "111",
                Title = "C# för nybörjare",
                Author = "Anna Andersson",
                PublishedYear = 2020
            },
            new Book
            {
                ISBN = "222",
                Title = "Java Programming",
                Author = "John Smith",
                PublishedYear = 2021
            });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.SearchAsync("C#");

        // Assert
        Assert.Single(result);
        Assert.Equal("111", result.First().ISBN);
        Assert.Equal("C# för nybörjare", result.First().Title);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        // Arrange
        using var context = CreateContext(nameof(GetAllAsync_ShouldReturnAllBooks));
        var repository = new BookRepository(context);

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
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, b => b.ISBN == "111");
        Assert.Contains(result, b => b.ISBN == "222");
    }
}
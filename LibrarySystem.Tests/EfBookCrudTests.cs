using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bibliotekssystem.Tests;

public class EfBookCrudTests
{
    private LibraryContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new LibraryContext(options);
    }

    [Fact]
    public async Task AddBook_ShouldPersistToDatabase()
    {
        using var context = CreateContext();
        var book = new Book("Test", "Author", 2024, "ISBN-123");

        context.Books.Add(book);
        await context.SaveChangesAsync();

        var saved = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "ISBN-123");
        Assert.NotNull(saved);
    }

    [Fact]
    public async Task DeleteBook_ShouldRemoveFromDatabase()
    {
        using var context = CreateContext();
        var book = new Book("A", "X", 2000, "1");

        context.Books.Add(book);
        await context.SaveChangesAsync();

        context.Books.Remove(book);
        await context.SaveChangesAsync();

        Assert.False(await context.Books.AnyAsync(b => b.ISBN == "1"));
    }



    [Fact]
    public async Task UpdateBook_ShouldPersistChanges()
    {
        using var context = CreateContext();
        var book = new Book("A", "X", 2000, "1");

        context.Books.Add(book);
        await context.SaveChangesAsync();

        book.MarkAsLoaned();
        await context.SaveChangesAsync();

        var reloaded = await context.Books.SingleAsync(b => b.ISBN == "1");
        Assert.False(reloaded.IsAvailable);
    }

    [Fact]
    public async Task GetAllBooks_ShouldReturnAllSavedBooks()
    {
        using var context = CreateContext();

        context.Books.Add(new Book("A", "X", 2000, "1"));
        context.Books.Add(new Book("B", "Y", 2001, "2"));
        await context.SaveChangesAsync();

        var books = await context.Books.ToListAsync();

        Assert.Equal(2, books.Count);
    }
}
using Bunit;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data.Context;
using LibrarySystem.Web.Components.Pages;

namespace Bibliotekssystem.Tests;

public class BooksPageBunitTests : TestContext
{
    [Fact]
    public void BooksPage_ShouldRender()
    {
        Services.AddDbContext<LibraryContext>(opt =>
            opt.UseInMemoryDatabase("bunit_books"));

        var cut = RenderComponent<Books>();

        Assert.Contains("Böcker", cut.Markup);
    }
}

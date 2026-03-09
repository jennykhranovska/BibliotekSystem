using Bunit;
using LibrarySystem.Web.Components.Pages;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BooksPageBunitTests
    {
        [Fact]
        public void BooksPage_ShouldRenderWithoutCrashing()
        {
            using var ctx = new BunitContext();

            var cut = ctx.Render<Books>();

            Assert.NotNull(cut);
        }

        [Fact]
        public void BooksPage_ShouldContainMarkup()
        {
            using var ctx = new BunitContext();

            var cut = ctx.Render<Books>();

            Assert.NotEmpty(cut.Markup);
        }
    }
}
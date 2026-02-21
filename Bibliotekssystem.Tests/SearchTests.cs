using Bibliotekssystem;
using Xunit;
using LibrarySystem.Core.Models;

namespace Bibliotekssystem.Tests
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]
        [InlineData("Rowling", false)]
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("Sagan om ringen", "J.R.R. Tolkien", 1954, "123");

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Member_Matches_ShouldFindByName_CaseInsensitive()
        {
            // Arrange
            var member = new Member("M001", "Anna Andersson", "anna@test.com");

            // Act & Assert
            Assert.True(member.Matches("anna"));
        }


        [Fact]
        public void SearchBooks_ShouldReturnEmptyList_WhenSearchTermIsEmpty()
        {
            var library = new Library();

            var result = library.SearchBooks("");

            Assert.Empty(result);
        }

    }
}

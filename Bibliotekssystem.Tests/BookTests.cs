using Bibliotekssystem;
using Xunit;
using LibrarySystem.Core.Models;

namespace Bibliotekssystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act 
            var book = new Book("Testbok", "Testförfattare", 2024, "978-91-0-012345-6");

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
           
        }

        [Fact]
        public void Matches_ShouldReturnTrue_WhenSearchTermMatchesTitle()
        {
            // Arrange
            var book = new Book("Sagan om ringen", "J.R.R. Tolkien", 1954, "123");

            // Act
            var result = book.Matches("ringen");

            // Assert
            Assert.True(result);
        }



        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            // Arrange
            var book = new Book("Testbok", "Testförfattare", 2024, "ISBN-1");

            // Act
            var info = book.GetInfo();

            // Assert
            Assert.Contains("Testbok", info);
            Assert.Contains("Testförfattare", info);
            Assert.Contains("2024", info);
            Assert.Contains("ISBN-1", info);
        }



        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Book("", "Author", 2020, "123"));
        }

    }
}

using Bibliotekssystem;
using Xunit;

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
        public void NewBook_ShouldNotBeLoaned_WhenNoLoansExist()
        {

            var library = new Library();
            var book = new Book("Ny bok", "Författare", 2020, "ISBN-1");
            var member = new Member("M001", "Test Person", "test@test.com");

            library.BookCatalog.AddBook(book);
            library.MemberRegistry.AddMember(member);

            // Act
            var activeLoansBefore = library.LoanManager.GetActiveLoans();

            // Assert
            Assert.Empty(activeLoansBefore);
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
    }
}

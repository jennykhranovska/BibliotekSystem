using System;
using Bibliotekssystem;
using Xunit;
using LibrarySystem.Core.Models;

namespace Bibliotekssystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("Test", "Author", 2024, "123");
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act & Assert
            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed_AndNotReturned()
        {
            // Arrange
            var book = new Book("Test", "Author", 2024, "123");
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));

            // Act & Assert
            Assert.True(loan.IsOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Arrange
            var book = new Book("Test", "Author", 2024, "123");
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act
            loan.ReturnBook();

            // Assert
            Assert.True(loan.IsReturned);
            Assert.NotNull(loan.ReturnDate);
        }



        [Fact]
        public void LoanBook_ShouldSet_IsAvailable_ToFalse()
        {
            // Arrange
            var library = new Library();
            var book = new Book("Test", "Author", 2024, "123");
            var member = new Member("M001", "Test Person", "test@test.com");

            library.BookCatalog.AddBook(book);
            library.MemberRegistry.AddMember(member);

            // Act
            library.LoanManager.LoanBook(book, member);

            // Assert
            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void ReturnBook_ShouldSet_IsAvailable_ToTrue()
        {
            // Arrange
            var library = new Library();
            var book = new Book("Test", "Author", 2024, "123");
            var member = new Member("M001", "Test Person", "test@test.com");

            library.BookCatalog.AddBook(book);
            library.MemberRegistry.AddMember(member);

            library.LoanManager.LoanBook(book, member);

            // Act
            library.LoanManager.ReturnBook("123");

            // Assert
            Assert.True(book.IsAvailable);
        }

    }

}

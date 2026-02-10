using System;
using Bibliotekssystem;
using Xunit;

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
    }
}

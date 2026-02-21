using System;
using System.Linq;
using Bibliotekssystem;
using Xunit;
using LibrarySystem.Core.Models;

namespace Bibliotekssystem.Tests
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBooksCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            library.BookCatalog.AddBook(new Book("A", "X", 2000, "1"));
            library.BookCatalog.AddBook(new Book("B", "Y", 2001, "2"));

            // Act
            var total = library.GetTotalBooksCount();

            // Assert
            Assert.Equal(2, total);
        }

        [Fact]
        public void GetLoanedBooksCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("A", "X", 2000, "1");
            var book2 = new Book("B", "Y", 2001, "2");
            var member = new Member("M001", "Test Person", "test@test.com");

            library.BookCatalog.AddBook(book1);
            library.BookCatalog.AddBook(book2);
            library.MemberRegistry.AddMember(member);

            library.LoanManager.LoanBook(book1, member); // 1 aktivt lån

            // Act
            var count = library.GetLoanedBooksCount();

            // Assert
            Assert.Equal(1, count);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            // Arrange
            var library = new Library();

            var m1 = new Member("M001", "Anna", "anna@test.com");
            var m2 = new Member("M002", "Erik", "erik@test.com");

            var b1 = new Book("A", "X", 2000, "1");
            var b2 = new Book("B", "Y", 2001, "2");
            var b3 = new Book("C", "Z", 2002, "3");

            library.MemberRegistry.AddMember(m1);
            library.MemberRegistry.AddMember(m2);

            library.BookCatalog.AddBook(b1);
            library.BookCatalog.AddBook(b2);
            library.BookCatalog.AddBook(b3);

            library.LoanManager.LoanBook(b1, m1);
            library.LoanManager.ReturnBook("1");

            library.LoanManager.LoanBook(b2, m1);
            library.LoanManager.ReturnBook("2");

            library.LoanManager.LoanBook(b3, m2); // m2 har 1 lån

            // Act
            var mostActive = library.GetMostActiveBorrower();

            // Assert
            Assert.NotNull(mostActive);
            Assert.Equal("M001", mostActive!.MemberId);
        }

        [Fact]
        public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
        {
            // Arrange
            var library = new Library();
            library.BookCatalog.AddBook(new Book("C", "X", 2000, "3"));
            library.BookCatalog.AddBook(new Book("A", "X", 2000, "1"));
            library.BookCatalog.AddBook(new Book("B", "X", 2000, "2"));

            // Act
            var sorted = library.SortBooksByTitle();

            // Assert
            Assert.Equal(new[] { "A", "B", "C" }, sorted.Select(b => b.Title).ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibrarySystem.Core.Models

{
    public class Library
    {

        public BookCatalog BookCatalog { get; }
        public MemberRegistry MemberRegistry { get; }
        public LoanManager LoanManager { get; }



        public Library()
        {
            BookCatalog = new BookCatalog();
            MemberRegistry = new MemberRegistry();
            LoanManager = new LoanManager();
        }

        public List<Book> SortBooksByTitle(bool ascending = true)
        {
            var books = BookCatalog.GetBooks();

            return ascending
                ? books.OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase).ToList()
                : books.OrderByDescending(b => b.Title, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public List<Book> SortBooksByPublishedYear(bool ascending = true)
        {
            var books = BookCatalog.GetBooks();

            return ascending
                ? books.OrderBy(b => b.PublishedYear).ToList()
                : books.OrderByDescending(b => b.PublishedYear).ToList();
        }


        public List<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Book>();

            return BookCatalog
                .GetBooks()
                .Where(b => b.Matches(searchTerm))
                .ToList();
        }


        public int GetTotalBooksCount()
        {
            return BookCatalog.GetBooks().Count;
        }


        public int GetLoanedBooksCount()
        {
            return LoanManager.GetActiveLoans().Count;
        }

        public Member? GetMostActiveBorrower()
        {
            var loans = LoanManager.GetLoans();

            if (loans.Count == 0) return null;

            return loans
                .GroupBy(l => l.Member)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
        }

    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Models
{
    public class Book : ISearchable
    {

        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public Book() { }
        public int Id { get; private set; }

        public void MarkAsLoaned() => IsAvailable = false;
        public void MarkAsReturned() => IsAvailable = true;



        public Book(string title, string author, int publishedYear, string isbn)

        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN cannot be empty.");

            if (publishedYear > DateTime.Now.Year)
                throw new ArgumentException("Published year cannot be in the future.");

            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            ISBN = isbn;

        }

        public bool Matches(string searchTerm)
        {
            return Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                || Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                || ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }


        public string GetInfo()
        {
            return $"{Title} av {Author} ({PublishedYear}) - ISBN: {ISBN}";
        }



    }


}
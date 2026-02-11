using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem
{
    public class Book : ISearchable
    {

        public string ISBN { get; }

        public string Title { get; }


        public string Author { get; }

        public int PublishedYear { get; }
        public bool IsAvailable { get; private set; } = true;

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
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





        public Book(string title, string author, int publishedYear, string isbn)

        { 
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


        public void MarkAsLoaned() => IsAvailable = false;
        public void MarkAsReturned() => IsAvailable = true;

    }
}
     
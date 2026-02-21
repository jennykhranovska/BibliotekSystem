



namespace LibrarySystem.Core.Models
{
    public class BookCatalog
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public IReadOnlyList<Book>GetBooks() {
            return books;
        }

        public Book GetBook(string isbn)
        {
            return books.FirstOrDefault(b => b.ISBN == isbn);
        }
    }
}

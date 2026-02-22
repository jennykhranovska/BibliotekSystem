using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Core.Interfaces;

namespace LibrarySystem.Core.Models
{
    public class BookCatalog
    {
        private readonly IBookRepository _repo;

        // Behåll parameterlös ctor så gamla tester/kod funkar
        public BookCatalog() : this(new InMemoryBookRepository())
        {
        }

      
        public BookCatalog(IBookRepository repo)
        {
            _repo = repo;
        }

 

        public void AddBook(Book book)
            => _repo.AddAsync(book).GetAwaiter().GetResult();

        public IReadOnlyList<Book> GetBooks()
            => _repo.GetAllAsync().GetAwaiter().GetResult().ToList();

        public Book? GetBook(string isbn)
            => _repo.GetByISBNAsync(isbn).GetAwaiter().GetResult();


        // === Minimal in-memory repo för att inte tvinga EF i tester ===
        private class InMemoryBookRepository : IBookRepository
        {
            private readonly List<Book> _books = new();

            public Task<IEnumerable<Book>> GetAllAsync()
                => Task.FromResult<IEnumerable<Book>>(_books.ToList());

            public Task<Book?> GetByIdAsync(int id)
                => Task.FromResult<Book?>(_books.FirstOrDefault(b => b.Id == id));

            public Task<Book?> GetByISBNAsync(string isbn)
                => Task.FromResult<Book?>(_books.FirstOrDefault(b => b.ISBN == isbn));

            public Task AddAsync(Book book)
            {
                _books.Add(book);
                return Task.CompletedTask;
            }

            public Task UpdateAsync(Book book)
            {
                return Task.CompletedTask;
            }

            public Task DeleteAsync(int id)
            {
                var existing = _books.FirstOrDefault(b => b.Id == id);
                if (existing != null) _books.Remove(existing);
                return Task.CompletedTask;
            }

            public Task<IEnumerable<Book>> SearchAsync(string searchTerm)
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return Task.FromResult<IEnumerable<Book>>(_books.ToList());

                var term = searchTerm.Trim();

                var result = _books.Where(b =>
                        (b.Title?.Contains(term) ?? false) ||
                        (b.Author?.Contains(term) ?? false) ||
                        (b.ISBN?.Contains(term) ?? false))
                    .ToList();

                return Task.FromResult<IEnumerable<Book>>(result);
            }
        }
    }
}
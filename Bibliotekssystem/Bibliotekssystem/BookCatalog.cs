using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Models;

public class InMemoryBookRepository : IBookRepository
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
        var existing = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existing != null)
        {
            _books.Remove(existing);
            _books.Add(book);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existing = _books.FirstOrDefault(b => b.Id == id);
        if (existing != null)
            _books.Remove(existing);

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

    public Task<IEnumerable<Book>> GetAvailableAsync()
    {
        // Om du inte har en IsAvailable-property,
        // returnera alla så länge
        return Task.FromResult<IEnumerable<Book>>(_books.ToList());
    }
}
using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByISBNAsync(string isbn);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> SearchAsync(string searchTerm);
        Task<IEnumerable<Book>> GetAvailableAsync();

       
    }
}

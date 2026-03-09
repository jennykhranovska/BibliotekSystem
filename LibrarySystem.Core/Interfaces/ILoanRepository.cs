using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Interfaces;

public interface ILoanRepository
{
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Loan>> GetActiveLoansAsync(CancellationToken cancellationToken = default);

    // Returnerar ett lån inklusive navigationsdata (Book + Member)
    Task<Loan?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Loan loan, CancellationToken cancellationToken = default);

    // Behåll om du använder explicit unit-of-work; annars ta bort och spara i Add/Update
    Task SaveAsync(CancellationToken cancellationToken = default);
}
using System.Threading;
using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _context.Loans
        .AsNoTracking()
        .Include(l => l.Member)
        .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Loan>> GetActiveLoansAsync(CancellationToken cancellationToken = default)
     => await _context.Loans
         .AsNoTracking()
         .Include(l => l.Member)
         .Include(l => l.Book)
         .Where(l => l.ReturnDate == null)
         .ToListAsync(cancellationToken);
    public Task<Loan?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        => _context.Loans
            .Include(l => l.Member)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        => _context.Loans.AddAsync(loan, cancellationToken).AsTask();

    public Task SaveAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
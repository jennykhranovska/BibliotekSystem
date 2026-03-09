using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Models;
using LibrarySystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryContext _context;

    public MemberRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();


    }
    public Task<List<Member>> GetAllAsync()
    => _context.Members.AsNoTracking().ToListAsync();

    public Task<Member?> GetByMemberIdAsync(string memberId)
    => _context.Members
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.MemberId == memberId);
}
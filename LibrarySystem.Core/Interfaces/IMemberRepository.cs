using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Interfaces
{

    public interface IMemberRepository
    {
        Task AddAsync(Member member);
        Task SaveAsync();

        Task<List<Member>> GetAllAsync();


        Task<Member?> GetByMemberIdAsync(string memberId);
    }
}

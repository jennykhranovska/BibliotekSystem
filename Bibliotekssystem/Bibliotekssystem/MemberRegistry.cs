using System.Collections.Generic;
using System.Linq;


namespace LibrarySystem.Core.Models
{
    public class MemberRegistry
    {
        private List<Member> members = new();

        public void AddMember(Member member)
        {
            members.Add(member);
        }

        public IReadOnlyList<Member> GetMembers()
        {
            return members;
        }

        public Member GetMember(string memberId)
        {
            return members.FirstOrDefault(m => m.MemberId == memberId);
        }

    }
}

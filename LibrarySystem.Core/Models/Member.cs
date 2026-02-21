using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Models
{
    public class Member : ISearchable
    {
        public string MemberId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; set; }

        public DateTime MemberSince { get; private set; }

        private readonly List<Book> borrowedBooks = new();
        public IReadOnlyList<Book> BorrowedBooks => borrowedBooks;

        // EF Core behöver en parameterlös konstruktor
        private Member() { }

        public Member(string memberId, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("MemberId cannot be empty.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");

            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;
        }

        public void ShowMemberInfo()
        {
            Console.WriteLine($"MemberId: {MemberId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Member since: {MemberSince:yyyy-MM-dd}");
            Console.WriteLine($"Borrowed books: {BorrowedBooks.Count}");
        }

        public bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return false;

            return MemberId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                || Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                || (Email?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false);
        }
    }
}
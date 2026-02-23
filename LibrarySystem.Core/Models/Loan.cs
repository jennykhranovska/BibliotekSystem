using System;

namespace LibrarySystem.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        // Matchar Member.MemberId (string)
        public string MemberId { get; set; } = default!;
        public Member Member { get; set; } = default!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }   

        // Parameterlös ctor för EF
        public Loan() { }

        // Domän‑ctor som sätter både navigation och FK
        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            BookId = book.Id;
            MemberId = member.MemberId;
            LoanDate = loanDate;
            DueDate = dueDate;
        }

        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public bool IsReturned => ReturnDate != null;
        public void ReturnBook() => ReturnDate = DateTime.Now;
    }
}
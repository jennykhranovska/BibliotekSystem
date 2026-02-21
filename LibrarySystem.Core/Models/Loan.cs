using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Core.Models

{
    public class Loan
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Id { get; set; }

        public Loan() { }

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = dueDate;
        }
        public bool IsOverdue
        {
            get { return !IsReturned && DateTime.Now > DueDate; }
        }


        public bool IsReturned
        {
            get
            {
                return ReturnDate != null;
            }
        }

        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
        }

    }
}
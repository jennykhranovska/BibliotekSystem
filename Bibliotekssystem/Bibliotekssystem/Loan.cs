using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem
{
    public class Loan
    {
        public Book Book { get; }
        public Member Member { get; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get;}
        public DateTime? ReturnDate { get; private set; }


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
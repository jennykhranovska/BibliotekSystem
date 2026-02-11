using System;
using System.Collections.Generic;
using System.Linq;

namespace Bibliotekssystem
{
    public class LoanManager
    {
        private readonly List<Loan> loans = new List<Loan>();

        public void LoanBook(Book book, Member member, int days = 14)
        {
            // kan inte låna ut samma bok om den redan är utlånad och inte returnerad
            bool isAlreadyLoaned =
                loans.Any(l => l.Book.ISBN == book.ISBN && !l.IsReturned);

            if (isAlreadyLoaned)
                throw new InvalidOperationException("Boken är redan utlånad.");

            var loanDate = DateTime.Now;
            var dueDate = loanDate.AddDays(days);

            book.MarkAsLoaned();
            loans.Add(new Loan(book, member, loanDate, dueDate));

        


        }

        public void ReturnBook(string isbn)
        {
            var activeLoan = loans.FirstOrDefault(l => l.Book.ISBN == isbn && !l.IsReturned);

            if (activeLoan == null)
                throw new InvalidOperationException("Hittade inget aktivt lån för den boken.");

            activeLoan.ReturnBook();
            activeLoan.Book.MarkAsReturned();

        }

        public List<Loan> GetLoans()
        {
            return new List<Loan>(loans);
        }

        public List<Loan> GetActiveLoans()
        {
            return loans.Where(l => !l.IsReturned).ToList();
        }



    }
}

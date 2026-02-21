using System;
using System.Linq;


namespace LibrarySystem.Core.Models
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();

            SeedData(library);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Bibliotekssystem ===");
                Console.WriteLine("1. Visa alla böcker");
                Console.WriteLine("2. Sök bok");
                Console.WriteLine("3. Låna bok");
                Console.WriteLine("4. Returnera bok");
                Console.WriteLine("5. Visa medlemmar");
                Console.WriteLine("6. Statistik");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nVälj: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks(library);
                        break;
                    case "2":
                        SearchBooks(library);
                        break;
                    case "3":
                        LoanBook(library);
                        break;
                    case "4":
                        ReturnBook(library);
                        break;
                    case "5":
                        ShowMembers(library);
                        break;
                    case "6":
                        ShowStatistics(library);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        Pause();
                        break;
                }
            }
        }

        // ===== MENYVAL =====

        static void ShowAllBooks(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Alla böcker ===\n");

            var books = library.BookCatalog.GetBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("Inga böcker finns i katalogen.");
                Pause();
                return;
            }

            foreach (var book in books)
            {
                var status = IsLoaned(library, book.ISBN) ? "Utlånad" : "Tillgänglig";
                Console.WriteLine($"{book.GetInfo()} - {status}");
            }

            Pause();
        }

        static void SearchBooks(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Sök bok ===\n");
            Console.Write("Sökterm: ");
            var term = Console.ReadLine();

            var results = library.SearchBooks(term);

            Console.WriteLine("\nSökresultat:");

            if (results.Count == 0)
            {
                Console.WriteLine("Inga träffar.");
                Pause();
                return;
            }

            int i = 1;
            foreach (var book in results)
            {
                var status = IsLoaned(library, book.ISBN) ? "Utlånad" : "Tillgänglig";
                Console.WriteLine($"{i}. {book.GetInfo()} - {status}");
                i++;
            }

            Pause();
        }

        static void LoanBook(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Låna bok ===\n");

            Console.Write("Ange ISBN: ");
            var isbn = Console.ReadLine();

            Console.Write("Ange medlems-ID: ");
            var memberId = Console.ReadLine();

            var book = library.BookCatalog.GetBook(isbn);
            if (book == null)
            {
                Console.WriteLine("Boken hittades inte.");
                Pause();
                return;
            }

            var member = library.MemberRegistry.GetMember(memberId);
            if (member == null)
            {
                Console.WriteLine("Medlemmen hittades inte.");
                Pause();
                return;
            }

            try
            {
                library.LoanManager.LoanBook(book, member);

                // Hämta lånet vi precis skapade (för att visa DueDate)
                var loan = library.LoanManager
                    .GetActiveLoans()
                    .FirstOrDefault(l => l.Book.ISBN == isbn && l.Member.MemberId == memberId);

                Console.WriteLine($"\nBoken \"{book.Title}\" har lånats ut till {member.Name}.");

                if (loan != null)
                    Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kunde inte låna ut: {ex.Message}");
            }

            Pause();
        }

        static void ReturnBook(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Returnera bok ===\n");

            Console.Write("Ange ISBN: ");
            var isbn = Console.ReadLine();

            try
            {
                library.LoanManager.ReturnBook(isbn);
                Console.WriteLine("\nBoken är returnerad.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kunde inte returnera: {ex.Message}");
            }

            Pause();
        }

        static void ShowMembers(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Medlemmar ===\n");

            var members = library.MemberRegistry.GetMembers();

            if (members.Count == 0)
            {
                Console.WriteLine("Inga medlemmar registrerade.");
                Pause();
                return;
            }

            foreach (var m in members)
            {
                Console.WriteLine($"{m.MemberId} - {m.Name} ({m.Email})");
            }

            Pause();
        }

        static void ShowStatistics(Library library)
        {
            Console.Clear();
            Console.WriteLine("=== Statistik ===\n");

            Console.WriteLine($"Totalt antal böcker: {library.GetTotalBooksCount()}");
            Console.WriteLine($"Antal utlånade böcker: {library.GetLoanedBooksCount()}");

            var mostActive = library.GetMostActiveBorrower();
            if (mostActive == null)
                Console.WriteLine("Mest aktiva låntagaren: (ingen ännu)");
            else
                Console.WriteLine($"Mest aktiva låntagaren: {mostActive.Name} ({mostActive.MemberId})");

            Pause();
        }

   
        static bool IsLoaned(Library library, string isbn)
        {
            return library.LoanManager.GetActiveLoans().Any(l => l.Book.ISBN == isbn);
        }

        static void Pause()
        {
            Console.WriteLine("\nTryck Enter för att fortsätta...");
            Console.ReadLine();
        }

        static void SeedData(Library library)
        {
            // Böcker
            library.BookCatalog.AddBook(new Book("Sagan om ringen", "J.R.R. Tolkien", 1954, "978-91-0-012345-6"));
            library.BookCatalog.AddBook(new Book("Hobbiten", "J.R.R. Tolkien", 1937, "978-91-0-000000-0"));
            library.BookCatalog.AddBook(new Book("Clean Code", "Robert C. Martin", 2008, "ISBN-456"));
            library.BookCatalog.AddBook(new Book("C# Basics", "Anna Andersson", 2022, "ISBN-123"));

            // Medlemmar
            library.MemberRegistry.AddMember(new Member("M001", "Anna Andersson", "anna@test.com"));
            library.MemberRegistry.AddMember(new Member("M002", "Erik Svensson", "erik@test.com"));
        }
    }
}

# Bibliotekssystem

## Hur man kör projektet

1. Klona projektet från GitHub.
2. Öppna lösningen **Bibliotekssystem.sln** i Visual Studio.
3. Återställ NuGet-paket om det behövs.
4. Starta projektet genom att trycka **F5** eller klicka på **Run**.

Projektet använder **Blazor** för användargränssnittet och **.NET** för backend-logiken.

---

## Databasmodell

Systemet innehåller följande huvudsakliga entiteter:

### Bok

* Id
* Title (Titel)
* Author (Författare)
* ISBN
* PublishedYear (Utgivningsår)
* IsAvailable (Tillgänglig)

### Medlem

* MemberId
* Name (Namn)
* Email


### Lån

* Id
* BookId
* MemberId
* LoanDate (Lånedatum)
* ReturnDate (Återlämningsdatum)

Dessa modeller används för att hantera böcker, medlemmar och lån i bibliotekssystemet.

### Relationer

- En **Book** kan ha flera **Loans**
- En **Member** kan ha flera **Loans**
- En **Loan** kopplar en **Book** till en **Member**

## Skärmbilder

### Översikt
![Översikt](screenshots/overview.png)


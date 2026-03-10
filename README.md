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

* Book (Bok)
* Member (Medlem)
* LoanDate (Lånedatum)
* ReturnDate (Återlämningsdatum)

Dessa modeller används för att hantera böcker, medlemmar och lån i bibliotekssystemet.

## Skärmbilder

### Översikt
![Översikt](images/overview.png)

### Böcker

*(Lägg in en bild på sidan där böcker visas)*

### Medlemmar

*(Lägg in en bild på sidan där medlemmar visas)*

### Lån

*(Lägg in en bild på sidan där lån hanteras)*

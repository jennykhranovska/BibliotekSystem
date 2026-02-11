# Bibliotekssystem

Ett konsolbaserat bibliotekssystem utvecklat i C#.

## Funktionalitet

Systemet hanterar:

- Böcker
- Medlemmar
- Utlåning
- Sökning
- Sortering
- Statistik

### Stöd för:

- Objektorienterad design (klasser, inkapsling, komposition)
- Interface (ISearchable)
- Polymorfism
- Enhetstester med xUnit
- Robust felhantering (validering i konstruktorer och exceptions)

---

## Struktur

Projektet består av:

- `Book`
- `Member`
- `Loan`
- `Library`
- `BookCatalog`
- `MemberRegistry`
- `LoanManager`

Tillgänglighet för böcker hanteras via aktiva lån.

---

## Testning

Testprojekt: `Bibliotekssystem.Tests`

Innehåller tester för:

- Book
- Loan
- Member
- Sökfunktion
- Statistik
- Felhantering och exceptions

Kör tester med:
dotnet test

## Hur man kör projektet

1. Öppna solution-filen i Visual Studio
2. Kör projektet
3. Följ menyvalen i konsolen


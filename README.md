# Vehicheck

Vehicheck este o aplicație dezvoltată pentru gestionarea și monitorizarea vehiculelor, oferind un sistem centralizat pentru stocarea și accesarea informațiilor despre autovehicule, întreținerea acestora și administrarea flotelor auto.

## Ce probleme rezolvă?
* **Gestionarea eficientă a informațiilor:** Centralizează toate datele într-un singur sistem, eliminând nevoia documentelor fizice.
* **Urmărirea întreținerii:** Permite programarea și monitorizarea reparațiilor și a lucrărilor de mentenanță.
* **Vizibilitate și Raportare:** Oferă informații în timp real despre starea vehiculelor din flotă.
* **Monitorizarea costurilor:** Ajută la urmărirea costurilor asociate fiecărui vehicul în parte.

## Tehnologii Folosite
* **Framework:** ASP.NET Core 9.0 (C#)
* **Arhitectură:** Layered Architecture
* **Bază de date:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Securitate:** Autentificare și Autorizare folosind JWT (JSON Web Tokens)
* **Documentație API:** Swagger / OpenAPI

## Structura Arhitecturală
Proiectul este structurat pe mai multe straturi (proiecte) pentru o separare clară a responsabilităților:
* **Vehicheck.API**: Punctul de intrare în aplicație, conține controllerele RESTful.
* **Vehicheck.Core**: Conține logica de business, serviciile, DTO-urile (Data Transfer Objects) și mapările.
* **Vehicheck.Database**: Gestionează contextul bazei de date, entitățile, migrările și repository-urile.
* **Vehicheck.Infrastructure**: Conține configurările aplicației, middleware-uri personalizate (ex: pentru logare și gestionarea unitară a excepțiilor).

## Baza de Date (Entități Principale)
Baza de date este relațională și cuprinde următoarele entități de bază:
* `Users` - Informații despre utilizatorii sistemului (Administratori și Utilizatori Standard).
* `Cars` - Date despre vehiculele înregistrate, asociate unui utilizator.
* `CarModels` & `CarManufacturers` - Modelele și producătorii auto.
* `Components` & `ComponentManufacturers` - Piesele auto și producătorii acestora.
* `Fixes` - Tipurile de reparații disponibile în sistem.

## API Endpoints
API-ul oferă operațiuni CRUD complete, opțiuni de filtrare, sortare și paginare pentru principalele resurse:
* `POST /api/Auth/login` - Autentificare și generare token JWT.
* `/api/Users` - Gestionarea utilizatorilor.
* `/api/Cars` - Gestionarea vehiculelor.
* `/api/CarModels` & `/api/CarManufacturers` - Gestionarea modelelor și mărcilor.
* `/api/Components` & `/api/ComponentManufacturers` - Gestiunea pieselor auto.
* `/api/Fixes` - Gestiunea reparațiilor.

## Cum se rulează proiectul

### Precondiții
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) instalat și rulând.
* Un IDE precum Visual Studio 2022 sau Visual Studio Code.

### Setup
1. Clonează repository-ul local.
2. Deschide fișierul `appsettings.json` din proiectul `Vehicheck.API` și configurează string-ul de conexiune la baza ta de date (`ConnectionStrings:VehicheckDB`). De asemenea, asigură-te că ai setată cheia `Jwt:Key` (dacă nu e deja setată).
3. Deschide un terminal în folderul soluției sau în `Vehicheck.API` și rulează migrările pentru a crea structura bazei de date:
   ```bash
   dotnet ef database update --project ../Vehicheck.Database
4. Rulează aplicația: dotnet run --project Vehicheck.API
5. Accesează interfața Swagger la adresa afișată în consolă (ex: https://localhost:7057/swagger) pentru a vizualiza și testa endpoint-urile API-ului.

Dezvoltatori: Arustei Andrei & Petre Flaviu

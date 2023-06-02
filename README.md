# Dokumentacja -  LanguageApp

## Opis

Aplikacja służy do nauki języka angielskiego. Po wybraniu poziomu trudności użytkownik może rozpocząć naukę. Użytkownik może tworzyć quizy, które polegają na przetłumaczeniu słowa z języka polskiego na język angielski. Po zakończeniu nauki użytkownik może sprawdzić swoje wyniki.

## Autorzy
- Katarzyna Stępień - grupa 6 - godz. 17:30 piątek
- Kinga Wrona - grupa 7 - godz. 19:00 piątek

## Technologie
- Baza danych - SQLite
- Entity Framework
- .NET, C#
- MVC 

## Uruchomienie
Aby uruchomić aplikację należy:
- Pobrać pliki z repozytorium
- Otworzyć projekt w preferowanym środowisku
- Zainstalować niezbędne pakiety NuGet
- Przejść do folderu `./Pojects/LanguageApp`
- Uruchomić aplikację komendą `dotnet run`

## Struktura projektu
- LanguageApp
    - Controllers
        - HomeController.cs
        - LoginController.cs
        - HomeController.cs
        - QuizController.cs
        - StatisticsController.cs
        - WordController.cs
    - Data
        - LanguageAppContext.cs
    - Migrations
        - ...
    - Models
        - ErrorViewModel.cs
        - Quiz.cs
        - QuizDetail.cs
        - QuizResults.cs
        - Question.cs
        - Statistics.cs
        - User.cs
        - Word.cs
        - WordSummary.cs
    - Views
        - Login
            - Create.cshtml
            - Delete.cshtml
            - Edit.cshtml
            - Index.cshtml
        - Home
            - Index.cshtml
            - Privacy.cshtml
        - Quiz
            - Create.cshtml
            - Delete.cshtml
            - Edit.cshtml
            - Index.cshtml
            - FinishQuiz.cshtml
            - TakeQuiz.cshtml
        - Shared
            - Error.cshtml
            - _AdminLayout.cshtml
            - _Layout.cshtml
            - _LoginPartial.cshtml
        - Statistics
            - Index.cshtml
        - Word
            - Create.cshtml
            - Delete.cshtml
            - Edit.cshtml
            - Index.cshtml
    - wwwroot
        - ...
    - appsettings.json
    - LanguageApp.csproj
    - Program.cs

## Funkcjonalności
- Logowanie
- Rejestracja jako administrator
- Dodawanie/Wyświetlanie/Usuwanie użytkowników (admin)
- Dodawanie/Usuwanie/Edytowanie/Wyszukiwanie słów w słowniku 
- Dodawanie/Usuwanie/Edytowanie/Wyszukiwanie quizów
- Wyświetlanie listy quizów
- Wyświetlenie szczegółow quizu
- Rozegranie quizu
- Podgląd statystyk użytkownika -> ilość wykonanych quizów, średni wynik


## Użytkowanie

### Logowanie:
- Użytkownik może się zalogować na konto, które zostało wcześniej utworzone.
- Jeśli użytkownik nie posiada konta i aplikacja nie była wcześniej uruchomiona, może się zarejestrować jako administrator.

### Wylogowanie:
- Użytkownik może się wylogować z aplikacji poprzez kliknięcie przycisku `Wyloguj się`. Znajduje ie on na pasku nawigacji.

### Dodawanie użytkownika:
- Administrator może dodać nowego użytkownika poprzez kliknięcie przycisku `Dodaj użytkownika`. Znajduje się on na stronie zawierającą listę użytkowników.

### Usuwanie użytkownika:
- Administrator może usunąć użytkownika poprzez kliknięcie przycisku `Delete`. Znajduje się on na stronie zawierającą listę użytkowników, obok danego użytkownika.

### Dodawanie słów:
- Każdy zalogowany użytkownik może dodać nowe słowo poprzez kliknięcie przycisku `Dodaj słowo`. Znajduje się on na stronie zawierającą listę słów.

### Usuwanie słów:
- Każdy zalogowany użytkownik może usunąć słowo poprzez kliknięcie przycisku `Delete`. Znajduje się on na stronie zawierającą listę słów, obok danego słowa.

### Edytowanie słów:
- Każdy zalogowany użytkownik może edytować słowo poprzez kliknięcie przycisku `Edit`. Znajduje się on na stronie zawierającą listę słów, obok danego słowa.

### Wyszukiwanie słów:
- Każdy zalogowany użytkownik może wyszukać słowo poprzez wpisanie go w pole `Search`. Znajduje się on na stronie zawierającą listę słów. Słowa zaczynające się na wpisany ciąg znaków zostaną wyświetlone.

### Dodawanie quizu:
- Każdy zalogowany użytkownik może dodać nowy quiz poprzez kliknięcie przycisku `Dodaj quiz`. Znajduje się on na stronie zawierającą listę quizów. Po kliknięciu zostanie wyświetlony formularz, w którym należy podać nazwę i opis quizu oraz wybrać liczbę zawartych słów. Po kliknięciu przycisku `Dodaj` quiz zostanie dodany do bazy danych.

### Usuwanie quizu:
- Każdy zalogowany użytkownik może usunąć quiz poprzez kliknięcie przycisku `Delete`. Znajduje się on na stronie zawierającą listę quizów, obok danego quizu.

### Edytowanie quizu:
- Każdy zalogowany użytkownik może edytować quiz poprzez kliknięcie przycisku `Edit`. Znajduje się on na stronie zawierającą listę quizów, obok danego quizu.

### Statystyki:
- Są tworzone dla każdego nowo dodanego użytkownika. Zawierają informacje o ilości wykonanych quizów oraz średnim wyniku. Statystyki są wyświetlane w zakładce `Statustyki`. Znajduje się ona na pasku nawigacji. Nie można jej edytować ani usunąć.

## Baza danych

### Tabele:
- User
    - UserId: int
    - Username: string
    - Password: string
- Word
    - WordId: int
    - Translation: string
    - Polish: string
- Quiz
    - QuizId: int
    - UserId: int
    - Name: string
    - Description: string
    - Liczba: int
- Question
    - QuestionId: int
    - QuizId: int
    - WordId: int
- Statistics
    - UserId: int
    - QuizCounter: int
    - AverageScore: double




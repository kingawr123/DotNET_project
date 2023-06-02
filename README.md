# Dokumentacja -  LanguageApp

## Opis

Aplikacja służy do nauki języka angielskiego. Po wybraniu poziomu trudności użytkownik może rozpocząć naukę. Użytkownik może tworzyć quizy, które polegają na przetłumaczeniu słowa z języka angielskiego na język polski. Po zakończeniu nauki użytkownik może sprawdzić swoje wyniki.

## Autorzy

- Katarzyna Stępień - grupa 6 - godz. 17:30 piątek
- Kinga Wrona - grupa 7 - godz. 19:00 piątek

## Funkcjonalności

- Logowanie
- Rejestracja jako administrator
- Dodawanie/Wyświetlanie/Usuwanie użytkowników (admin)
- Dodawanie/Usuwanie/Edytowanie/Wyszukiwanie słów w słowniku 
- Dodawanie/Usuwanie/Edytowanie/Wyszukiwanie quizów
- Wyświetlanie listy quizów
- Wyświetlenie szczegółow quizu
- Podgląd statystyk użytkownika -> ilość wykonanych quizów, średni wynik

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




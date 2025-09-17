using System;

namespace OOP_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();

            // Додавання книг
            lib += "Гаррі Поттер";
            lib += "Володар перснів";
            lib += "1984";

            lib.ShowBooks();

            // Доступ через індексатор
            Console.WriteLine($"\nКнига №2: {lib[1]}");

            // Заміна книги
            lib[1] = "Хоббіт";
            Console.WriteLine($"\nПісля заміни: {lib[1]}");

            // Видалення книги
            lib -= "1984";
            Console.WriteLine("\nПісля видалення:");
            lib.ShowBooks();
        }
    }
}

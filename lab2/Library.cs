namespace OOP_Lab2
{
    class Library
    {
        private string[] books;
        private int count;

        public int Count => count;

        public Library(int size = 10)
        {
            books = new string[size];
            count = 0;
        }

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < count)
                    return books[index];
                throw new IndexOutOfRangeException("Невірний індекс книги");
            }
            set
            {
                if (index >= 0 && index < count)
                    books[index] = value;
                else
                    throw new IndexOutOfRangeException("Невірний індекс книги");
            }
        }

        public static Library operator +(Library lib, string book)
        {
            if (lib.count >= lib.books.Length)
                Array.Resize(ref lib.books, lib.books.Length * 2);

            lib.books[lib.count++] = book;
            return lib;
        }

        public static Library operator -(Library lib, string book)
        {
            int index = Array.IndexOf(lib.books, book, 0, lib.count);
            if (index >= 0)
            {
                for (int i = index; i < lib.count - 1; i++)
                    lib.books[i] = lib.books[i + 1];

                lib.books[--lib.count] = null!;
            }
            return lib;
        }

        public void ShowBooks()
        {
            Console.WriteLine("Книги в бібліотеці:");
            for (int i = 0; i < count; i++)
                Console.WriteLine($"{i + 1}. {books[i]}");

            if (count == 0)
                Console.WriteLine("Бібліотека порожня.");
        }
    }
}

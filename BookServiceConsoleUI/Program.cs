using System;
using BookListService;

namespace BookServiceConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BookListService.BookListService bookListService=new BookListService.BookListService();
            Book[] books=new Book[5];
            books[0] = new Book("Albahari", "C# in a nutshell", 2012, 1043);
            books[1] = new Book("Richter", "CLR via C#", 2013, 896);
            books[2] = new Book("Richter", "Thinking in Java", 2009, 637);
            books[3] = new Book("Lorem", "Ipsum", 2005, 1024);
            books[4] = new Book("Dolor", "Sit Amet", 2015, 512);
            for (int i = 0; i < books.Length; i++)
            {
                bookListService.AddBook(books[i]);
                Console.WriteLine($"Book '{books[i].Title}' has been successfully added to the collection");
            }
            Console.WriteLine();
            try
            {
                bookListService.AddBook(books[0]);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine();
            BinaryBookStorage binaryBookStorage=new BinaryBookStorage("books.dat");
            bookListService.SaveCollectionToStorage(binaryBookStorage);
            Console.WriteLine("Collection has been successfully saved in the storage");
            var anotherBookListService =new BookListService.BookListService();
            anotherBookListService.ReadCollectionFromStorage(binaryBookStorage);
            Console.WriteLine("Collection has been successfully loaded from the storage");

            foreach (BookListService.BookListService.BookTag tag in Enum.GetValues(typeof(BookListService.BookListService.BookTag)))
            {
                Console.WriteLine($"Sorting by tag {tag}");
                anotherBookListService.SortByTag(tag);
                foreach (Book book in anotherBookListService.ToList())
                {
                    Console.WriteLine(book);
                }
                Console.WriteLine();
            }
            Book bookToRemove = anotherBookListService.FindBookByTag("Ipsum",
                BookListService.BookListService.BookTag.Title);
            anotherBookListService.RemoveBook(bookToRemove);
            try
            {
                anotherBookListService.RemoveBook(bookToRemove);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadKey();
        }
    }
}

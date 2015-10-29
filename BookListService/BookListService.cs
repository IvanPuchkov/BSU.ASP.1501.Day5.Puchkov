using System;
using System.Collections.Generic;

namespace BookListService
{
    public class BookListService
    {
        private List<Book> _books;

        public enum BookTag { Title,Author,Year,PageCount}

        public BookListService(params Book[] books)
        {
            if (books==null)
                throw new ArgumentNullException(nameof(books));
            _books=new List<Book>();
            foreach (var book in books)
            {
                _books.Add(book);
            }
        }

        public List<Book> ToList()
        {
            List<Book> resultList=new List<Book>();
            foreach (var book in _books)
            {
                resultList.Add(book);
            }
            return resultList;
        } 

        public void SaveCollectionToStorage(IBookDataStorage storage)
        {
            if (storage==null)
                throw new ArgumentNullException(nameof(storage));
            if (string.IsNullOrEmpty(storage.StorageLocation))
                throw new InvalidOperationException("StorageLocation can't be null or empty");
            storage.SaveToStorage(_books);
        }

        public void ReadCollectionFromStorage(IBookDataStorage storage)
        {
            if(storage==null)
                throw new ArgumentNullException(nameof(storage));
            if (string.IsNullOrEmpty(storage.StorageLocation))
                throw new InvalidOperationException("StorageLocation can't be null or empty");
            _books = storage.ReadListFromStorage();
        }

        public void SortByTag(BookTag tag)
        {
            switch (tag)
            {
                case BookTag.Year:
                    _books.Sort((x, y) => x.Year.CompareTo(y.Year));
                    break;
                case BookTag.Author:
                    _books.Sort((x, y) => string.Compare(x.Author, y.Author, StringComparison.InvariantCulture));
                    break;
                case BookTag.PageCount:
                    _books.Sort((x, y) => x.PageCount.CompareTo(y.PageCount));
                    break;
                case BookTag.Title:
                    _books.Sort();
                    break;
            }
        }

        public void AddBook(Book book)
        {
            if (book==null)
                throw new ArgumentNullException(nameof(book));
            if ((book.Title==null) || (book.Author==null))
                throw new ArgumentException("All fields in book must be non-null");
            foreach (var item in _books)
            {
                if (item.Equals(book))
                    throw new ArgumentException($"Book '{book.Title}' already exists in the collection");
            }
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if (book==null)
                throw new ArgumentNullException(nameof(book));
            Book bookToDelete = null;
            foreach (var item in _books)
            {
                if (item.Equals(book))
                bookToDelete = item;
            }
            if (bookToDelete==null)
                throw new ArgumentException("Can't find specified book in the collection");
            _books.Remove(bookToDelete);
        }
        
        public Book FindBookByTag(object tagObject, BookTag tag)
        {
            if (tagObject==null)
                throw new ArgumentNullException(nameof(tagObject));
            string stringTagObject;
            Book resultBook = null;
            switch (tag)
            {
                case BookTag.Author:
                    stringTagObject = tagObject as string;
                    if (stringTagObject == null)
                        throw new ArgumentException("Incorrect tagObject!");
                    resultBook =
                        _books.Find(x => x.Author.Equals(stringTagObject, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case BookTag.Title:
                    stringTagObject = tagObject as string;
                    if (stringTagObject==null)
                        throw new ArgumentException("Incorrect tagObject!");
                    resultBook=_books.Find(x => x.Title.Equals(stringTagObject, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case BookTag.PageCount:
                    int pageCount;
                    try
                    {
                        pageCount = Convert.ToInt32(tagObject);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Incorrect tagObject!");
                    }
                    resultBook = _books.Find(x => x.PageCount == pageCount);
                    break;
                case BookTag.Year:
                    int year;
                    try
                    {
                        year = Convert.ToInt32(tagObject);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Incorrect tagObject!");
                    }
                    resultBook = _books.Find(x => x.PageCount == year);
                    break;
            }
            return resultBook;
        }
    }
}

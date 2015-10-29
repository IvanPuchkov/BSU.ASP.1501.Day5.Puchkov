using System;

namespace BookListService
{
    public class Book:IEquatable<Book>,IComparable<Book>
    {
        private string _author;
        private string _title;

        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                if (value != null)
                    _author = value;
                else
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != null)
                    _title = value;
                else
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }

        }
        public int Year { get; set; }
        public int PageCount { get; set; }

        public Book(string author,string title,int year,int pageCount)
        {
            Author = author;
            Title = title;
            Year = year;
            PageCount = pageCount;
        }

        public Book(string title)
        {
            Title = title;
        }

        public int CompareTo(Book compareBook)
        {
            if (compareBook == null)
                return 1;
            return String.Compare(this.Title, compareBook.Title, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Book book = obj as Book;
            if (book == null)
                return false;
            return Equals(book);
        }

        public bool Equals(Book otherBook)
        {
            if (otherBook == null)
                return false;
            bool titleCompare = String.Equals(this.Title, otherBook.Title, StringComparison.InvariantCulture);
            bool authorCompare = String.Equals(this.Author, otherBook.Author, StringComparison.InvariantCulture);
            return titleCompare && authorCompare;
        }

        public override string ToString()
        {
            return $"Title: '{Title}'; Author: {Author}; Year: {Year}; PageCount:{PageCount}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace BookListService
{
    public class BinaryBookStorage:IBookDataStorage
    {
        public string StorageLocation { get; set; }

        public BinaryBookStorage(string storageLocation)
        {
            StorageLocation = storageLocation;
        }

        public void SaveToStorage(List<Book> books)
        {
            if(string.IsNullOrEmpty(StorageLocation))
                throw new InvalidOperationException("StorageLocation can't be null or empty");
            if (books==null)
                throw new ArgumentNullException();
            using (var fs = new FileStream(StorageLocation, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    foreach (var book in books)
                    {
                        bw.Write(book.Author);
                        bw.Write(book.Title);
                        bw.Write(book.PageCount);
                        bw.Write(book.Year);
                    }
                }
            }
        }

        public List<Book> ReadListFromStorage()
        {
            if (string.IsNullOrEmpty(StorageLocation))
                throw new InvalidOperationException("StorageLocation can't be null or empty");
            if (!File.Exists(StorageLocation))
                throw new FileNotFoundException();
            var result=new List<Book>();
            using (var fs = new FileStream(StorageLocation, FileMode.Open))
            {
                using (var br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        string author = br.ReadString();
                        string title = br.ReadString();
                        int pageCount = br.ReadInt32();
                        int year = br.ReadInt32();
                        result.Add(new Book(author,title,year,pageCount));
                    }
                }
            }
            return result;
        }
    }
}

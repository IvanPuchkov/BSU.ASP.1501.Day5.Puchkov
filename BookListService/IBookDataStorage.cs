using System.Collections.Generic;

namespace BookListService
{
    public interface IBookDataStorage
    {
        string StorageLocation { get; set; }
        void SaveToStorage(List<Book> books);
        List<Book> ReadListFromStorage();
    }
}

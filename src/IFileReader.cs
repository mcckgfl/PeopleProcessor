using System.Collections.Generic;

namespace PeopleProcessor
{
    public interface IFileReader
    {
        IList<T> DeserilizeCSVFile<T>(string filePath);
    }
}
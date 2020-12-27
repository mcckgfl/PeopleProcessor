using System.Collections.Generic;

namespace PeopleProcessor
{
    public interface ICsvReader
    {
        IList<T> DeserilizeCSVFile<T>(string filePath);
    }
}
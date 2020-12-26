using System.Collections.Generic;

namespace PeopleProcessor
{
    public interface IPeopleCsvReader
    {
        IList<T> DeserilizeCSVFile<T>(string filePath);
    }
}
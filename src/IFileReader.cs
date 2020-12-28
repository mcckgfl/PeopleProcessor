using System.Collections.Generic;

namespace PeopleProcessor
{
    public interface IFileReader
    {
        IList<T> Convert<T>(string filePath);
    }
}
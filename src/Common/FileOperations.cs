using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PeopleProcessor.Common
{
    public static class FileOperations
    {

        public static void ExportTextFile(string result, string outputFilePathName)
        {
            File.WriteAllText(outputFilePathName, result);
            Console.WriteLine("File Output To: " + outputFilePathName);
        }
    }
}

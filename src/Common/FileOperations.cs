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
        public static bool InputFileExists(string filePathName)
        {
            if (!File.Exists(filePathName))
            {
                Console.WriteLine("Unable to locate file: " + filePathName);
                return false;
            }
            return true;
        }
    }
}

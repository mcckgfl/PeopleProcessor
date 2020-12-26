using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleProcessor.Helpers
{
    public static class Helpers
    {
        public static string GetCurrentDirectoryUsingReflection()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            return System.IO.Path.GetDirectoryName(
             System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

    }
}

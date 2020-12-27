using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PeopleProcessor
{
    public class PeopleCsvReader : ICsvReader
    {
        public IList<T> DeserilizeCSVFile<T>(string filePath)
        {

            IList<T> list = new List<T>();

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.TrimOptions = TrimOptions.Trim;
                    list = csv.GetRecords<T>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }


    }
}

using CsvHelper;
using CsvHelper.Configuration;
using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PeopleProcessor
{
    public class PeopleCsvReader : IFileReader
    {

        public IList<T> Convert<T>(string filePath)
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

    public static class CsvProcessor
    {
        public static List<T> ConvertTo<T, TMap>(string file) where T : class
                    where TMap : ClassMap<T>
        {
            using (var reader = new StreamReader(file))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var good = new List<T>();

                // Set up CSV Helper
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.IgnoreQuotes = true;
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.HeaderValidated = null;
                csvReader.Configuration.DetectColumnCountChanges = true;
                csvReader.Configuration.TrimOptions = TrimOptions.Trim;

                var classMap = csvReader.Configuration.RegisterClassMap<TMap>();

                try
                {
                    good = csvReader.GetRecords<T>().ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine(ex.Data["CsvHelper"]);
                }

                return good.ToList();
            }
        }
    }

        public class PersonMap : ClassMap<Person>
        {
            public PersonMap()
            {
                Map(m => m.Id);
                Map(m => m.ParentId);
                Map(m => m.FirstName);
                Map(m => m.LastName);
                Map(m => m.RowId).ConvertUsing(row => row.Context.RawRow);
        }
        }


}

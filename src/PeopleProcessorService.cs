using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Reflection;
using Newtonsoft.Json;
using PeopleProcessor.Common;
using System.IO;

namespace PeopleProcessor
{
    public class PeopleProcessorService
    {
        private readonly ICsvReader _peopleCsvReader;
        private readonly IPersonValidator _personValidator;
        
        public PeopleProcessorService(ICsvReader peopleCsvReader, IPersonValidator personValidator)
        {
            _peopleCsvReader = peopleCsvReader;
            _personValidator = personValidator;
        }

        public void Run()
        {
            var directoryName = Environment.CurrentDirectory + "\\Data\\";
            var inputFilePathName = directoryName + "people.csv";
            var outputFilePathName = directoryName + DateTime.Today.ToString("yyyy-MM-dd.") + "people.json";
            IList<string> qualifiedNameList = new List<string> { "Christina", "Dave", "Kris", "Jared", "Kait", "Paul" };

            var people = _peopleCsvReader.DeserilizeCSVFile<Person>(inputFilePathName);

            //create config for automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, FamilyDto>()
                .ConstructUsing(x => new FamilyDto(x.Id, people));
            });

            //map persons to families
            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<Person>, IList<FamilyDto>>(people);

            //filter families using pre-determined list and logic
            List<FamilyDto> selectedParents = GetSelectedFamilyDto(people, parentList, qualifiedNameList);

            string result = JsonConvert.SerializeObject(selectedParents, JsonConfiguration.JsonSettings);
            Console.WriteLine(result);

            ExportTextFile(result, outputFilePathName);

        }

        private static void ExportTextFile(string result, string outputFilePathName)
        {
            File.WriteAllText(outputFilePathName, result);
            Console.WriteLine("File Output To: " + outputFilePathName);
        }

        private List<FamilyDto> GetSelectedFamilyDto(IList<Person> persons, IList<FamilyDto> parentList, IList<string> qualifiedNameList)
        {
            List<FamilyDto> selectedParents = new List<FamilyDto>();

            foreach (FamilyDto p in parentList)
            {
                if (_personValidator.Validate(p, persons, qualifiedNameList))
                {
                    selectedParents.Add(p);
                }
            }

            return selectedParents;
        }
    }
}

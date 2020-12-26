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
    public class PeopleProcessorService : IPeopleProcessorService
    {
        private readonly IPeopleCsvReader _peopleCsvReader;
        private readonly IPersonValidator _personValidator;
        private readonly IList<string> qualifiedNameList = new List<string> { "Christina", "Dave", "Kris", "Jared", "Kait", "Paul" };
        public PeopleProcessorService(IPeopleCsvReader peopleCsvReader, IPersonValidator personValidator)
        {
            _peopleCsvReader = peopleCsvReader;
            _personValidator = personValidator;
        }

        public void Run()
        {
            var directoryName = Environment.CurrentDirectory + "\\Data\\";
            var inputFilePathName = directoryName + "people.csv";
            var outputFilePathName = directoryName + DateTime.Today.ToString("yyyy-MM-dd.") + "people.json";

            var persons = _peopleCsvReader.DeserilizeCSVFile<PersonDto>(inputFilePathName);

            var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<PersonDto, Parent>()
                    .ConstructUsing(x => new Parent(x.Id, persons));
            });

            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<PersonDto>, IList<Parent>>(persons);

            foreach (Parent p in parentList) 
            {
                if (_personValidator.Validate(p, persons, qualifiedNameList))
                {
                    Console.WriteLine("{0} {1}", p.Id, p.Children.Count);
                }
            }

            string temp = JsonConvert.SerializeObject(parentList, JsonConfiguration.JsonSettings);
            Console.WriteLine(temp);
            File.WriteAllText(outputFilePathName, temp);




        }

    }
}

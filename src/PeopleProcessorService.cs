using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Reflection;
using Newtonsoft.Json;
using PeopleProcessor.Common;

namespace PeopleProcessor
{
    /// <summary>
    /// Utility to process a list of people from csv flat file to relational model, apply filter, and dump results in json format
    /// </summary>
    public class PeopleProcessorService
    {
        private readonly IFileReader _peopleCsvReader;
        private readonly QualifiedPersonValidator _personValidator;
        
        public PeopleProcessorService(IFileReader peopleCsvReader, QualifiedPersonValidator personValidator)
        {
            _peopleCsvReader = peopleCsvReader;
            _personValidator = personValidator;
        }

        public void Run()
        {
            
            var directoryName = Environment.CurrentDirectory + "\\Data\\";
            var inputFilePathName = directoryName + "people.csv";
            var outputFilePathName = directoryName + DateTime.Today.ToString("yyyy-MM-dd") + "_people.json";
            IList<string> qualifiedNameList = new List<string> { "Christina", "Dave", "Kris", "Jared", "Kait", "Paul" };

            var people = _peopleCsvReader.Convert<Person>(inputFilePathName);

            //create config for automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, RelationshipDto>()
                .ConstructUsing(x => new RelationshipDto(x.Id, people));
            });

            //map list of people to list of relationships... this will call constructor and populate children
            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<Person>, IList<RelationshipDto>>(people);

            //get qualifying persons and relationships
            List<RelationshipDto> selectedRelationships = _personValidator.GetQualifiedRelationshipDtos(people, parentList, qualifiedNameList);

            string result = JsonConvert.SerializeObject(selectedRelationships, JsonConfiguration.JsonSettings);
            Console.WriteLine(result);

            FileOperations.ExportTextFile(result, outputFilePathName);

        }

    }
}

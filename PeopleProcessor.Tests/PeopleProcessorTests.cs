using Xunit;
using System;
using CsvHelper;
using CsvHelper.Configuration;
using PeopleProcessor.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using AutoMapper;

namespace PeopleProcessor.CsvParserTests
{

    public class CsvReaderTest : IDisposable
    {
        private StreamReader streamReader;
        private CsvReader csvReader;
        IEnumerable<Person> records = new List<Person>();

        public CsvReaderTest()
        {
            streamReader = new StreamReader(Environment.CurrentDirectory + "\\Data\\sample.csv");
            csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            records = csvReader.GetRecords<Person>();
        }
        public void Dispose()
        {
            streamReader.Dispose();
        }

        [Fact]
        public void VerifyNumberOfCSVLines()
        {
            Assert.Equal(9, records.Count());
        }
    }

    public class DeserializerTests
    {

        [Theory]
        [InlineData(1, "John", "Doe", -1)]
        [InlineData(2, "Jane", "Doe", 1)]
        [InlineData(9, "Will", "Doe", 2)]
        public void DeserilizedListValidation(int id, string fname, string lname, int parentId)
        {
            PeopleCsvReader pcr = new PeopleCsvReader();
            var p = pcr.DeserilizeCSVFile<Person>(Environment.CurrentDirectory + "\\Data\\sample.csv");

            Assert.Equal(fname, p.ElementAt(id - 1).FirstName);
            Assert.Equal(lname, p.ElementAt(id - 1).LastName);
            Assert.Equal(id, p.ElementAt(id - 1).Id);
            Assert.Equal(parentId, p.ElementAt(id - 1).ParentId);
        }

        [Fact]
        public void DeserilizedListContainsData()
        {
            PeopleCsvReader pcr = new PeopleCsvReader();
            var p = pcr.DeserilizeCSVFile<Person>(Environment.CurrentDirectory + "\\Data\\sample.csv");

            Assert.NotEmpty(p);
        }
    }

    public class IMapperTests
    {
        [Fact]
        public void ParentsAreAssignedViaAutomapper()
        {
            List<Person> people = new List<Person>() {
                new Person() { Id = 1, FirstName = "John", LastName = "Doe", ParentId = -1 },
                new Person() { Id = 2, FirstName = "Jane", LastName = "Doe", ParentId = 1 }
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, RelationshipDto>()
                .ConstructUsing(x => new RelationshipDto(x.Id, people));
            });

            //map persons to families
            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<Person>, IList<RelationshipDto>>(people);

            Assert.Equal("John", parentList[0].FirstName);
        }

        [Fact]
        public void ChildrenAreAssigned()
        {
            List<Person> people = new List<Person>() {
                new Person() { Id = 1, FirstName = "John", LastName = "Doe", ParentId = -1 },
                new Person() { Id = 2, FirstName = "Jane", LastName = "Doe", ParentId = 1 }
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, RelationshipDto>()
                .ConstructUsing(x => new RelationshipDto(x.Id, people));
            });

            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<Person>, IList<RelationshipDto>>(people);

            Assert.Equal("Jane", parentList[0].Children[0].FirstName);
        }
    }


    public class QualifiedPersonValidatorTests
    {

        private List<Person> people = new List<Person>() {
                new Person() { Id = 1, FirstName = "John", LastName = "Doe", ParentId = -1 },
                new Person() { Id = 2, FirstName = "Jane", LastName = "Doe", ParentId = 1 },
                new Person() { Id = 4, FirstName = "David", LastName = "Ortiz", ParentId = -1 },
                new Person() { Id = 8, FirstName = "David", LastName = "Ortiz", ParentId = -1 }
            };

        private IList<RelationshipDto> relationships;

        public QualifiedPersonValidatorTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, RelationshipDto>()
                .ConstructUsing(x => new RelationshipDto(x.Id, people));
            });

            IMapper iMapper = config.CreateMapper();
            var parentList = iMapper.Map<IList<Person>, IList<RelationshipDto>>(people);

            //List<RelationshipDto> relationships = new List<RelationshipDto>() {
            //    new RelationshipDto(1, people),
           //     new RelationshipDto(2, people),
           //     new RelationshipDto(4, people),
           //     new RelationshipDto(8, people)
           // };
            relationships = iMapper.Map<IList<Person>, IList<RelationshipDto>>(people);

            //Assert.Equal("David", relationships[0].FirstName);

        }

        [Fact]
        public void OnlyReturnsOneRecordForSameFirstName()
        {
            IList<string> qualified = new List<string> { "David" };
            QualifiedPersonValidator qpv = new QualifiedPersonValidator();
            List<RelationshipDto> selectedParents = qpv.GetQualifiedPersonRelationshipDtos(people, relationships, qualified);
            Assert.Single(selectedParents);
        }

        [Fact]
        public void ReturnTwoRecords()
        {
            IList<string> qualified = new List<string> { "John", "David"};
            QualifiedPersonValidator qpv = new QualifiedPersonValidator();
            List<RelationshipDto> selectedParents = qpv.GetQualifiedPersonRelationshipDtos(people, relationships, qualified);
            Assert.Equal(2, selectedParents.Count);
        }

        [Fact]
        public void ReturnZeroRecords()
        {
            IList<string> qualified = new List<string>();
            QualifiedPersonValidator qpv = new QualifiedPersonValidator();
            List<RelationshipDto> selectedParents = qpv.GetQualifiedPersonRelationshipDtos(people, relationships, qualified);
            Assert.Empty(selectedParents);
        }

        [Fact]
        public void ReturnsOneChildNamedJane()
        {
            IList<string> qualified = new List<string> { "John" };
            QualifiedPersonValidator qpv = new QualifiedPersonValidator();
            List<RelationshipDto> selectedParents = qpv.GetQualifiedPersonRelationshipDtos(people, relationships, qualified);
            Assert.Equal("Jane", selectedParents[0].Children[0].FirstName="Jane");
        }
    }
}



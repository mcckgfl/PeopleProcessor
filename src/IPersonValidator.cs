using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleProcessor
{
    public interface IPersonValidator
    {
        public bool Validate(FamilyDto parent, IList<Person> peopleList, IList<string> firstNames);
    }
}

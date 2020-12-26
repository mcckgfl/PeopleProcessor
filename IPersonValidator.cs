using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleProcessor
{
    public interface IPersonValidator
    {
        public bool Validate(Parent parent, IList<PersonDto> peopleList, IList<string> firstNames);
    }
}

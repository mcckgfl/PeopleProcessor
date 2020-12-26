using PeopleProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleProcessor
{
    public class QualifiedPersonValidator : IPersonValidator
    {
        //private IList<string> qualifiedPersonFirstNameList = new List<string> { "Christina", "Dave", "Kris", "Jared", "Kait", "Paul" };
        public bool Validate(Parent parent, IList<PersonDto> peopleList, IList<string> criteria)
        {
            return FirstNameMatches(parent, criteria) && IsFirstInstanceOfThisPerson(parent, peopleList) ? true : false;
        }

        private bool FirstNameMatches(Parent parent, IList<string> criteria)
        {
            return criteria.Contains(parent.FirstName) ? true : false;
        }

        private bool IsFirstInstanceOfThisPerson(Parent parent, IList<PersonDto> peopleList)
        {
            //peopleList.Where(p => p.IsQualifiedParent == true).GroupBy(x => x.FirstName, (key, g) => g.OrderBy(e => e.Id).First());
            return peopleList.Where(p => p.FirstName == parent.FirstName).First().Id == parent.Id;
        }
    }
}

using PeopleProcessor.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeopleProcessor
{
    public class QualifiedPersonValidator : IPersonValidator
    {

        public bool Validate(FamilyDto parent, IList<Person> peopleList, IList<string> criteria)
        {
            return FirstNameMatches(parent, criteria) && IsFirstInstanceOfThisPerson(parent, peopleList) ? true : false;
        }

        private bool FirstNameMatches(FamilyDto parent, IList<string> criteria)
        {
            return criteria.Contains(parent.FirstName) ? true : false;
        }

        private bool IsFirstInstanceOfThisPerson(FamilyDto parent, IList<Person> peopleList)
        {
            //peopleList.Where(p => p.IsQualifiedParent == true).GroupBy(x => x.FirstName, (key, g) => g.OrderBy(e => e.Id).First());
            return peopleList.Where(p => p.FirstName == parent.FirstName).First().Id == parent.Id;
        }
    }
}

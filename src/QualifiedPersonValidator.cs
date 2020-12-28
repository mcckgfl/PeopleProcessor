using PeopleProcessor.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeopleProcessor
{
    public class QualifiedPersonValidator
    {

        public bool Validate(RelationshipDto parent, IList<Person> peopleList, IList<string> criteria)
        {
            return FirstNameMatches(parent, criteria) && IsFirstInstanceOfThisPerson(parent, peopleList) ? true : false;
        }

        private bool FirstNameMatches(RelationshipDto parent, IList<string> criteria)
        {
            return criteria.Contains(parent.FirstName) ? true : false;
        }

        private bool IsFirstInstanceOfThisPerson(RelationshipDto parent, IList<Person> peopleList)
        {
            //peopleList.Where(p => p.IsQualifiedParent == true).GroupBy(x => x.FirstName, (key, g) => g.OrderBy(e => e.Id).First());
            return peopleList.Where(p => p.FirstName == parent.FirstName).First().Id == parent.Id;
        }

        /// <summary>This method returns a list of all relationships that qualify based first instance of each input name </summary>
        public List<RelationshipDto> GetQualifiedPersonRelationshipDtos(IList<Person> persons, IList<RelationshipDto> relationshipList, IList<string> qualifiedParentNameList)
        {
            List<RelationshipDto> selectedParents = new List<RelationshipDto>();

            foreach (RelationshipDto p in relationshipList)
            {
                if (Validate(p, persons, qualifiedParentNameList))
                {
                    selectedParents.Add(p);
                }
            }

            return selectedParents;
        }

    }
}

using PeopleProcessor.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeopleProcessor
{
    public class QualifiedPersonValidator
    {
        /// <summary>
        /// specifies validity of output criteria
        /// </summary>
        /// <param name="person"></param>
        /// <param name="peopleList"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        protected bool Validate(RelationshipDto person, IList<Person> peopleList, IList<string> criteria)
        {
            return FirstNameMatches(person, criteria) && IsFirstInstanceOfThisPerson(person, peopleList) ? true : false;
        }

        private bool FirstNameMatches(RelationshipDto person, IList<string> criteria)
        {
            return criteria.Contains(person.FirstName) ? true : false;
        }

        private bool IsFirstInstanceOfThisPerson(RelationshipDto person, IList<Person> peopleList)
        {
            //peopleList.Where(p => p.IsQualifiedParent == true).GroupBy(x => x.FirstName, (key, g) => g.OrderBy(e => e.Id).First());
            return peopleList.Where(p => p.FirstName == person.FirstName).First().Id == person.Id;
        }

        /// <summary>
        /// Qualifies requsted first names
        /// </summary>
        /// <param name="peopleList"></param>
        /// <param name="relationshipList"></param>
        /// <param name="criteria"></param>
        /// <returns>
        /// Returns a list of all relationships that qualify based first instance of each input first name
        /// </returns>
        public List<RelationshipDto> GetQualifiedRelationshipDtos(IList<Person> peopleList, IList<RelationshipDto> relationshipList, IList<string> criteria)
        {
            List<RelationshipDto> selectedRelationships = new List<RelationshipDto>();

            foreach (RelationshipDto relationship in relationshipList)
            {
                if (Validate(relationship, peopleList, criteria))
                {
                    selectedRelationships.Add(relationship);
                }
            }

            return selectedRelationships;
        }

    }
}

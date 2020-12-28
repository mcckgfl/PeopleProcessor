using System.Collections.Generic;
using System.Linq;

namespace PeopleProcessor.Models
{
    public class RelationshipDto: Person
    {
        public IList<Person> Children { get; }

        public RelationshipDto(int personId, IList<Person> people)
        {
            Children = people.Where(p => p.ParentId == personId).ToList();
        }
    }
}

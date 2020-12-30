using MoreLinq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace PeopleProcessor.Models
{
    public class RelationshipDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ParentId { get; set; }
        public IList<Person> Children { get; }
        [JsonIgnore]
        public int RowId { get; set; }

        public RelationshipDto(int personId, IList<Person> people)
        {
            Children = people.Where(p => p.ParentId == personId)
                             .DistinctBy(c => c.Id)
                             .ToList();   
        }
    }
}

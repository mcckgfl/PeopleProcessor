
using Newtonsoft.Json;

namespace PeopleProcessor.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ParentId { get; set; }
        [JsonIgnore]
        public int RowId { get; set; }

    }
}

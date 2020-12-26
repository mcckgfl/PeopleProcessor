using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleProcessor.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ParentId { get; set; }
        public IList<PersonDto> Children { get; set; }
  
        public Parent(int personId, IList<PersonDto> people)
        {
            Children = people.Where(p => p.ParentId == personId).ToList();
        }
    }


}

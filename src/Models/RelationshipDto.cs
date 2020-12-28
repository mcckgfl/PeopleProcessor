﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleProcessor.Models
{
    public class RelationshipDto : Person
    {

        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public int ParentId { get; set; }
        public IList<Person> Children { get; set; }

        public RelationshipDto(int personId, IList<Person> people)
        {
            Children = people.Where(p => p.ParentId == personId).ToList();
        }

    }


}

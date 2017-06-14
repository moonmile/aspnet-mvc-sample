using System;
using System.Collections.Generic;

namespace SampleDBModelMvc.Models
{
    public partial class Prefecture
    {
        public Prefecture()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}

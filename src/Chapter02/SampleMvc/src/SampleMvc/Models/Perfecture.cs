using System;
using System.Collections.Generic;

namespace SampleMvc.Models
{
    public partial class Perfecture
    {
        public Perfecture()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}

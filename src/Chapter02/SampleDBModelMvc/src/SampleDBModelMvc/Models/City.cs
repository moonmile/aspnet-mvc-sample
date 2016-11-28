using System;
using System.Collections.Generic;

namespace SampleDBModelMvc.Models
{
    public partial class City
    {
        public City()
        {
            Author = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Author> Author { get; set; }
    }
}

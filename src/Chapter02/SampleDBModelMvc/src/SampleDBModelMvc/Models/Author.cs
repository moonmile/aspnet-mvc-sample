using System;
using System.Collections.Generic;

namespace SampleDBModelMvc.Models
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CityId { get; set; }

        public virtual ICollection<Book> Book { get; set; }
        public virtual City City { get; set; }
    }
}

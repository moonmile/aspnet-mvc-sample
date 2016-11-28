using System;
using System.Collections.Generic;

namespace SampleMvc.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Price { get; set; }

        public virtual Author Author { get; set; }
    }
}

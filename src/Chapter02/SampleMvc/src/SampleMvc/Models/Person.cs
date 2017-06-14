using System;
using System.Collections.Generic;

namespace SampleMvc.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int PrefectureId { get; set; }

        public virtual Prefecture Prefecture { get; set; }
    }
}

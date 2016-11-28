using System;
using System.Collections.Generic;

namespace SampleDBModelMvc.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int PerfectureId { get; set; }

        public virtual Perfecture Perfecture { get; set; }
    }
}

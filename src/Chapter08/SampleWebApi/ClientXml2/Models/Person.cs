using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApiXml.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int PerfectureId { get; set; }
        public Perfecture Perfecture { get; set; }
    }
    public class People
    {
        public List<Person> Items { get; set; }
    }
}

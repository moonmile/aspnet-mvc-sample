using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApiXml.Models
{
    public class Prefecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Prefectures
    {
        public List<Prefecture> Items { get; set; }
    }
}

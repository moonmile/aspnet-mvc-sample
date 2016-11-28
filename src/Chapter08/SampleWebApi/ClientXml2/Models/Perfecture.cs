using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApiXml.Models
{
    public class Perfecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Perfectures
    {
        public List<Perfecture> Items { get; set; }
    }
}

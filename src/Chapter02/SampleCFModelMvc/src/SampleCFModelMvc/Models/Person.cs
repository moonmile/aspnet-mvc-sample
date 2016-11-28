﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCFModelMvc.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        // Perfectureへ外部リンク
        public int PerfectureId { get; set; }
        public Perfecture Perfecture { get; set; }
    }
}

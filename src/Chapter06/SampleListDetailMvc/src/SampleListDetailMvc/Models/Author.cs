using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleListDetailMvc.Models
{
    /// <summary>
    /// 著者名
    /// </summary>
    public class Author
    {
        public int Id { get; set; }
        [Display(Name = "著者名")]
        public string Name { get; set; }
        [Display(Name = "年齢")]
        public int Age { get; set; }
        [Display(Name = "出身地")]
        public int PerfectureId { get; set; }

        public virtual ICollection<Book> Book { get; set; }
        public virtual Perfecture Perfecture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAnoModelMvc.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        // Perfectureへ外部リンク
        public int PerfectureId { get; set; }
        public Perfecture Perfecture { get; set; }

        // 入社日（日付）
        public DateTime Hireate { get; set; }
        // 出社
        public bool IsAttendance { get; set; }
        // Email
        public string Email { get; set; }
        // ブログページ(URL)
        public string Blog { get; set; }
        // 社員番号（正規表現) XXX-9999 形式
        public string EmployeeNo { get; set; }

    }
}

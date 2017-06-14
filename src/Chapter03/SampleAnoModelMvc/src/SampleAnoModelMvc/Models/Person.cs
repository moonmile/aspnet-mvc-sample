using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAnoModelMvc.Models
{

    public class Person0
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int PrefectureId { get; set; }
        public Prefecture Prefecture { get; set; }
        public DateTime? Hireate { get; set; }
        public bool IsAttendance { get; set; }
        public string Email { get; set; }
        public string Blog { get; set; }
        public string EmployeeNo { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        [Required]                                                  // 入力必須
        [MaxLength(20, ErrorMessage = "最大文字数は20文字までです")]   // 最大文字数を制限
        [Display(Name = "名前")]
        public string Name { get; set; }

        [Range(18, 100, ErrorMessage = "年齢は18歳から100歳までです")]   // 範囲制限
        [Display(Name = "年齢")]
        [DisplayFormat(DataFormatString = "{0} 歳")]
        public int? Age { get; set; }                               // NULLを有効にする

        // Prefectureへ外部リンク
        [Display(Name = "出身地")]
        public int PrefectureId { get; set; }
        public Prefecture Prefecture { get; set; }

        // 入社日（日付）
        [Display(Name = "入社日")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        [DataType(DataType.Date)]
        [ValidHireate]
        public DateTime? Hireate { get; set; }
        // 出社
        [Display(Name = "出社状態")]
        public bool IsAttendance { get; set; }
        // Email
        [Display(Name = "メールアドレス")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // ブログページ(URL)
        [Display(Name = "ブログのURL")]
        [DataType(DataType.Url)]
        public string Blog { get; set; }
        // 社員番号（正規表現) XXX-9999 形式
        [RegularExpression("[A-Z]{3}-[0-9]{4}")]
        [Display(Name = "社員番号")]
        public string EmployeeNo { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidHireate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = Convert.ToDateTime(value);
                if (date > DateTime.Now)
                {
                    return new ValidationResult("入社日は本日以前を指定してください");
                }
            }
            return ValidationResult.Success;
        }
    }
}

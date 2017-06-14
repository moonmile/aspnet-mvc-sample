using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlHelperViewMvc.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]                                                  
        [MaxLength(20, ErrorMessage = "最大文字数は20文字までです")]   
        [Display(Name = "名前")]
        public string Name { get; set; }

        [Range(18, 100, ErrorMessage = "年齢は18歳から100歳までです")]   
        [Display(Name = "年齢")]
        [DisplayFormat(DataFormatString = "{0} 歳")]
        public int? Age { get; set; }                               

        // Prefectureへ外部リンク
        [Display(Name = "出身地")]
        public int? PrefectureId { get; set; }
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
        [Required]
        [RegularExpression("[A-Z]{3}-[0-9]{4}")]
        [Display(Name = "社員番号")]
        public string EmployeeNo { get; set; }
    }

    /// <summary>
    /// 入社日用のカスタム検証
    /// </summary>
    public class ValidHireate : ValidationAttribute
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

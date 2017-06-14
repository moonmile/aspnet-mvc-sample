using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperViewMvc.Models
{
    public class Prefecture
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 初回のみ都道府県のデータを作る
        public static void Initialize(DbContext context)
        {
            var t = context.Set<Prefecture>();
            if (t.Any() == false)
            {
                // データを作る
                t.AddRange(
                    new Prefecture() { Name = "北海道" },
                    new Prefecture() { Name = "青森県" },
                    new Prefecture() { Name = "岩手県" },
                    new Prefecture() { Name = "宮城県" },
                    new Prefecture() { Name = "秋田県" },
                    new Prefecture() { Name = "山形県" },
                    new Prefecture() { Name = "福島県" },
                    new Prefecture() { Name = "茨城県" },
                    new Prefecture() { Name = "栃木県" },
                    new Prefecture() { Name = "群馬県" },
                    new Prefecture() { Name = "埼玉県" },
                    new Prefecture() { Name = "千葉県" },
                    new Prefecture() { Name = "東京都" },
                    new Prefecture() { Name = "神奈川県" },
                    new Prefecture() { Name = "新潟県" },
                    new Prefecture() { Name = "富山県" },
                    new Prefecture() { Name = "石川県" },
                    new Prefecture() { Name = "福井県" },
                    new Prefecture() { Name = "山梨県" },
                    new Prefecture() { Name = "長野県" },
                    new Prefecture() { Name = "岐阜県" },
                    new Prefecture() { Name = "静岡県" },
                    new Prefecture() { Name = "愛知県" },
                    new Prefecture() { Name = "三重県" },
                    new Prefecture() { Name = "滋賀県" },
                    new Prefecture() { Name = "京都府" },
                    new Prefecture() { Name = "大阪府" },
                    new Prefecture() { Name = "兵庫県" },
                    new Prefecture() { Name = "奈良県" },
                    new Prefecture() { Name = "和歌山県" },
                    new Prefecture() { Name = "鳥取県" },
                    new Prefecture() { Name = "島根県" },
                    new Prefecture() { Name = "岡山県" },
                    new Prefecture() { Name = "広島県" },
                    new Prefecture() { Name = "山口県" },
                    new Prefecture() { Name = "徳島県" },
                    new Prefecture() { Name = "香川県" },
                    new Prefecture() { Name = "愛媛県" },
                    new Prefecture() { Name = "高知県" },
                    new Prefecture() { Name = "福岡県" },
                    new Prefecture() { Name = "佐賀県" },
                    new Prefecture() { Name = "長崎県" },
                    new Prefecture() { Name = "熊本県" },
                    new Prefecture() { Name = "大分県" },
                    new Prefecture() { Name = "宮崎県" },
                    new Prefecture() { Name = "鹿児島県" },
                    new Prefecture() { Name = "沖縄県" });
                context.SaveChanges();
            }
        }
    }
}

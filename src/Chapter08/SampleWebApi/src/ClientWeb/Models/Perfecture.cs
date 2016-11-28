using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Models
{
public class Perfecture
{
    public int Id { get; set; }
    public string Name { get; set; }

    // 初回のみ都道府県のデータを作る
    public static void Initialize(DbContext context)
    {
        var t = context.Set<Perfecture>();
        if (t.Any() == false)
        {
            // データを作る
            t.AddRange(
                new Perfecture() { Name = "北海道" },
                new Perfecture() { Name = "青森県" },
                new Perfecture() { Name = "岩手県" },
                new Perfecture() { Name = "宮城県" },
                new Perfecture() { Name = "秋田県" },
                new Perfecture() { Name = "山形県" },
                new Perfecture() { Name = "福島県" },
                new Perfecture() { Name = "茨城県" },
                new Perfecture() { Name = "栃木県" },
                new Perfecture() { Name = "群馬県" },
                new Perfecture() { Name = "埼玉県" },
                new Perfecture() { Name = "千葉県" },
                new Perfecture() { Name = "東京都" },
                new Perfecture() { Name = "神奈川県" },
                new Perfecture() { Name = "新潟県" },
                new Perfecture() { Name = "富山県" },
                new Perfecture() { Name = "石川県" },
                new Perfecture() { Name = "福井県" },
                new Perfecture() { Name = "山梨県" },
                new Perfecture() { Name = "長野県" },
                new Perfecture() { Name = "岐阜県" },
                new Perfecture() { Name = "静岡県" },
                new Perfecture() { Name = "愛知県" },
                new Perfecture() { Name = "三重県" },
                new Perfecture() { Name = "滋賀県" },
                new Perfecture() { Name = "京都府" },
                new Perfecture() { Name = "大阪府" },
                new Perfecture() { Name = "兵庫県" },
                new Perfecture() { Name = "奈良県" },
                new Perfecture() { Name = "和歌山県" },
                new Perfecture() { Name = "鳥取県" },
                new Perfecture() { Name = "島根県" },
                new Perfecture() { Name = "岡山県" },
                new Perfecture() { Name = "広島県" },
                new Perfecture() { Name = "山口県" },
                new Perfecture() { Name = "徳島県" },
                new Perfecture() { Name = "香川県" },
                new Perfecture() { Name = "愛媛県" },
                new Perfecture() { Name = "高知県" },
                new Perfecture() { Name = "福岡県" },
                new Perfecture() { Name = "佐賀県" },
                new Perfecture() { Name = "長崎県" },
                new Perfecture() { Name = "熊本県" },
                new Perfecture() { Name = "大分県" },
                new Perfecture() { Name = "宮崎県" },
                new Perfecture() { Name = "鹿児島県" },
                new Perfecture() { Name = "沖縄県" });
            context.SaveChanges();
        }
    }
}
}

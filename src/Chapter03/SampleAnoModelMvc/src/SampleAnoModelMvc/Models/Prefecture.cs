using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAnoModelMvc.Models
{
public class Prefecture
{
    public int Id { get; set; }
    public string Name { get; set; }

        // 初回のみ都道府県のデータを作る
        public static void Initialize( DbContext context )
        {
            var t = context.Set<Prefecture>();
            if ( t.Any() == false )
            {
                // データを作る
                t.AddRange(
                    new Prefecture() { Name = "北海道" },
                    new Prefecture() { Name = "青森県" },
                    new Prefecture() { Name = "岩手県" },
                    new Prefecture() { Name = "宮城県" },
                    new Prefecture() { Name = "秋田県" },
                    new Prefecture() { Name = "山形県" },
                    new Prefecture() { Name = "福島県" });
                context.SaveChanges();
            }
        }
    }
}

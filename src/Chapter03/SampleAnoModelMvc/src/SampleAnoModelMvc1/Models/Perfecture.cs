using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAnoModelMvc.Models
{
    public class Perfecture
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 初回のみ都道府県のデータを作る
        public static void Initialize( DbContext context )
        {
            var t = context.Set<Perfecture>();
            if ( t.Any() == false )
            {
                // データを作る
                t.AddRange(
                    new Perfecture() { Name = "北海道" },
                    new Perfecture() { Name = "青森県" },
                    new Perfecture() { Name = "岩手県" },
                    new Perfecture() { Name = "宮城県" },
                    new Perfecture() { Name = "秋田県" },
                    new Perfecture() { Name = "山形県" },
                    new Perfecture() { Name = "福島県" });
                context.SaveChanges();
            }
        }
    }
}

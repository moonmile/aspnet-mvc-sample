using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleListDetailMvc.Models;

namespace SampleListDetailMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Book> Book { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public void Seed()
        {
            try
            {
                if (Publisher.Any())
                    return;
                Publisher.AddRange(
                    new Publisher { Name = "日経BP"},
                    new Publisher { Name = "A出版" },
                    new Publisher { Name = "B出版" },
                    new Publisher { Name = "C出版" },

                    new Publisher { Name = "共立出版" },
                    new Publisher { Name = "ソフトバンククリエイティブ" },
                    new Publisher { Name = "ピアソンエデュケーション" }
                    );
                Author.AddRange(
                    new Author { Name = "増田 智明", Age = 49, PrefectureId = 1 },
                    new Author { Name = "○田 太郎", Age = 20, PrefectureId = 2 },
                    new Author { Name = "×田 次郎", Age = 30, PrefectureId = 3 },
                    new Author { Name = "ジェラルド・ワインバーグ", Age = 0, PrefectureId = 4 },
                    new Author { Name = "トム・デマルコ", Age = 0, PrefectureId = 4 },
                    new Author { Name = "カーニハン＆リッチー", Age = 0, PrefectureId = 4 },
                    new Author { Name = "ストラウストラップ", Age = 0, PrefectureId = 4 },
                    new Author { Name = "フレデリック・ブルックス・ジュニア", Age = 0, PrefectureId = 4 }
                    );
                this.SaveChanges();

                Book.AddRange(
                    new Book { AuthorId = 1, PublisherId = 1, Title = "ASP.NET MVC 入門", ISBN = "978-4-82229-888-3", Price = 2000 },
                    new Book { AuthorId = 1, PublisherId = 1, Title = "Xamarin 入門", ISBN = "978-4-82229-834-0", Price = 3200 },
                    new Book { AuthorId = 2, PublisherId = 2, Title = "はじめての C#", ISBN = "000-0-0000-000-0", Price = 3200 },
                    new Book { AuthorId = 2, PublisherId = 2, Title = "はじめての F#",  ISBN = "001-0-0000-000-0", Price = 3200 },
                    new Book { AuthorId = 3, PublisherId = 3, Title = "はじめてのクラシックギター", ISBN = "003-0-0000-000-0", Price = 3200 },
                    new Book { AuthorId = 3, PublisherId = 3, Title = "はじめてのボサノバギター", ISBN = "003-1-0000-000-0", Price = 3200 },
                    new Book { AuthorId = 3, PublisherId = 2, Title = "誰でも学べる C#", ISBN = "004-0-0000-000-0", Price = 2000 },
                    new Book { AuthorId = 3, PublisherId = 2, Title = "誰でも学べる VB", ISBN = "005-0-0000-000-0", Price = 2000 },

                    new Book { AuthorId = 4, PublisherId = 1, Title = "コンサルタントの道具箱", ISBN = "978-4822281724", Price = 2000 },
                    new Book { AuthorId = 5, PublisherId = 1, Title = "ピープルウェア", ISBN = "978-4822285241", Price = 2000 },
                    new Book { AuthorId = 6, PublisherId = 5, Title = "プログラム言語C", ISBN = "978-4320026926", Price = 2000 },
                    new Book { AuthorId = 7, PublisherId = 6, Title = "C++の設計と進化", ISBN = "978-4797328547", Price = 2000 },
                    new Book { AuthorId = 8, PublisherId = 7, Title = "人月の神話", ISBN = "978-4894716650", Price = 2000 }
                    );
                this.SaveChanges();
            }
            catch
            {

            }

        }
    }
}

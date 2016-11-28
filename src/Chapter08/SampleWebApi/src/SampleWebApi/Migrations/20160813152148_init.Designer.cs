using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleWebApi.Data;

namespace SampleWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160813152148_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleWebApi.Models.Perfecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Perfecture");
                });

            modelBuilder.Entity("SampleWebApi.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.Property<int>("PerfectureId");

                    b.HasKey("Id");

                    b.HasIndex("PerfectureId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("SampleWebApi.Models.Person", b =>
                {
                    b.HasOne("SampleWebApi.Models.Perfecture", "Perfecture")
                        .WithMany()
                        .HasForeignKey("PerfectureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

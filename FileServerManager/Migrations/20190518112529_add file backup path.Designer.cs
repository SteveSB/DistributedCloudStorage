﻿// <auto-generated />
using FileServerManager.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileServerManager.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190518112529_add file backup path")]
    partial class addfilebackuppath
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileServerManager.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BackupPath")
                        .IsRequired();

                    b.Property<int>("BackupServer");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Owner")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int>("ServerId");

                    b.Property<double>("Size");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileServerManager.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Size");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("FileServerManager.Models.File", b =>
                {
                    b.HasOne("FileServerManager.Models.Server", "Server")
                        .WithMany("Files")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using DynamicRoutine.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DynamicRoutine.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190316075919_AddEntity_RoutineForm_Migration")]
    partial class AddEntity_RoutineForm_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DynamicRoutine.Entities.Routine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.Property<string>("TitleEn");

                    b.HasKey("Id");

                    b.ToTable("Routines");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineDashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("MultiUser");

                    b.Property<int>("RoutineId");

                    b.Property<int>("StartStep");

                    b.Property<string>("Title");

                    b.Property<string>("TitleEn");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineDashboards");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FieldTypeDes")
                        .HasMaxLength(100);

                    b.Property<bool>("IsRequide");

                    b.Property<int>("RoutineId");

                    b.Property<string>("Title")
                        .HasMaxLength(300);

                    b.Property<string>("TitleEn")
                        .HasMaxLength(300);

                    b.Property<int>("Type");

                    b.Property<string>("Url")
                        .HasMaxLength(700);

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineFields");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FieldJson");

                    b.Property<int>("RoutineId");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineForms");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Action");

                    b.Property<int>("EntityId");

                    b.Property<int>("FromStep");

                    b.Property<int>("RoutineId");

                    b.Property<int?>("ToStep");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineLog");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Action");

                    b.Property<int>("FromStep");

                    b.Property<int>("RoutineId");

                    b.Property<int?>("ToStep");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineSteps");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineDashboard", b =>
                {
                    b.HasOne("DynamicRoutine.Entities.Routine", "Routine")
                        .WithMany("Dashboards")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineField", b =>
                {
                    b.HasOne("DynamicRoutine.Entities.Routine", "Routine")
                        .WithMany("Fields")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineForm", b =>
                {
                    b.HasOne("DynamicRoutine.Entities.Routine", "Routine")
                        .WithMany("Forms")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineLog", b =>
                {
                    b.HasOne("DynamicRoutine.Entities.Routine", "Routine")
                        .WithMany("Logs")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DynamicRoutine.Entities.RoutineStep", b =>
                {
                    b.HasOne("DynamicRoutine.Entities.Routine", "Routine")
                        .WithMany("Steps")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

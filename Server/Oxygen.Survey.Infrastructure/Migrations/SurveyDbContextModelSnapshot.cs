﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oxygen.Survey.Infrastructure.Persistence;

namespace Oxygen.Survey.Infrastructure.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Oxygen.Infrastructure.Common.Persistence.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serializedData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.EmployeeSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSubmitted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("EmployeeSurveys");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.EmployeeSurveyItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeSurveyId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeSurveyId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionItemId");

                    b.ToTable("EmployeeSurveyItems");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<bool>("IsRequired")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.QuestionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionItems");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.QuestionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int?>("SurveyTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyTypeId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.SurveyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("SurveyTypes");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.EmployeeSurvey", b =>
                {
                    b.HasOne("Oxygen.Survey.Domain.Models.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.EmployeeSurveyItem", b =>
                {
                    b.HasOne("Oxygen.Survey.Domain.Models.EmployeeSurvey", null)
                        .WithMany("EmployeeSurveyItems")
                        .HasForeignKey("EmployeeSurveyId");

                    b.HasOne("Oxygen.Survey.Domain.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Oxygen.Survey.Domain.Models.QuestionItem", "QuestionItem")
                        .WithMany()
                        .HasForeignKey("QuestionItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.Question", b =>
                {
                    b.HasOne("Oxygen.Survey.Domain.Models.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Oxygen.Survey.Domain.Models.Survey", null)
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.QuestionItem", b =>
                {
                    b.HasOne("Oxygen.Survey.Domain.Models.Question", null)
                        .WithMany("QuestionItems")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Oxygen.Survey.Domain.Models.Survey", b =>
                {
                    b.HasOne("Oxygen.Survey.Domain.Models.SurveyType", "SurveyType")
                        .WithMany()
                        .HasForeignKey("SurveyTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

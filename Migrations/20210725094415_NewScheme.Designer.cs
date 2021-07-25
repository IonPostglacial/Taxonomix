﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taxonomix.Data;

namespace Taxonomix.Migrations
{
    [DbContext(typeof(TaxonomyContext))]
    [Migration("20210725094415_NewScheme")]
    partial class NewScheme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Taxonomix.Data.BookReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Detail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fasc")
                        .HasColumnType("TEXT");

                    b.Property<int>("Page")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaxonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TaxonId");

                    b.ToTable("BookReference");
                });

            modelBuilder.Entity("Taxonomix.Data.Character", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("Taxonomix.Data.Dataset", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Datasets");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Character>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DatasetId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Hierarchy<Character>Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.HasIndex("EntryId");

                    b.HasIndex("Hierarchy<Character>Id");

                    b.ToTable("Hierarchy<Character>");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Taxon>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DatasetId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Hierarchy<Taxon>Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.HasIndex("EntryId");

                    b.HasIndex("Hierarchy<Taxon>Id");

                    b.ToTable("Hierarchy<Taxon>");
                });

            modelBuilder.Entity("Taxonomix.Data.ItemName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Chinese")
                        .HasColumnType("TEXT");

                    b.Property<string>("English")
                        .HasColumnType("TEXT");

                    b.Property<string>("French")
                        .HasColumnType("TEXT");

                    b.Property<string>("Scientific")
                        .HasColumnType("TEXT");

                    b.Property<string>("Vernacular")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemName");
                });

            modelBuilder.Entity("Taxonomix.Data.Picture", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Legend")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("StateId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaxonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("StateId");

                    b.HasIndex("TaxonId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("Taxonomix.Data.State", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CharacterId1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NameId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaxonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("CharacterId1");

                    b.HasIndex("NameId");

                    b.HasIndex("TaxonId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("Taxonomix.Data.Taxon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NameId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Taxon");
                });

            modelBuilder.Entity("Taxonomix.Data.BookReference", b =>
                {
                    b.HasOne("Taxonomix.Data.Taxon", null)
                        .WithMany("BookReferences")
                        .HasForeignKey("TaxonId");
                });

            modelBuilder.Entity("Taxonomix.Data.Character", b =>
                {
                    b.HasOne("Taxonomix.Data.ItemName", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Character>", b =>
                {
                    b.HasOne("Taxonomix.Data.Dataset", null)
                        .WithMany("Characters")
                        .HasForeignKey("DatasetId");

                    b.HasOne("Taxonomix.Data.Character", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId");

                    b.HasOne("Taxonomix.Data.Hierarchy<Taxonomix.Data.Character>", null)
                        .WithMany("Children")
                        .HasForeignKey("Hierarchy<Character>Id");

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Taxon>", b =>
                {
                    b.HasOne("Taxonomix.Data.Dataset", null)
                        .WithMany("Taxons")
                        .HasForeignKey("DatasetId");

                    b.HasOne("Taxonomix.Data.Taxon", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId");

                    b.HasOne("Taxonomix.Data.Hierarchy<Taxonomix.Data.Taxon>", null)
                        .WithMany("Children")
                        .HasForeignKey("Hierarchy<Taxon>Id");

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Taxonomix.Data.Picture", b =>
                {
                    b.HasOne("Taxonomix.Data.Character", null)
                        .WithMany("Pictures")
                        .HasForeignKey("CharacterId");

                    b.HasOne("Taxonomix.Data.State", null)
                        .WithMany("Pictures")
                        .HasForeignKey("StateId");

                    b.HasOne("Taxonomix.Data.Taxon", null)
                        .WithMany("Pictures")
                        .HasForeignKey("TaxonId");
                });

            modelBuilder.Entity("Taxonomix.Data.State", b =>
                {
                    b.HasOne("Taxonomix.Data.Character", null)
                        .WithMany("RequiredStates")
                        .HasForeignKey("CharacterId");

                    b.HasOne("Taxonomix.Data.Character", null)
                        .WithMany("States")
                        .HasForeignKey("CharacterId1");

                    b.HasOne("Taxonomix.Data.ItemName", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");

                    b.HasOne("Taxonomix.Data.Taxon", null)
                        .WithMany("States")
                        .HasForeignKey("TaxonId");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Taxonomix.Data.Taxon", b =>
                {
                    b.HasOne("Taxonomix.Data.ItemName", "Name")
                        .WithMany()
                        .HasForeignKey("NameId");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Taxonomix.Data.Character", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("RequiredStates");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Taxonomix.Data.Dataset", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Taxons");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Character>", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Taxonomix.Data.Hierarchy<Taxonomix.Data.Taxon>", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Taxonomix.Data.State", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Taxonomix.Data.Taxon", b =>
                {
                    b.Navigation("BookReferences");

                    b.Navigation("Pictures");

                    b.Navigation("States");
                });
#pragma warning restore 612, 618
        }
    }
}

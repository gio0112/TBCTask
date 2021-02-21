﻿// <auto-generated />
using System;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<int?>("AttachmentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderID")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonTypeID")
                        .HasColumnType("int");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("AttachmentID");

                    b.HasIndex("GenderID");

                    b.HasIndex("PersonTypeID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Domain.Entities.PersonRelation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<int>("PersonTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ContactID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PersonTypeID");

                    b.ToTable("PersonRelations");
                });

            modelBuilder.Entity("Domain.Entities.PersonType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PersonTypes");
                });

            modelBuilder.Entity("Domain.Entities.Phone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<int>("PhoneTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PhoneTypeID");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Domain.Entities.PhoneType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PhoneTypes");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.HasOne("Domain.Entities.City", "Address")
                        .WithMany("People")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Attachment", "Attachment")
                        .WithMany("People")
                        .HasForeignKey("AttachmentID");

                    b.HasOne("Domain.Entities.Gender", "Gender")
                        .WithMany("People")
                        .HasForeignKey("GenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PersonType", null)
                        .WithMany("People")
                        .HasForeignKey("PersonTypeID");

                    b.Navigation("Address");

                    b.Navigation("Attachment");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Domain.Entities.PersonRelation", b =>
                {
                    b.HasOne("Domain.Entities.Person", "People")
                        .WithMany("PersonRelations")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Person", "Contacts")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PersonType", "Type")
                        .WithMany("PersonRelations")
                        .HasForeignKey("PersonTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contacts");

                    b.Navigation("People");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.Entities.Phone", b =>
                {
                    b.HasOne("Domain.Entities.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PhoneType", "Type")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.Entities.Attachment", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Entities.Gender", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Navigation("PersonRelations");

                    b.Navigation("Phones");
                });

            modelBuilder.Entity("Domain.Entities.PersonType", b =>
                {
                    b.Navigation("People");

                    b.Navigation("PersonRelations");
                });

            modelBuilder.Entity("Domain.Entities.PhoneType", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Gender)
                .WithMany(b => b.People)
                .HasForeignKey(e => e.GenderID);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithMany(b => b.People)
                .HasForeignKey(e => e.AddressID);
            
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Attachment)
                .WithMany(b => b.People)
                .HasForeignKey(e => e.AttachmentID);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(p => p.People)
                .WithMany(b => b.PersonRelations)
                .HasForeignKey(e => e.PersonID);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(p => p.People)
                .WithMany(b => b.PersonRelations)
                .HasForeignKey(e => e.ContactID);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(p => p.Type)
                .WithMany(b => b.PersonRelations)
                .HasForeignKey(e => e.PersonTypeID);

            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Person)
                .WithMany(b => b.Phones)
                .HasForeignKey(e => e.PersonID);

            modelBuilder.Entity<Phone>()
                .HasOne(p => p.Type)
                .WithMany(b => b.Phones)
                .HasForeignKey(e => e.PhoneTypeID);
        }
    }
}


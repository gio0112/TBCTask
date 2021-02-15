using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        void Dispose();
        IRepository<Attachment> Attachments { get; }
        IRepository<City> Cities { get; }
        IRepository<Gender> Genders { get; }
        IRepository<Person> Persons { get; }
        IRepository<PersonRelation> PersonRelations { get; }
        IRepository<PersonType> PersonTypes { get; }
        IRepository<Phone> Phones { get; }
        IRepository<PhoneType> PhoneTypes { get; }
    }
}

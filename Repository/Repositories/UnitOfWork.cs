using Domain.Data;
using Domain.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public IRepository<Attachment> Attachments => new Repository<Attachment>(_context);

        public IRepository<City> Cities => new Repository<City>(_context);

        public IRepository<Gender> Genders => new Repository<Gender>(_context);

        public IRepository<Person> Persons => new Repository<Person>(_context);

        public IRepository<PersonRelation> PersonRelations => new Repository<PersonRelation>(_context);

        public IRepository<PersonType> PersonTypes => new Repository<PersonType>(_context);

        public IRepository<Phone> Phones => new Repository<Phone>(_context);

        public IRepository<PhoneType> PhoneTypes => new Repository<PhoneType>(_context);



        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

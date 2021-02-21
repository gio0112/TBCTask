using AutoMapper;
using Domain.Common;
using Domain.Data;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Service.Services
{
    public class PersonService : IService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public PersonService(IUnitOfWork uow, IMapper mapper, IHostEnvironment env)
        {
            _uow = uow;
            _mapper = mapper;
            _env = env;
        }
        public async Task<PersonResult> GetAll(int page)
        {
            PageParameters parameters = new PageParameters
            {
                PageNumber = page  == 0 ? 1 : page
            };

            var persons = await _uow.Persons.GetAll()
                .Include(x=>x.Gender)
                .Include(x => x.Address)
                .Include(x => x.Attachment)
                .Include(x => x.Phones)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToListAsync();

            double pageCount = _uow.Persons.GetAll().Count() / (double)parameters.PageSize;

            var model = new PersonResult
            {
                Persons = _mapper.Map<List<PersonDTO>>(persons),
                CurrentPage = parameters.PageNumber,
                PageCount = (int)Math.Ceiling(pageCount)
            };

            return model;
        }
        public async Task<PersonAddEditDTO> Add(PersonAddEditDTO model)
        {
            try
            {
                
                var person = _mapper.Map<Person>(model);
                _uow.Persons.Insert(person);
                _uow.Save();

                return _mapper.Map<PersonAddEditDTO>(person);
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public async Task<PersonDTO> Get(int id)
        {
            Person person = await _uow.Persons.Search(x => x.ID == id)
                .Include(x => x.Gender)
                .Include(x => x.Address)
                .Include(x => x.Attachment)
                .Include(x => x.Phones)
                .FirstOrDefaultAsync();

            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<PersonResult> Search(string filter, int page)
        {
            PageParameters parameters = new PageParameters
            {
                PageNumber = page == 0 ? 1 : page
            };

            var personWithFilter =_uow.Persons.Search(
                    x => x.FirstName.Contains(filter) ||
                    x.LastName.Contains(filter) ||
                    x.PersonalNumber.Contains(filter)
                );
            

            var persons = await personWithFilter 
                .Include(x => x.Gender)
                .Include(x => x.Address)
                .Include(x => x.Attachment)
                .Include(x => x.Phones)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToListAsync();

            double pageCount = _uow.Persons.GetAll().Count() / (double)parameters.PageSize;

            return new PersonResult
                {
                    Persons = _mapper.Map<List<PersonDTO>>(persons),
                    CurrentPage = parameters.PageNumber,
                    PageCount = (int)Math.Ceiling(pageCount)
            };
        }

        public async Task<PersonAddEditDTO> Edit(PersonAddEditDTO model)
        {
            var person = _mapper.Map<Person>(model);
            _uow.Persons.Update(person);
            _uow.Save();
            return _mapper.Map<PersonAddEditDTO>(person);
        }

        public async Task<int> Delete(int id)
        {
            _uow.Persons.Delete(id);
            _uow.Save();
            return id;
        }

        public async Task<AttachmentDTO> FileUpload(IFormFile file)
        {

            string path = _env.ContentRootPath + "\\Uploads\\";
            if (file != null)
            {

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using(FileStream fileStream = File.Create(path + file.FileName))
                {
                    file.CopyToAsync(fileStream);
                    fileStream.FlushAsync();
                }
            }

            var model = new Attachment
            {
                Title = file.FileName,
                Url = path + file.FileName
            };
            _uow.Attachments.Insert(model);
            _uow.Save();

            return _mapper.Map<AttachmentDTO>(model);
        }

        public async Task<PersonContactResultDTO> GetConcats(int id, int page)
        {
            PageParameters parameters = new PageParameters
            {
                PageNumber = page == 0 ? 1 : page
            };

            var person = await _uow.Persons.Search(x => x.ID == id)
                .Include(x => x.Gender)
                .Include(x => x.Address)
                .Include(x => x.Attachment)
                .Include(x => x.Phones)
                .FirstOrDefaultAsync();

            var contacts = await _uow.PersonRelations.Search(x => x.PersonID == id)
                .Include(t => t.Type)
                .Include(p => p.People)
                .ThenInclude(a => a.Attachment)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Select(x => new PersonContactDTO { 
                    ID = x.People.ID,
                    FirstName = x.People.FirstName,
                    LastName = x.People.LastName,
                    PersonalNumber = x.People.PersonalNumber,
                    BirthDay = x.People.BirthDay,
                    Attachment = _mapper.Map<AttachmentDTO>(x.People.Attachment),
                    PersonType = _mapper.Map<PersonTypeDTO>(x.Type)
                })
                .ToListAsync();

            var personTypes = _uow.PersonTypes.GetAll();

            var selectPersons = await _uow.Persons.Search(x => x.ID != id)
                .Select(x => new SelectPersonsDTO{
                    ID = x.ID,
                    FullName =$"{x.FirstName} {x.LastName} {x.PersonalNumber}"
                })
                .ToListAsync();

            double pageCount = contacts.Count() / (double)parameters.PageSize;

            return new PersonContactResultDTO
            {
                Person = _mapper.Map<PersonDTO>(person),
                Contacts = _mapper.Map<List<PersonContactDTO>>(contacts),
                PersonTypes = _mapper.Map<List<PersonTypeDTO>>(personTypes),
                SelectPersons = _mapper.Map<List<SelectPersonsDTO>>(selectPersons),
                CurrentPage = parameters.PageNumber,
                PageCount = (int)Math.Ceiling(pageCount)
            };

        }

        public async Task<PersonRelationDTO> AddContact(PersonRelationDTO model)
        {
            
            var contact = new PersonRelation
            {
                PersonID = model.PersonID,
                ContactID = model.ContactID,
                PersonTypeID = model.PersonTypeID
            };
            _uow.PersonRelations.Insert(contact);
            _uow.Save();

            return _mapper.Map<PersonRelationDTO>(contact);
            

        }
    }
}

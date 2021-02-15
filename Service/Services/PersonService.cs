using AutoMapper;
using Domain.Common;
using Domain.Data;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
                PageNumber = page
            };



            var persons = _uow.Persons.GetAll()
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

             var model = new PersonResult
                {
                    Persons = _mapper.Map<List<PersonDTO>>(persons),
                    CurrentPage = parameters.PageNumber,
                    PageCount = _uow.Persons.GetAll().Count() / parameters.PageSize
                };

            return model;
        }
        public async Task<PersonAddEditDTO> Add(PersonAddEditDTO model)
        {
            if(model.File != null)
            {
                var file = await FileUpload(model.File);
                model.AvatarID = file.ID;
            }

            var person = _mapper.Map<Person>(model);
            _uow.Persons.Insert(person);
            _uow.Save();
            return _mapper.Map<PersonAddEditDTO>(person);
        }

        public async Task<PersonDTO> Get(int id) => _mapper.Map<PersonDTO>(_uow.Persons.GetByID(id));

        public async Task<PersonResult> Search(string filter, int page)
        {
            PageParameters parameters = new PageParameters
            {
                PageNumber = page
            };

            var personWithFilter =_uow.Persons.Search(
                    x => x.FirstName.Contains(filter) ||
                    x.LastName.Contains(filter) ||
                    x.PersonalNumber.Contains(filter)
                );
            

            var persons = personWithFilter
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToList();

            return new PersonResult
                {
                    Persons = _mapper.Map<List<PersonDTO>>(persons),
                    CurrentPage = parameters.PageNumber,
                    PageCount = personWithFilter.Count() / parameters.PageSize
            };
        }

        public async Task<PersonAddEditDTO> Edit(PersonAddEditDTO model)
        {
            if (model.File.Length > 0)
            {
                var file = await FileUpload(model.File);
                model.AvatarID = file.ID;
            }

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

            string path = _env.ContentRootPath + "Uploads";
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
                Url = path
            };
            _uow.Attachments.Insert(model);
            _uow.Save();

            return _mapper.Map<AttachmentDTO>(model);
        }
    }
}

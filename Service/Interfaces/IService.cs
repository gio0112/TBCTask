using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IService
    {
        Task<PersonResult> GetAll(int page);
        Task<PersonDTO> Get(int id);
        Task<PersonResult> Search(string filter, int page);
        Task<PersonAddEditDTO> Add(PersonAddEditDTO model);
        Task<PersonAddEditDTO> Edit(PersonAddEditDTO model);
        Task<int> Delete(int id);
        Task<AttachmentDTO> FileUpload(IFormFile file);
        Task<PersonContactResultDTO> GetConcats(int id, int page);
        Task<PersonRelationDTO> AddContact(PersonRelationDTO model);
    }
}

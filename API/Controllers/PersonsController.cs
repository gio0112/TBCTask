using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class PersonsController : BaseApiController
    {
        private readonly IService _service;

        public PersonsController(IService person)
        {
            _service = person;
        }

        // GET: api/<PersonsController>
        [HttpGet("GetAll")]
        public async Task<ActionResult<PersonResult>> GetAll(int page) => await _service.GetAll(page);

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> Get(int id) => await _service.Get(id);
        [HttpGet("Search/{filter}")]
        public async Task<ActionResult<PersonResult>> Search(string filter, int page) => await _service.Search(filter, page);
        // POST api/<PersonsController>
        [HttpPost("Add")]
        public async Task<ActionResult<PersonAddEditDTO>> Add([FromBody] PersonAddEditDTO model) => await _service.Add(model);

        // POST api/<PersonsController>
        [HttpPost("Edit")]
        public async Task<ActionResult<PersonAddEditDTO>> Edit([FromBody] PersonAddEditDTO model) => await _service.Edit(model);
        [HttpPost("FileUpload")]
        public async Task<ActionResult<AttachmentDTO>> FileUpload(IFormFile file) => await _service.FileUpload(file);

        // DELETE api/<PersonsController>/5
        [HttpGet("Delete/{id}")]
        public async Task<int> Delete(int id) => await _service.Delete(id);
        [HttpGet("GetConcats/{id}")]
        public async Task<ActionResult<PersonContactResultDTO>> GetConcats(int id, int page) => await _service.GetConcats(id, page);
        [HttpPost("AddContact")]
        public async Task<ActionResult<PersonRelationDTO>> AddContact([FromBody] PersonRelationDTO model) => await _service.AddContact(model);

    }
}

using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class GenderController : BaseApiController
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        // GET: api/<GenderController>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<GenderDTO>> GetAll() => await _genderService.GetAll();

        
    }
}

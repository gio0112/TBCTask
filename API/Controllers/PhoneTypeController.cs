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
    public class PhoneTypeController : BaseApiController
    {
        private readonly IPhoneTypeService _phoneTypeService;

        public PhoneTypeController(IPhoneTypeService phoneTypeService)
        {
            _phoneTypeService = phoneTypeService;
        }
        
        [HttpGet("GetAll")]
        public async Task<IEnumerable<PhoneTypeDTO>> GetAll() => await _phoneTypeService.GetAll();
    }
}

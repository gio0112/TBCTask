using AutoMapper;
using Domain.Dto;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PhoneTypeService : IPhoneTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PhoneTypeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PhoneTypeDTO>> GetAll() => _mapper.Map<IEnumerable<PhoneTypeDTO>>(_uow.PhoneTypes.GetAll());
    }
}

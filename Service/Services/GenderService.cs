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
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GenderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenderDTO>> GetAll() => _mapper.Map<IEnumerable<GenderDTO>>(_uow.Genders.GetAll());

    }
}

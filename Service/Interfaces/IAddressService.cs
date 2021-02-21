﻿using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<CityDTO>> GetAll();
    }
}

using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGenderService
    {
        Task<IEnumerable<GenderDTO>> GetAll();
    }
}

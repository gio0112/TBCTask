using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPhoneTypeService
    {
        Task<IEnumerable<PhoneTypeDTO>> GetAll();
    }
}

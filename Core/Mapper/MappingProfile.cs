using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Attachment
            CreateMap<Attachment, AttachmentDTO>();
            CreateMap<AttachmentDTO, Attachment>();

            //Address
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            //Gender
            CreateMap<Gender, GenderDTO>();
            CreateMap<GenderDTO, Gender>();

            //Person
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            //Person Add Or Edit
            CreateMap<Person, PersonAddEditDTO>();
            CreateMap<PersonAddEditDTO, Person>();

            //PersonRelation
            CreateMap<PersonRelation, PersonRelationDTO>();
            CreateMap<PersonRelationDTO, PersonRelation>();

            //PersonType
            CreateMap<PersonType, PersonTypeDTO>();
            CreateMap<PersonTypeDTO, PersonType>();

            //Phone
            CreateMap<Phone, PhoneDTO>();
            CreateMap<PhoneDTO, Phone>();

            //PhoneType
            CreateMap<PhoneType, PhoneTypeDTO>();
            CreateMap<PhoneTypeDTO, PhoneType>();
        }
    }
}

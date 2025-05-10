﻿using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers
{
    public class ApplicationUserToUserDTOMappingProfile : Profile
    {
        public ApplicationUserToUserDTOMappingProfile()
        {
            // Create a mapping configuration between ApplicationUser and UserDTO
            CreateMap<ApplicationUser, UserDTO>().
                ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)).
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).
                ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName)).
                ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
        }
    }
}

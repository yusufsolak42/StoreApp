using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertion, Product>(); //we created the map. It sits between source and destination points.  the source (ProductDtoForInsertion) and destination (Product).
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap(); //we created the map. It sits between source and destination points.  the source (ProductDtoForUpdate) and destination (Product).
             //we use ReverMap because i might go from destination to source.

             CreateMap<UserDtoForCreation, IdentityUser>();
             CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap();


        }
    }
}

//MappingProfile class serves as a central place to define your mapping configurations
//using AutoMapper

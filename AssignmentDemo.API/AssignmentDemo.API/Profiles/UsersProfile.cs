using AssignmentDemo.API.Models.Users;
using AssignmentDemo.Entities.API.UserDetails;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Profiles
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<Geo, GeoDto>();
        }
    }
}

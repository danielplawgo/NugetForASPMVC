using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NugetForAspMvc.Models;
using NugetForAspMvc.ViewModels.Users;

namespace NugetForAspMvc.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<User, CreateEmail>();
        }
    }
}
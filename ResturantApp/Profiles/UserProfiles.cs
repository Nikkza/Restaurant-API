﻿using AutoMapper;
using RestaurantApp.Dtos.UserDto;
using RestaurantApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<UserInfo, UserReadDto>();
            CreateMap<LoginDto, UserInfo>();
            CreateMap<RegisterUserDto, UserInfo>();
            CreateMap<UpdateUserDto, UserInfo>();
        }
    }
}

using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            this.CreateMap<UserModel, UserEntity>().ReverseMap();
        }
    }
}

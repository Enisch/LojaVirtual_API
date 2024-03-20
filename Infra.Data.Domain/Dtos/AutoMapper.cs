using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Data.Domain.Models;


namespace Infra.Data.Domain.Dtos
{
    public class AutoMapper_Dto:Profile
    {
        public AutoMapper_Dto()
        {
            CreateMap<Usuario,UserDto>().ReverseMap();
        }
    }
}

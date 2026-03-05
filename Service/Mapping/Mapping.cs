using AutoMapper;
using DBContext.cs.Entity;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserServiceDTO, User>().ReverseMap();
           

            CreateMap<VehicleServiceDTO, Vehicle>().ReverseMap();
            CreateMap<ServiceHistoryDTO, ServiceHistory>().ReverseMap();


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using AutoMapper;
using Core.Entites;
using Core.Entites.Identity;
using Core.Interfaces;

namespace Api.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bikes,BikeDTo>();
            CreateMap<Details,DetailsDto>();
          
        }
    }
}
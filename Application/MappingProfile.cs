using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Infraestructure.Entities.Invoice, Domain.Models.Invoice>().ReverseMap();
        }
    }
}

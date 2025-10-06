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
            CreateMap<Infraestructure.Entities.Task, Domain.Models.Task>().ReverseMap();
            CreateMap<Infraestructure.Entities.User, Domain.Models.User>().ReverseMap();
            CreateMap<Infraestructure.Entities.TaskAssignment, Domain.Models.TaskAssignment>().ReverseMap();
            CreateMap<Infraestructure.Entities.TaskState, Domain.Models.TaskState>().ReverseMap();
            CreateMap<Infraestructure.Entities.TaskStatusHistory, Domain.Models.TaskStatusHistory>().ReverseMap();
            CreateMap<Infraestructure.Entities.UserRole, Domain.Models.UserRole>().ReverseMap();
        }
    }
}

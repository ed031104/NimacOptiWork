using AutoMapper;
using Infraestructure.Entities;
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
            CreateMap<Invoice, Domain.Models.Invoice>().ReverseMap();
            CreateMap<Entities.Task, Domain.Models.Task>().ReverseMap();
            CreateMap<User, Domain.Models.User>().ReverseMap();
            CreateMap<TaskState, Domain.Models.TaskState>().ReverseMap();
            CreateMap<TaskStatusHistory, Domain.Models.TaskStatusHistory>().ReverseMap();
            CreateMap<UserRole, Domain.Models.UserRole>().ReverseMap();
            CreateMap<Rol, Domain.Models.Rol>().ReverseMap();

            CreateMap<TaskAssignment, Domain.Models.TaskAssignment>()
                .ForMember(dest => dest.AssignedTo,
                           opt => opt.MapFrom(src => src.AssignedToNavigation != null ? new Domain.Models.User
                           {
                               Id = src.AssignedToNavigation.Id,
                               Username = src.AssignedToNavigation.Username,
                               Active = src.AssignedToNavigation.Active,
                               DateCreated = src.AssignedToNavigation.DateCreated,
                               DateModified = src.AssignedToNavigation.DateModified,
                               Email = src.AssignedToNavigation.Email,
                               Password = src.AssignedToNavigation.Password
                           } : null))
                .ForMember(dest => dest.Task,
                           opt => opt.MapFrom(src => src.Task != null ? new Domain.Models.Task
                           {
                               TaskId = src.Task.TaskId,
                               Createdby = src.Task.Createdby,
                               CreatedDate = src.Task.CreatedDate,
                               Description = src.Task.Description,
                               InvoiceNumber = src.Task.InvoiceNumber,
                               TaskCode = src.Task.TaskCode
                           } : null));
        }
    
    }
}

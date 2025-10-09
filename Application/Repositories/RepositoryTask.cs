using Infraestructure.Context;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infraestructure.Repositories
{
    public class RepositoryTask : IRepositoryTask
    {
        private readonly NimacOptiWorkContext _context;
        private readonly IMapper _mapper;
        private readonly UserSession _userSession;

        public RepositoryTask(NimacOptiWorkContext context, IMapper mapper, UserSession userSession)
        {
            _context = context;
            _mapper = mapper;
            _userSession = userSession;
        }

        // funciona
        public async System.Threading.Tasks.Task addTaskAsync(Domain.Models.Task task)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Entities.Task entityTask = _mapper.Map<Infraestructure.Entities.Task>(task);
                await _context.Tasks.AddAsync(entityTask);
                await _context.SaveChangesAsync();

                // Crear una asignación de tarea inicial sin usuario asignado
                var taskAssignment = new Entities.TaskAssignment
                {
                    Task = entityTask,
                    AssignedTo = null, // No asignado inicialmente
                    AssignedDate = DateTime.Now
                };

                await _context.TaskAssignments.AddAsync(taskAssignment);
                await _context.SaveChangesAsync();

                // Crear un historial de estado inicial para la tarea
                var taskStatusHistory = new Entities.TaskStatusHistory
                {
                    TaskAssignment = taskAssignment,
                    TaskAssignmentId = taskAssignment.TaskAssignmentId,
                    TaskStateId = (int)StatusTaskE.ALAESPERA, // Estado inicial
                    ChangedDate = DateTime.Now,
                    ChangedBy = _userSession.UserName
                };

                await _context.TaskStatusHistories.AddAsync(taskStatusHistory);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async System.Threading.Tasks.Task assignTaskToUserAsync(int idTask, int idUser)
        {
            try
            {
                //actualizar el campo user id en la tabla task assignment
                var taskAssignment = await _context.TaskAssignments
                    .FirstOrDefaultAsync(t => t.TaskAssignmentId == idTask);

                if (taskAssignment == null)
                    return;

                taskAssignment.AssignedTo = idUser;
                taskAssignment.AssignedDate = DateTime.Now;
                _context.TaskAssignments.Update(taskAssignment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Count() => await _context.Tasks.CountAsync();

        public async System.Threading.Tasks.Task deleteTaskAsync(int id)
        {
            try
            {
                var task = _context.Tasks.Find(id);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Domain.Models.Task>> getAllTasksAsync()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                var domainTasks = _mapper.Map<IEnumerable<Domain.Models.Task>>(tasks);
                return domainTasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<string>> getCodeFactura()
        {
            // Ensure null values are filtered out to match the non-nullable IEnumerable<string> return type
            return await _context.Invoices
                .Where(i => i.Rfdcno != null) // Filter out null values
                .Select(i => i.Rfdcno!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Domain.Models.Task> getTaskByCode(string code)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.InvoiceNumber == code);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var domainTask = _mapper.Map<Domain.Models.Task>(task);
                return domainTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Domain.Models.Task> getTaskByIdAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var domainTask = _mapper.Map<Domain.Models.Task>(task);
                return domainTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Domain.Models.Task> getTaskByTitle(string title)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Description == title);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var domainTask = _mapper.Map<Domain.Models.Task>(task);
                return domainTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Domain.Models.TaskAssignment>> getTaskByUserAssigned(int idUser)
        {
            try
            {
                var tasksByUser = await _context.TaskAssignments
                    .Where(ta => ta.AssignedTo == idUser)
                    .ToListAsync(); // Fix: Convert IAsyncEnumerable to a List

                var domainTasks = _mapper.Map<IEnumerable<Domain.Models.TaskAssignment>>(tasksByUser);
                return domainTasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Domain.Models.TaskAssignment>> getTasksByStatusAsync(int idStatus)
        {
            try
            {
                var domainTasks = await _context.TaskAssignments
                    .Where(ta => ta.TaskStatusHistories.Any(tsh => tsh.TaskStateId == idStatus))
                    .Include(ta => ta.Task)
                    .Include(ta => ta.AssignedToNavigation)
                    .Select(ta => new Domain.Models.TaskAssignment
                    {
                        TaskAssignmentId = ta.TaskAssignmentId,
                        AssignedDate = ta.AssignedDate,
                        DateStarted = ta.DateStarted,
                        DateCompleted = ta.DateCompleted,
                        TimeEstimatedStart = ta.TimeEstimatedStart,
                        TimeEstimatedEnd = ta.TimeEstimatedEnd,
                        Task = ta.Task != null ? new Domain.Models.Task
                        {
                            TaskId = ta.Task.TaskId,
                            Createdby = ta.Task.Createdby,
                            CreatedDate = ta.Task.CreatedDate,
                            Description = ta.Task.Description,
                            InvoiceNumber = ta.Task.InvoiceNumber,
                            TaskCode = ta.Task.TaskCode
                        } : null,
                        AssignedTo = ta.AssignedToNavigation != null ? new Domain.Models.User
                        {
                            Id = ta.AssignedToNavigation.Id,
                            Username = ta.AssignedToNavigation.Username,
                            Password = ta.AssignedToNavigation.Password,
                            Email = ta.AssignedToNavigation.Email,
                            Active = ta.AssignedToNavigation.Active,
                            DateCreated = ta.AssignedToNavigation.DateCreated,
                            DateModified = ta.AssignedToNavigation.DateModified
                        } : null
                    })
                    .ToListAsync();

                return domainTasks;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public System.Threading.Tasks.Task TaskStateAsync(int idTask, StatusInvoicesE state)
        {
            try
            {
                var task = _context.TaskAssignments.Find(idTask);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var taskStateHistory = new Entities.TaskStatusHistory
                {
                    TaskAssignment = task,
                    TaskAssignmentId = task.TaskAssignmentId,
                    TaskStateId = (int)state,
                    ChangedDate = DateTime.Now
                };
                _context.TaskStatusHistories.Add(taskStateHistory);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public System.Threading.Tasks.Task updateStatusTaskAsync(int idTask, int idStatus)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task updateTaskAsync(Domain.Models.Task task)
        {
            try
            {
                var existingTask = _context.Tasks.Find(task.TaskId);
                if (existingTask == null)
                {
                    throw new Exception("Task not found");
                }
                _mapper.Map(task, existingTask);
                _context.Tasks.Update(existingTask);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

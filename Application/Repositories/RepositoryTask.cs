using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
                    TaskStateId = (int)StatusTaskE.CREADO, // Estado inicial
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
            var transaction = await _context.Database.BeginTransactionAsync();
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

                //crear un nuevo registro en la tabla task status history con el estado asignado
                var taskStatusHistory = new Entities.TaskStatusHistory
                {
                    TaskAssignment = taskAssignment,
                    TaskAssignmentId = taskAssignment.TaskAssignmentId,
                    TaskStateId = (int)StatusTaskE.ENPROGRESO, // Estado asignado
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

        public Task<Response<bool>> assingIntervalTime(int idTaskAssigned, DateTime startTime, DateTime endTime, DateTime? statimeTimeStimated = null, DateTime? endTimeStimated = null)
        {
            try
            {
                var findTaskAssigned = _context.TaskAssignments.Find(idTaskAssigned);
                if (findTaskAssigned == null)
                {
                    return new Task<Response<bool>>(() => Response<bool>.Failure("Tarea no encontrada"));
                }
                findTaskAssigned.DateStarted = startTime;
                findTaskAssigned.DateCompleted = endTime;
                findTaskAssigned.TimeEstimatedStart = statimeTimeStimated;
                findTaskAssigned.TimeEstimatedEnd = endTimeStimated;

                _context.TaskAssignments.Update(findTaskAssigned);
                _context.SaveChanges();
                return new Task<Response<bool>>(() => Response<bool>.Success(true));
            }
            catch
            {
                return new Task<Response<bool>>(() => Response<bool>.Failure("Error al asignar el intervalo de tiempo"));
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

        public async Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getTaskByUserAssigned(int idUser)
        {
            Response<IEnumerable<Domain.Models.TaskStatusHistory>> response;
            try
            {
                //traer todas las tareas asignadas a un usuario con el último estado asignado
                var taskStatusHistories = await _context.TaskStatusHistories
                        .Where(tsh => tsh.TaskAssignment.AssignedToNavigation.Id == idUser &&
                                      tsh.ChangedDate == _context.TaskStatusHistories
                                          .Where(inner => inner.TaskAssignmentId == tsh.TaskAssignmentId)
                                          .Max(inner => inner.ChangedDate))
                        .Include(tsh => tsh.TaskAssignment)
                            .ThenInclude(ta => ta.Task)
                        .Include(tsh => tsh.TaskAssignment)
                            .ThenInclude(ta => ta.AssignedToNavigation)
                        .Include(tsh => tsh.TaskState)
                        .ToListAsync();


                if (taskStatusHistories == null || !taskStatusHistories.Any())
                {
                    response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure("No tasks found for the user");
                    return response;
                }

                var domainTaskStatusHistories = taskStatusHistories.Select(tsh => new Domain.Models.TaskStatusHistory(
                    tsh.TaskStatusHistoryId,
                    new Domain.Models.TaskAssignment(
                        tsh.TaskAssignment.TaskAssignmentId,
                        new Domain.Models.Task(
                            tsh.TaskAssignment.Task.TaskId,
                            tsh.TaskAssignment.Task.InvoiceNumber,
                            tsh.TaskAssignment.Task.Description,
                            tsh.TaskAssignment.Task.CreatedDate,
                            tsh.TaskAssignment.Task.Createdby,
                            tsh.TaskAssignment.Task.TaskCode
                        ),
                        tsh.TaskAssignment.AssignedToNavigation != null
                            ? new Domain.Models.User(
                                tsh.TaskAssignment.AssignedToNavigation.Id,
                                tsh.TaskAssignment.AssignedToNavigation.Email,
                                tsh.TaskAssignment.AssignedToNavigation.Username,
                                tsh.TaskAssignment.AssignedToNavigation.Password,
                                tsh.TaskAssignment.AssignedToNavigation.DateCreated,
                                tsh.TaskAssignment.AssignedToNavigation.DateModified,
                                tsh.TaskAssignment.AssignedToNavigation.Active
                            )
                            : null,
                        tsh.TaskAssignment.AssignedDate,
                        tsh.TaskAssignment.DateStarted,
                        tsh.TaskAssignment.DateCompleted,
                        tsh.TaskAssignment.TimeEstimatedStart,
                        tsh.TaskAssignment.TimeEstimatedEnd
                    ),
                    new Domain.Models.TaskState(
                        tsh.TaskState.TaskStateId,
                        tsh.TaskState.TaskStateName
                    ),
                    tsh.ChangedDate,
                    tsh.ChangedBy
                ));


                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Success(domainTaskStatusHistories);
                return response;
            }
            catch (Exception ex)
            {
                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure(ex.Message);
                return response;
            }
        }

        public async Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getTasksByStatusAsync(int idStatus)
        {
            Response<IEnumerable<Domain.Models.TaskStatusHistory>> response;
            try
            {

                var taskStatusHistories = await _context.TaskStatusHistories
                    .Where(tsh => tsh.TaskStateId == idStatus &&
                                  tsh.ChangedDate == _context.TaskStatusHistories
                                      .Where(inner => inner.TaskAssignmentId == tsh.TaskAssignmentId)
                                      .Max(inner => inner.ChangedDate))
                    .Include(tsh => tsh.TaskAssignment)
                        .ThenInclude(ta => ta.Task)
                    .Include(tsh => tsh.TaskAssignment)
                        .ThenInclude(ta => ta.AssignedToNavigation)
                    .Include(tsh => tsh.TaskState)
                    .ToListAsync();

                if (taskStatusHistories == null || !taskStatusHistories.Any())
                {
                    response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure("No tasks found with the specified status");
                    return response;
                }

                // Mapeo manual de la entidad ORM a la entidad de dominio
                var domainTaskStatusHistories = taskStatusHistories.Select(tsh => new Domain.Models.TaskStatusHistory(
                    tsh.TaskStatusHistoryId,
                    new Domain.Models.TaskAssignment(
                        tsh.TaskAssignment.TaskAssignmentId,
                        new Domain.Models.Task(
                            tsh.TaskAssignment.Task.TaskId,
                            tsh.TaskAssignment.Task.InvoiceNumber,
                            tsh.TaskAssignment.Task.Description,
                            tsh.TaskAssignment.Task.CreatedDate,
                            tsh.TaskAssignment.Task.Createdby,
                            tsh.TaskAssignment.Task.TaskCode
                        ),
                        tsh.TaskAssignment.AssignedToNavigation != null
                            ? new Domain.Models.User(
                                tsh.TaskAssignment.AssignedToNavigation.Id,
                                tsh.TaskAssignment.AssignedToNavigation.Email,
                                tsh.TaskAssignment.AssignedToNavigation.Username,
                                tsh.TaskAssignment.AssignedToNavigation.Password,
                                tsh.TaskAssignment.AssignedToNavigation.DateCreated,
                                tsh.TaskAssignment.AssignedToNavigation.DateModified,
                                tsh.TaskAssignment.AssignedToNavigation.Active
                            )
                            : null,
                        tsh.TaskAssignment.AssignedDate,
                        tsh.TaskAssignment.DateStarted,
                        tsh.TaskAssignment.DateCompleted,
                        tsh.TaskAssignment.TimeEstimatedStart,
                        tsh.TaskAssignment.TimeEstimatedEnd
                    ),
                    new Domain.Models.TaskState(
                        tsh.TaskState.TaskStateId,
                        tsh.TaskState.TaskStateName
                    ),
                    tsh.ChangedDate,
                    tsh.ChangedBy
                ));

                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Success(domainTaskStatusHistories);

                return response;
            }
            catch (Exception ex)
            {
                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure(ex.Message);
                return response;
            }
        }

        public async Task<Response<IEnumerable<TaskStatusHistory>>> getUnassignedTasks()
        {
            Response<IEnumerable<Domain.Models.TaskStatusHistory>> response;
            try
            {
                //traer todas las tareas con el ultimo estado asignado
                var taskStatusHistories = await _context.TaskStatusHistories
                    .Include(tsh => tsh.TaskAssignment)
                        .ThenInclude(ta => ta.Task)
                    .Include(tsh => tsh.TaskAssignment)
                        .ThenInclude(ta => ta.AssignedToNavigation)
                    .Include(tsh => tsh.TaskState)
                    .Where(tsh => tsh.TaskStateId == (int)StatusTaskE.CREADO && tsh.TaskAssignment.AssignedTo == null)
                    .GroupBy(tsh => tsh.TaskAssignmentId)
                    .Select(g => g.OrderByDescending(tsh => tsh.ChangedDate).FirstOrDefault())
                    .ToListAsync();

                if (taskStatusHistories == null || !taskStatusHistories.Any())
                {
                    response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure("No tasks found with the specified status");
                    return response;
                }

                // Mapeo manual de la entidad ORM a la entidad de dominio
                var domainTaskStatusHistories = taskStatusHistories.Select(tsh => new Domain.Models.TaskStatusHistory(
                    tsh.TaskStatusHistoryId,
                    new Domain.Models.TaskAssignment(
                        tsh.TaskAssignment.TaskAssignmentId,
                        new Domain.Models.Task(
                            tsh.TaskAssignment.Task.TaskId,
                            tsh.TaskAssignment.Task.InvoiceNumber,
                            tsh.TaskAssignment.Task.Description,
                            tsh.TaskAssignment.Task.CreatedDate,
                            tsh.TaskAssignment.Task.Createdby,
                            tsh.TaskAssignment.Task.TaskCode
                        ),
                        tsh.TaskAssignment.AssignedToNavigation != null
                            ? new Domain.Models.User(
                                tsh.TaskAssignment.AssignedToNavigation.Id,
                                tsh.TaskAssignment.AssignedToNavigation.Email,
                                tsh.TaskAssignment.AssignedToNavigation.Username,
                                tsh.TaskAssignment.AssignedToNavigation.Password,
                                tsh.TaskAssignment.AssignedToNavigation.DateCreated,
                                tsh.TaskAssignment.AssignedToNavigation.DateModified,
                                tsh.TaskAssignment.AssignedToNavigation.Active
                            )
                            : null,
                        tsh.TaskAssignment.AssignedDate,
                        tsh.TaskAssignment.DateStarted,
                        tsh.TaskAssignment.DateCompleted,
                        tsh.TaskAssignment.TimeEstimatedStart,
                        tsh.TaskAssignment.TimeEstimatedEnd
                    ),
                    new Domain.Models.TaskState(
                        tsh.TaskState.TaskStateId,
                        tsh.TaskState.TaskStateName
                    ),
                    tsh.ChangedDate,
                    tsh.ChangedBy
                ));

                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Success(domainTaskStatusHistories);

                return response;
            }
            catch (Exception ex)
            {
                response = Response<IEnumerable<Domain.Models.TaskStatusHistory>>.Failure(ex.Message);
                return response;
            }
        }

        public System.Threading.Tasks.Task TaskStateAsync(int idTaskAssigned, StatusTaskE state)
        {
            try
            {
                var task = _context.TaskAssignments.Find(idTaskAssigned);
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

        public async System.Threading.Tasks.Task updateStatusTaskAsync(TaskAssignment taskAssignment)
        {            
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<Response<IEnumerable<TaskState>>> getStateTask()
        {
            try
            {
                // Obtener los estados de la base de datos
                var taskStates = await _context.TaskStates.AsNoTracking().ToListAsync();

                // Mapear a las entidades de dominio
                var domainTaskStates = _mapper.Map<IEnumerable<Domain.Models.TaskState>>(taskStates);

                // Validar si hay resultados
                if (!domainTaskStates.Any())
                {
                    return Response<IEnumerable<TaskState>>.Failure("No task states found");
                }

                return Response<IEnumerable<TaskState>>.Success(domainTaskStates);
            }
            catch (AutoMapperMappingException ex)
            {
                return Response<IEnumerable<TaskState>>.Failure($"Mapping error: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return Response<IEnumerable<TaskState>>.Failure($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TaskState>>.Failure($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<Response<bool>> addStateTaskAssigment(int idTaskAssigned, StatusTaskE state)
        {
            try
            {
                //verificar si ya esxiste el estado para la tarea asignada
                var existingState = await _context.TaskStatusHistories
                    .FirstOrDefaultAsync(tsh => tsh.TaskAssignmentId == idTaskAssigned && tsh.TaskStateId == (int)state);
                if (existingState != null)
                    {
                    return Response<bool>.Failure("El estado ya existe para esta tarea asignada");
                }

                await _context.TaskStatusHistories.AddAsync(new Entities.TaskStatusHistory
                {
                    TaskAssignmentId = idTaskAssigned,
                    TaskStateId = (int)state,
                    ChangedDate = DateTime.Now,
                    ChangedBy = _userSession.UserName
                });
                
                await _context.SaveChangesAsync();
                return Response<bool>.Success(true);
            }
            catch (Exception e)
            { 
                return Response<bool>.Failure(e.Message);
            }
        }
    }
}

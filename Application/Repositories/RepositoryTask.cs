using Application.Context;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryTask : IRepositoryTask
    {
        private readonly NimacOptiWorkContext _context;
        private readonly IMapper _mapper;

        public RepositoryTask(NimacOptiWorkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task addTaskAsync(Domain.Models.Task task)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                // Map the Domain.Models.Task to Infraestructure.Entities.Task
                Entities.Task entityTask = _mapper.Map<Infraestructure.Entities.Task>(task);
                await _context.Tasks.AddAsync(entityTask);
                await _context.SaveChangesAsync();

                // add status created in history

                var taskStateHistory = new Entities.TaskStatusHistory
                {
                    Task = entityTask,
                    TaskId = entityTask.TaskId,
                    TaskStateId = (int)StatusTaskE.ALAESPERA,
                    ChangedDate = DateTime.Now,
                    ChangedBy = "system"
                };

                await _context.TaskStatusHistories.AddAsync(taskStateHistory);
                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();
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
                var task = await _context.Tasks.FindAsync(idTask);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                var user = await _context.Users.FindAsync(idUser);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var assignment = new Entities.TaskAssignment
                {
                    Task = task,
                    AssignedTo = user.Id,
                    AssignedDate = DateTime.Now
                };

                await _context.TaskAssignments.AddAsync(assignment);
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

        public Task<Domain.Models.Task> getTaskByTitle(string title)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(t => t.Description == title);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var domainTask = _mapper.Map<Domain.Models.Task>(task);
                return Task.FromResult(domainTask);
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

        public Task<IEnumerable<Domain.Models.Task>> getTasksByStatusAsync(int idStatus)
        {
            try
            {

                var tasks = _context.TaskStatusHistories
                    .Where(tsh => tsh.TaskStateId == idStatus)
                    .Select(tsh => tsh.Task)
                    .Distinct() // Ensure distinct tasks if multiple status entries exist
                    .ToList();

                if (tasks == null || !tasks.Any())
                {
                    throw new Exception("No tasks found with the specified status");
                }

                var domainTasks = _mapper.Map<IEnumerable<Domain.Models.Task>>(tasks);
                return Task.FromResult(domainTasks);
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
                var task = _context.Tasks.Find(idTask);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }
                var taskStateHistory = new Entities.TaskStatusHistory
                {
                    Task = task,
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

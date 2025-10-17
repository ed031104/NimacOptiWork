using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Generic
{
    public class ServicesTask : IServicesTask
    {
        private readonly IRepositoryTask repositoryTask;

        public ServicesTask(IRepositoryTask repositoryTask)
        {
            this.repositoryTask = repositoryTask;
        }

        public async System.Threading.Tasks.Task addTaskAsync(Domain.Models.Task task)
        {
            task.CreatedDate = DateTime.Now;
            task.Createdby = "system";
            await repositoryTask.addTaskAsync(task);
        }

        public async System.Threading.Tasks.Task assignTaskToUserAsync(int idTask, int idUser)
        {
            await repositoryTask.assignTaskToUserAsync(idTask, idUser);
        }


        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task deleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Models.Task>> getAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> getCodeFactura()
        {
            return await repositoryTask.getCodeFactura();
        }

        public Task<Domain.Models.Task> getTaskByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Task> getTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Task> getTaskByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<TaskStatusHistory>>> getTaskByUserAssigned(int idUser)
        {
            try
            {
                var task = await repositoryTask.getTaskByUserAssigned(idUser);

                if(!task.Status)
                {
                    return Response<IEnumerable<TaskStatusHistory>>.Failure(task.Message);
                }

                return Response<IEnumerable<TaskStatusHistory>>.Success(task.Data, task.Message);   
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<TaskStatusHistory>>.Failure(ex.Message);
            }
        }

        public async Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getTasksByStatusAsync(int idStatus)
        {
            if (idStatus <= 0 || idStatus == null)  return null!;

            Response<IEnumerable<Domain.Models.TaskStatusHistory>> task = await repositoryTask.getTasksByStatusAsync(idStatus);

            return task;
        }

        public async Task<Response<IEnumerable<TaskStatusHistory>>> getUnassignedTasks()
        {
            
            Response<IEnumerable<Domain.Models.TaskStatusHistory>> task = await repositoryTask.getUnassignedTasks();

            return task;
        }

        public System.Threading.Tasks.Task TaskStateAsync(int idTask, StatusInvoicesE state)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task updateStatusTaskAsync(int idTask, int idStatus)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task updateTaskAsync(Domain.Models.Task task)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<TaskState>>> getStateTask() => await repositoryTask.getStateTask();
        
        public async Task<Response<bool>> addStateTaskAssigment(int idTaskAssigned, StatusTaskE state) => await repositoryTask.addStateTaskAssigment(idTaskAssigned, state);

        public async Task<Response<bool>> assingIntervalTime(int idTaskAssigned, DateTime startTime, DateTime endTime, DateTime? statimeTimeStimated = null, DateTime? endTimeStimated = null)
        {
            var result = await repositoryTask.assingIntervalTime(idTaskAssigned, startTime, endTime, statimeTimeStimated, endTimeStimated);

            if (!result.Data)
            {
                return result;
            }
            return result;
        }
    }
}

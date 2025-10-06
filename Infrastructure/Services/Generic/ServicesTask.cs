using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public System.Threading.Tasks.Task assignTaskToUserAsync(int idTask, int idUser)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<TaskAssignment>> getTaskByUserAssigned(int idUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Models.Task>> getTasksByStatusAsync(int idStatus)
        {
            var task = await repositoryTask.getTasksByStatusAsync(idStatus);
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
    }
}

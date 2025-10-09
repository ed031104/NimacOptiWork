using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryTask
    {
        Task<IEnumerable<Domain.Models.Task>> getAllTasksAsync();
        Task<Domain.Models.Task> getTaskByIdAsync(int id);
        Task<Domain.Models.Task> getTaskByCode(string code);
        System.Threading.Tasks.Task addTaskAsync(Domain.Models.Task task);
        System.Threading.Tasks.Task updateTaskAsync(Domain.Models.Task task);
        System.Threading.Tasks.Task deleteTaskAsync(int id);

        Task<IEnumerable<Domain.Models.TaskAssignment>> getTasksByStatusAsync(int idStatus);
        System.Threading.Tasks.Task updateStatusTaskAsync(int idTask, int idStatus);

        Task<int> Count();

        System.Threading.Tasks.Task assignTaskToUserAsync(int idTask, int idUser);

        System.Threading.Tasks.Task TaskStateAsync(int idTask, StatusInvoicesE state);
        
        Task<Domain.Models.Task> getTaskByTitle(string title);
        Task<IEnumerable<Domain.Models.TaskAssignment>> getTaskByUserAssigned(int idUser);

        Task<IEnumerable<string>> getCodeFactura();
    }
}

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

        Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getTasksByStatusAsync(int idStatus);
        Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getUnassignedTasks();

        System.Threading.Tasks.Task updateStatusTaskAsync(TaskAssignment taskAssignment);

        Task<int> Count();

        System.Threading.Tasks.Task assignTaskToUserAsync(int idTask, int idUser);

        System.Threading.Tasks.Task TaskStateAsync(int idTaskAssigned, StatusTaskE state);
        
        Task<Domain.Models.Task> getTaskByTitle(string title);

        Task<Response<IEnumerable<Domain.Models.TaskStatusHistory>>> getTaskByUserAssigned(int idUser);

        Task<IEnumerable<string>> getCodeFactura();

        Task<Response<bool>> assingIntervalTime(int idTaskAssigned, DateTime startTime, DateTime endTime, DateTime? statimeTimeStimated = null, DateTime? endTimeStimated = null);

        Task<Response<bool>> addStateTaskAssigment(int idTaskAssigned, StatusTaskE state);

        Task<Response<IEnumerable<TaskState>>> getStateTask();
    }
}

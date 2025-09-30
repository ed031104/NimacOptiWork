using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUserTaskAssignmentRepository
    {
        Task<IEnumerable<System.Threading.Tasks.Task>> GetTasksForUserAsync(User user);
        Task<IEnumerable<User>> GetUsersForTaskAsync(System.Threading.Tasks.Task task);
        System.Threading.Tasks.Task AssignAsync(User user, System.Threading.Tasks.Task task);
        System.Threading.Tasks.Task UnassignAsync(User user, System.Threading.Tasks.Task task);
        Task<IEnumerable<TaskAssignment>> GetAllAssignmentsAsync();
    }
}

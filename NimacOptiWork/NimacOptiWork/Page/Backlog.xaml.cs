using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Models;
using Infraestructure.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NimacOptiWork.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace NimacOptiWork.Page
{
    public sealed partial class Backlog : Microsoft.UI.Xaml.Controls.Page
    {
        public ObservableCollection<Domain.Models.TaskStatusHistory> ToDoTasks { get; set; } = new();
        public ObservableCollection<Domain.Models.TaskStatusHistory> InProgressTasks { get; set; } = new();
        public ObservableCollection<Domain.Models.TaskStatusHistory> DoneTasks { get; set; } = new();

        private readonly UserSession _userSession;
        private readonly IServicesTask _servicesTask;

        public Domain.Models.TaskStatusHistory SelectedTask { get; set; }

        private Dictionary<userRole, Func<System.Threading.Tasks.Task>> userPermissions;

        public TimeSpan SelectedTime { get; set; }


        public Backlog()
        {
            InitializeComponent();

            _userSession = App.AppHost.Services.GetRequiredService<UserSession>();
            _servicesTask = App.AppHost.Services.GetRequiredService<IServicesTask>();

            userPermissions = new Dictionary<userRole, Func<System.Threading.Tasks.Task>>
            {
                { userRole.administrador, async () =>  await LoadAllTask()},
                { userRole.almacenista, async () => await LoadTaskByUserId() },
            };
        }

        protected async override void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                await LoadStateTask();
                await loadTaskAssigment();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void Task_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is StackPanel panel && panel.DataContext is Domain.Models.TaskStatusHistory task)
            {
                SelectedTask = task;
                MainSplitView.IsPaneOpen = true;
            }
        }

        private void ClosePane_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
        }

        private async System.Threading.Tasks.Task loadTaskAssigment()
        {
            var role = (userRole)_userSession.IdRole;
            UIHelpers.ForEachRoleAction(role, userPermissions);
        }

        private async void UpdateStatusTask(object sender, RoutedEventArgs e)
        {
            var selectedState = stateTask.SelectedValue;
            var selectedTask = SelectedTask;

            if (selectedState == null)
            {
                return;
            }

            await _servicesTask.addStateTaskAssigment(selectedTask.TaskAssignment.TaskAssignmentId, (StatusTaskE)selectedState);

            await refreshListTask();
        }
        
        #region others methods
        private async System.Threading.Tasks.Task LoadAllTask()
        {
            var tasksInProgress = await _servicesTask.getTasksByStatusAsync((int)StatusTaskE.ENPROGRESO);
            var tasksToDo = await _servicesTask.getTasksByStatusAsync((int)StatusTaskE.ALAESPERA);
            var tasksDone = await _servicesTask.getTasksByStatusAsync((int)StatusTaskE.COMPLETADA);

            if (!tasksInProgress.Status)
            {
                // Manejar el error según sea necesario
            }
            if (!tasksToDo.Status)
            {
                // Manejar el error según sea necesario
            }
            if (!tasksDone.Status)
            {
                // Manejar el error según sea necesario
            }

            if (tasksInProgress.Status)
            {
                foreach (var task in tasksInProgress.Data)
                {
                    InProgressTasks.Add(task);
                }
            }

            if (tasksToDo.Status)
            {
                foreach (var task in tasksToDo.Data)
                {
                    ToDoTasks.Add(task);
                }
            }

            if (tasksDone.Status)
            {
                foreach (var task in tasksDone.Data)
                {
                    DoneTasks.Add(task);
                }
            }
        }

        private async System.Threading.Tasks.Task LoadTaskByUserId()
        {
            int userID = _userSession.UserId ?? 0;
            var tasksInProgress = await _servicesTask.getTaskByUserAssigned(userID);

            if (!tasksInProgress.Status)
            {
                // Manejar el error según sea necesario
                return;
            }

            var tasksToDo = tasksInProgress.Data.Where(t => t.TaskState.TaskStateId == (int)StatusTaskE.ALAESPERA);
            var tasksDone = tasksInProgress.Data.Where(t => t.TaskState.TaskStateId == (int)StatusTaskE.COMPLETADA);
            var tasksInProg = tasksInProgress.Data.Where(t => t.TaskState.TaskStateId == (int)StatusTaskE.ENPROGRESO);


            if (tasksToDo != null)
            {
                foreach (var task in tasksToDo)
                {
                    ToDoTasks.Add(task);
                }
            }

            if (tasksDone != null)
            {
                foreach (var task in tasksDone)
                {
                    DoneTasks.Add(task);
                }
            }
            if (tasksInProg != null)
            {
                foreach (var task in tasksInProg)
                {
                    InProgressTasks.Add(task);
                }
            }
        }

        private async System.Threading.Tasks.Task refreshListTask()
        {
            ToDoTasks.Clear();
            InProgressTasks.Clear();
            DoneTasks.Clear();
            await LoadAllTask();
        }

        private async System.Threading.Tasks.Task LoadStateTask()
        {
            var states = await _servicesTask.getStateTask();
            if (!states.Status)
            {
                return;
            }
            stateTask.ItemsSource = states.Data;
        }
        #endregion

    }
}

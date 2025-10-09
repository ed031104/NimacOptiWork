using Application.Services.Generic;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    public sealed partial class GestionTask : Microsoft.UI.Xaml.Controls.Page
    {
        public ObservableCollection<Domain.Models.TaskAssignment> ListTask { get; set; } = new();
        private string codeFacturaSelected = string.Empty;
        private TaskAssignment? _taskSelected;
        public TaskAssignment? TaskSelected
        {
            get => _taskSelected;
            set
            {
                _taskSelected = value;
            }
        }

        private readonly IServicesTask _invoiceService;
        private readonly IServices<Domain.Models.User, Infraestructure.Entities.User> _userService;

        UserSession _userSession;

        public GestionTask()
        {
            InitializeComponent();
            _invoiceService = App.AppHost.Services.GetRequiredService<IServicesTask>();
            _userService = App.AppHost.Services.GetRequiredService<IServices<User, Infraestructure.Entities.User>>();
            _userSession = App.AppHost.Services.GetRequiredService<UserSession>();
        }

        protected override async void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await loadingTasksAsync();
            await loadUsersComboBox();
        }

        private async void crearTaskBtn(object sender, RoutedEventArgs e)
        {
            var task = new Domain.Models.Task.Builder()
                .WithInvoiceNumber(codeFacturaSelected)
                .WithDescription(descriptionInput.Text)
                .WithCreatedDate(DateTime.Now)
                .WithCreatedby(_userSession.UserName) // Cambiar por el usuario actual
                .Build();
            await _invoiceService.addTaskAsync(task);
            await loadingTasksAsync();

            codeFacturaSelected = string.Empty;
            descriptionInput.Text = string.Empty;
            tittleInput.Text = string.Empty;
            facturaAutoSuggest.Text = string.Empty;
        }


        private async void AsignarUsuario(object sender, RoutedEventArgs e)
        {
            var userSelected = users.SelectedItem as Domain.Models.User;
            var taskAssignmentId = TaskSelected;

            if (userSelected == null && taskAssignmentId == null)
            {
                await showInfobar("Error en la asignación", "Debe seleccionar un usuario y una tarea para asignar.", InfoBarSeverity.Error);
                return;
            }

            await _invoiceService.assignTaskToUserAsync(taskAssignmentId.TaskAssignmentId, userSelected.Id);
            MainSplitView.IsPaneOpen = false;
            _taskSelected = null;

            await showInfobar("Asignación exitosa", "La tarea ha sido asignada correctamente.", InfoBarSeverity.Success);

            await loadingTasksAsync();
        }

        // método para el autosuggest de facturas
        private async void facturaCreate_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = await _invoiceService.getCodeFactura();
                sender.ItemsSource = suggestions;
            }
        }

        private void facturaCreate_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                codeFacturaSelected = args.ChosenSuggestion.ToString()!;
                facturaAutoSuggest.Text = codeFacturaSelected;
                return;
            }
            else
            {
                codeFacturaSelected = args.QueryText;
                facturaAutoSuggest.Text = codeFacturaSelected;
                return;
            }
        }

        #region others methods
        private void ClosePane_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            _taskSelected = null;
        }

        private void Task_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Domain.Models.TaskAssignment taskAssignment)
            {
                tittleAssingmentUser.Text = "Detalles de " + taskAssignment.Task.TaskCode;
                TaskSelected = taskAssignment;
            }

            MainSplitView.IsPaneOpen = true;
        }

        private async System.Threading.Tasks.Task loadUsersComboBox()
        {
            var usersFromDb = await _userService.GetAllAsync();
            users.ItemsSource = usersFromDb;
        }

        private async System.Threading.Tasks.Task loadingTasksAsync()
        {
            var tasksFromDb = await _invoiceService.getTasksByStatusAsync((int)StatusTaskE.ALAESPERA);
            ListTask.Clear();
            foreach (var task in tasksFromDb)
            {
                ListTask.Add(task);
            }
        }
        
        private async System.Threading.Tasks.Task showInfobar(string tittle, string message, InfoBarSeverity severity, int timeline = 1000)
        {
            infoBar.Title = tittle;
            infoBar.Message = message;
            infoBar.Severity = severity;
            infoBar.IsOpen = true;
            await System.Threading.Tasks.Task.Delay(timeline);
            infoBar.IsOpen = false;
        }
        #endregion
    }
}

using Application.Services.Generic;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using NimacOptiWork.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NimacOptiWork.Page
{
    public sealed partial class GestionTask : Microsoft.UI.Xaml.Controls.Page
    {
        private Dictionary<userRole, List<string>> _permissionsUser;

        public ObservableCollection<Domain.Models.TaskStatusHistory> ListTask { get; set; } = new();
        private ObservableCollection<string> _invoices = new();
        private string codeFacturaSelected = string.Empty;
        private TaskStatusHistory? _taskSelected;
        public TaskStatusHistory? TaskSelected
        {
            get => _taskSelected;
            set
            {
                _taskSelected = value;
            }
        }

        private readonly IServicesTask _invoiceService;
        private readonly IServices<Domain.Models.User, Infraestructure.Entities.User> _userService;

        private UserSession _userSession;

        public GestionTask()
        {
            InitializeComponent();
            _invoiceService = App.AppHost.Services.GetRequiredService<IServicesTask>();
            _userService = App.AppHost.Services.GetRequiredService<IServices<User, Infraestructure.Entities.User>>();
            _userSession = App.AppHost.Services.GetRequiredService<UserSession>();
            
            _permissionsUser = new Dictionary<userRole, List<string>> {{ userRole.administrador, new List<string> { "OpenPaneButton" } },};
        }

        protected override async void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await validateRole();
            await loadingTasksAsync();
            await loadUsersComboBox();
            await loadInvoices();
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

            await _invoiceService.assignTaskToUserAsync(taskAssignmentId.TaskAssignment.TaskAssignmentId, userSelected.Id);
            MainSplitView.IsPaneOpen = false;
            _taskSelected = null;

            await loadingTasksAsync();

            await showInfobar("Asignación exitosa", "La tarea ha sido asignada correctamente.", InfoBarSeverity.Success);
        }

        // método para el autosuggest de facturas
        private async void facturaCreate_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if(string.IsNullOrEmpty(sender.Text))
                {
                    await loadInvoices();
                    return;
                }
                var filteredInvoices = _invoices.Select(_invoices => _invoices.ToLower())
                                                .Where(factura => factura.Contains(sender.Text.ToLower()))
                                                .ToList();
                sender.ItemsSource = filteredInvoices;
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
        
        private async System.Threading.Tasks.Task loadInvoices()
        {
            var invoicesFromDb = await _invoiceService.getCodeFactura();
            facturaAutoSuggest.ItemsSource = invoicesFromDb;

            foreach (var invoice in invoicesFromDb)
            {
                _invoices.Add(invoice);
            }
        }
        private async System.Threading.Tasks.Task validateRole()
        {
            if (_userSession.IdRole == null || Content is not FrameworkElement frameworkElement)
                return;

            var role = (userRole)_userSession.IdRole;
            UIHelpers.ApplyRoleUI(frameworkElement, role, _permissionsUser);
        }

        private void ClosePane_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            _taskSelected = null;
        }

        private void Task_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Domain.Models.TaskStatusHistory taskAssignment)
            {
                tittleAssingmentUser.Text = "Detalles de " + taskAssignment.TaskAssignment.Task.TaskCode;
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
            var tasksFromDb = await _invoiceService.getUnassignedTasks();

            if (!tasksFromDb.Status)
            { 
                infoBar.Title = "Error al cargar las tareas";
                infoBar.Message = tasksFromDb.Message;
                infoBar.Severity = InfoBarSeverity.Error;
                
                ListTask.Clear();
                return;
            }

            ListTask.Clear();
            foreach (var task in tasksFromDb.Data)
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

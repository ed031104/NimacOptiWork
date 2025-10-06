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
        private readonly IServicesTask _invoiceService;
        public ObservableCollection<Domain.Models.Task> ListTask { get; set; } = new();
        private string codeFacturaSelected = string.Empty;

        public GestionTask()
        {
            InitializeComponent();
            _invoiceService = App.AppHost.Services.GetRequiredService<IServicesTask>();
        }

        protected override async void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await loadingTasksAsync();
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

        private async void crearTaskBtn(object sender, RoutedEventArgs e)
        {
            var task = new Domain.Models.Task.Builder()
                .WithInvoiceNumber(codeFacturaSelected)
                .WithDescription(descriptionInput.Text)
                .WithCreatedDate(DateTime.Now)
                .WithCreatedby("Admin") // Cambiar por el usuario actual
                .Build();
            await _invoiceService.addTaskAsync(task);
            await loadingTasksAsync();

            codeFacturaSelected = string.Empty;
            descriptionInput.Text = string.Empty;
            tittleInput.Text = string.Empty;
            facturaAutoSuggest.Text = string.Empty;

        }
        private void ClosePane_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
        }

        private void Task_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = true;
        }

        private async void facturaCreate_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = await _invoiceService.getCodeFactura();
                sender.ItemsSource = suggestions;
            }
        }

        private void facturaCreate_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if(args.ChosenSuggestion != null)
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
    }
}

using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NimacOptiWork.Page;

public sealed partial class GestionInvoice : Microsoft.UI.Xaml.Controls.Page
{
    int itemsPerPage = 40;
    private List<Invoice> _invoices;

    private readonly IServices<Invoice, Infraestructure.Entities.Invoice> _invoiceService;
    ObservableCollection<Invoice> invoicesFromDb = new ObservableCollection<Invoice>();

    public GestionInvoice()
    {
        InitializeComponent();
        _invoiceService = App.AppHost.Services.GetRequiredService<IServices<Invoice, Infraestructure.Entities.Invoice>>();
        _invoices = new List<Invoice>();
        this.Loaded += Page_Loaded; // Subscribe to the Loaded event
    }

    private void Page_Loaded(object sender, RoutedEventArgs e) 
    {
        _= loadingInvoicesAsync();
    }

    private async System.Threading.Tasks.Task loadingInvoicesAsync() {
        _invoices = (await _invoiceService.GetAllAsync()).ToList();

        int totalCount = _invoices.Count();
        pipsPager.NumberOfPages = (int)Math.Ceiling((double)totalCount / itemsPerPage);
        loadDataGrid(0);
    }

    private void editInvoiceEventBtn(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is int id)
        {
            // Show a dialog with the Id of the selected row
            ContentDialog dialog = new ContentDialog
            {
                Title = "Usuario a editar",
                Content = $"El Id es: {id}",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot
            };
            _ = dialog.ShowAsync();
        }
    }

    private void deleteInvoiceEventBtn(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        var tip = new TeachingTip
        {
            Title = "This is the title",
            Subtitle = "And this is the subtitle",
            IsLightDismissEnabled = true
        };

        var rootGrid = (this.Content as Grid); // asumiendo que tu Page tiene un Grid raíz
        rootGrid.Children.Add(tip);

        // Mostrar el TeachingTip relativo al botón
        tip.Target = button;
        tip.IsOpen = true;
    }


    private void loadDataGrid(int pageIndex)
    {
        loadingOverlay.Visibility = Visibility.Visible;

        var listTemp = _invoices
            .OrderByDescending(i => i.DateEntry)
            .ThenByDescending(i => i.InvoiceId)
            .Skip(pageIndex * itemsPerPage)
            .Take(itemsPerPage);

        invoicesFromDb.Clear();
        foreach (var item in listTemp)
        {
            invoicesFromDb.Add(item);
        }

        loadingOverlay.Visibility = Visibility.Collapsed;
    }

    private void pipsPager_SelectedIndexChanged(PipsPager sender, PipsPagerSelectedIndexChangedEventArgs args)
    {

        loadDataGrid(sender.SelectedPageIndex);  
    }
}

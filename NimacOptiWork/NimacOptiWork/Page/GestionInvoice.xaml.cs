using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NimacOptiWork.Page;

public sealed partial class GestionInvoice : Microsoft.UI.Xaml.Controls.Page
{
    // Inyeccion de dependencias
    private readonly IServices<Invoice, Infraestructure.Entities.Invoice> _invoiceService;

    // Coleccion observable para enlazar con el DataGrid
    ObservableCollection<Invoice> invoicesFromDb = new ObservableCollection<Invoice>();
    private List<Invoice> _invoices = new List<Invoice>();

    int itemsPerPage = 40;

    public GestionInvoice()
    {
        InitializeComponent();
        _invoiceService = App.AppHost.Services.GetRequiredService<IServices<Invoice, Infraestructure.Entities.Invoice>>();
    }

    protected override async void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await loadingInvoicesAsync();
    }

    private async System.Threading.Tasks.Task loadingInvoicesAsync() {
        
        // Mostrar overlay de carga
        loadingOverlay.Visibility = Visibility.Visible;
        containerTable.Visibility = Visibility.Collapsed;

        int totalCount = await _invoiceService.Count();
        pipsPager.NumberOfPages = (int)Math.Ceiling((double)totalCount / itemsPerPage);
      
        await loadDataGrid(1);

        // Ocultar overlay de carga
        containerTable.Visibility = Visibility.Visible;
        loadingOverlay.Visibility = Visibility.Collapsed;
    }

    private void editInvoiceEventBtn(object sender, RoutedEventArgs e)
    {
        StandardPopup.IsOpen = true;
    }

    private void deleteInvoiceEventBtn(object sender, RoutedEventArgs e)
    {

        #region teaching tip delete confirmation
        var button = sender as Button;
        int id = (int)button.Tag;

        var tip = new TeachingTip
        {
            Title = "¿Seguro que quieres eliminar el registro?",
            Subtitle = "Una vez eliminado no se puede recuperar",
            IsLightDismissEnabled = true,
            ActionButtonContent = "Eliminar",
            CloseButtonContent = "Cancelar",
        };

        var rootGrid = (this.Content as Grid); // asumiendo que tu Page tiene un Grid raíz
        rootGrid.Children.Add(tip);

        // Mostrar el TeachingTip relativo al botón
        tip.Target = button;
        tip.IsOpen = true;
        #endregion

        tip.ActionButtonClick += async (s, args) =>
        {
            tip.IsOpen = false;
            rootGrid.Children.Remove(tip); // Eliminar el TeachingTip del árbol visual
            await deleteInvoice(id);
        };
    }


    private void ClosePopupClicked(object sender, RoutedEventArgs e) {
        StandardPopup.IsOpen = false;
    }


    private async System.Threading.Tasks.Task loadDataGrid(int pageIndex)
    {
        _invoices = (await _invoiceService.GetAllAsync(pageIndex, itemsPerPage)).ToList();
        invoicesFromDb.Clear();
        dataGridInvoices.ItemsSource = _invoices;
    }

    private async void ModificarBtn(object sender, RoutedEventArgs e)
    {
        await showInfobar("Exito", "Se modificó correctamente el Invoice", InfoBarSeverity.Success, 2000);
    }

    private async void pipsPager_SelectedIndexChanged(PipsPager sender, PipsPagerSelectedIndexChangedEventArgs args)
    {
        loadingOverlay.Visibility = Visibility.Visible;
        containerTable.Visibility = Visibility.Collapsed;

        await loadDataGrid(sender.SelectedPageIndex);

        containerTable.Visibility = Visibility.Visible;
        loadingOverlay.Visibility = Visibility.Collapsed;
    }


    #region functions additional
        
    private async System.Threading.Tasks.Task deleteInvoice(int id)
    {
        await showInfobar("Exito", "Se eleminó correctamente el Invoice", InfoBarSeverity.Success, 2000);
    }
    #endregion

    #region additional methods
    private async System.Threading.Tasks.Task showInfobar(string tittle, string message, InfoBarSeverity severity, int timeline = 1000) {
        infoBar.Title = tittle;
        infoBar.Message = message;
        infoBar.Severity = severity;
        infoBar.IsOpen = true;
        await System.Threading.Tasks.Task.Delay(timeline);
        infoBar.IsOpen = false;
    }
    #endregion

}

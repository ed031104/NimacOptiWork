using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork.Page;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GestionInvoice : Microsoft.UI.Xaml.Controls.Page
{

    ObservableCollection<Invoice> invoicesFromDb = new ObservableCollection<Invoice>();

    public GestionInvoice()
    {
        InitializeComponent();

        this.Loaded += Page_Loaded; // Subscribe to the Loaded event
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e) => await loadDataGrid();

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


    private async System.Threading.Tasks.Task loadDataGrid()
    {
        loadingOverlay.Visibility = Visibility.Visible;

        NimacOptiWorkContext dbContex = new NimacOptiWorkContext();

        using (var contexInvoice = new NimacOptiWorkContext())
        {
            var listTemp = await dbContex.Invoices
                .OrderByDescending(i => i.DateEntry)
                .ThenByDescending(i => i.InvoiceId)
                .Take(100)
                .ToListAsync();

            DispatcherQueue.TryEnqueue(() =>
            {
                foreach (var item in listTemp)
                    invoicesFromDb.Add(item);

                System.Threading.Tasks.Task.Yield();
            });
        }

        loadingOverlay.Visibility = Visibility.Collapsed;
    }


}

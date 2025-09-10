using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork.Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GestionUser : Microsoft.UI.Xaml.Controls.Page
    {
        public GestionUser()
        {
            InitializeComponent();
            this.Loaded += Page_Loaded; // Subscribe to the Loaded event
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void editUserEventBtn(object sender, RoutedEventArgs e)
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

        private void deleteUserEventBtn(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                // Show a dialog with the Id of the selected row
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Usuario a eliminar",
                    Content = $"El Id es: {id}",
                    CloseButtonText = "Ok",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = dialog.ShowAsync();
            }
        }

        #region other methods

        private void loadDataGrid()
        {
            List<Object> users = new List<Object>()
            {
                new { Id = 1, Name = "Juan",Email= "edwini.noviembre@gmail.com" , Role = "Admin" },
                new { Id = 2, Name = "María", Email= "edwini.noviembre@gmail.com", Role = "User" },
                new { Id = 3, Name = "Pedro",Email= "edwini.noviembre@gmail.com", Role = "User" }
            };
            dataGridUsers.ItemsSource = users;
        }
        #endregion
    }
}

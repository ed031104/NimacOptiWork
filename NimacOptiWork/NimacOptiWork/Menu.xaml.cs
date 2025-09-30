using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NimacOptiWork.Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(SimpleTitleBar); // Set the custom title bar element
        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;

            if (item != null)
            {
                string tag = item.Tag?.ToString();

                switch (tag)
                {
                    case "Gestión de usuarios":
                        // Acción para "Inicio"
                        ContentArea.Navigate(typeof(GestionUser));
                        break;

                    case "dashboard":
                        // Acción para "Dashboard"
                        ContentArea.Navigate(typeof(Dashboard));
                        break;
                    case "Backlog":
                        // Acción para "Calendario"
                        ContentArea.Navigate(typeof(Backlog));
                        break;
                    case "Gestión de actividades":
                        // Acción para "Reportes"
                        ContentArea.Navigate(typeof(GestionInvoice));
                        break;
                    case "Gestión de tareas":
                        // Acción para "Reportes"
                        ContentArea.Navigate(typeof(GestionTask));
                        break;
                    case "settings":
                        // Acción para "Configuración"
                        break;
                }
            }
        }
    }
}

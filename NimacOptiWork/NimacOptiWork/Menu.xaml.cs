using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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


namespace NimacOptiWork
{
    public sealed partial class Menu : Window
    {

        UserSession userSession;

        public Menu()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(SimpleTitleBar); // Set the custom title bar element
            
            userSession = App.AppHost.Services.GetRequiredService<UserSession>();
        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;

            if (item != null)
            {
                string tag = item.Tag?.ToString();

                switch (tag)
                {
                    case "Gesti�n de usuarios":
                        // Acci�n para "Inicio"
                        ContentArea.Navigate(typeof(GestionUser));
                        break;

                    case "dashboard":
                        // Acci�n para "Dashboard"
                        ContentArea.Navigate(typeof(Dashboard));
                        break;
                    case "Backlog":
                        // Acci�n para "Calendario"
                        ContentArea.Navigate(typeof(Backlog));
                        break;
                    case "Gesti�n de actividades":
                        // Acci�n para "Reportes"
                        ContentArea.Navigate(typeof(GestionInvoice));
                        break;
                    case "Gesti�n de tareas":
                        // Acci�n para "Reportes"
                        ContentArea.Navigate(typeof(GestionTask));
                        break;
                    case "settings":
                        // Acci�n para "Configuraci�n"
                        break;
                }
            }
        }
    }
}

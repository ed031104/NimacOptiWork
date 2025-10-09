using Domain.Interfaces.Services;
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace NimacOptiWork
{
    public sealed partial class MainWindow : Window
    {

        IServicesLogin _servicesLogin;

        public MainWindow()
        {
            InitializeComponent();
            _servicesLogin = App.AppHost.Services.GetRequiredService<IServicesLogin>();
        }


        private async void LoginEventBtn(object sender, RoutedEventArgs e)
        {
            loadingOverlay.Visibility = Visibility.Visible;

            var credentials = (username: string.Empty, password: string.Empty);
            credentials.username = email.Text.Trim();
            credentials.password = password.Password.Trim();

            bool isloginValid = await _servicesLogin.ValidateCredentialAsync(credentials.username, credentials.password);

            if (!isloginValid)
            {
                infoBar.IsOpen = true;
                infoBar.Title = "Error";
                infoBar.Message = "Invalid Credentials";
                infoBar.Severity = InfoBarSeverity.Error;

                await System.Threading.Tasks.Task.Delay(2000);

                loadingOverlay.Visibility = Visibility.Collapsed;
                return;
            }

            loadingOverlay.Visibility = Visibility.Collapsed;

            infoBar.IsOpen = true;
            infoBar.Title = "Éxito";
            infoBar.Message = "Login Successful";
            infoBar.Severity = InfoBarSeverity.Success;

            Menu mainMenu = new Menu();

            mainMenu.Activate();
            this.Close();
        }
    }
}

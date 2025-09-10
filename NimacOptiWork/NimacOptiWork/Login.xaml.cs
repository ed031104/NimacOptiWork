using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void LoginEventBtn(object sender, RoutedEventArgs e)
        {
            loadingOverlay.Visibility = Visibility.Visible;

            if (!email.Text.Equals("admin") && !password.Password.Equals("admin123"))
            {
                infoBar.IsOpen = true;
                infoBar.Title = "Error";
                infoBar.Message = "Invalid Credentials";
                infoBar.Severity = InfoBarSeverity.Error;

                await System.Threading.Tasks.Task.Delay(2000);

                loadingOverlay.Visibility = Visibility.Collapsed;
                return;
            }

            await System.Threading.Tasks.Task.Delay(2000);

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

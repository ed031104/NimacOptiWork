using Microsoft.UI;
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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork.Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Backlog : Microsoft.UI.Xaml.Controls.Page
    {
        public ObservableCollection<BacklogItem> ToDoTasks { get; set; } = new();
        public ObservableCollection<BacklogItem> InProgressTasks { get; set; } = new();
        public ObservableCollection<BacklogItem> DoneTasks { get; set; } = new();


        public BacklogItem SelectedTask { get; set; }


        public Backlog()
        {
            InitializeComponent();
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 1", Description = "Descripción de la tarea 1" });
            ToDoTasks.Add(new BacklogItem { Title = "Tarea 2", Description = "Descripción de la tarea 2" });

            InProgressTasks.Add(new BacklogItem { Title = "Tarea 3", Description = "En progreso..." });

            DoneTasks.Add(new BacklogItem { Title = "Tarea 4", Description = "Finalizada" });
        }

        private void Task_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is StackPanel panel && panel.DataContext is BacklogItem task)
            {
                SelectedTask = task;
                MainSplitView.IsPaneOpen = true;
            }
        }

        private void ClosePane_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
        }
    }

    public class BacklogItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }


}

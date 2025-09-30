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
    public sealed partial class GestionTask : Microsoft.UI.Xaml.Controls.Page
    {
        public ObservableCollection<TaskItem> ListTask { get; set; } = new();

        public GestionTask()
        {
            InitializeComponent();

            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });
            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });
            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });
            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });
            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });
            ListTask.Add(new TaskItem { Title = "Tarea 1", Description = "Descripci�n de la tarea 1" });
            ListTask.Add(new TaskItem { Title = "Tarea 2", Description = "Descripci�n de la tarea 2" });

        }


    }
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

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
    public sealed partial class GestionActivty : Microsoft.UI.Xaml.Controls.Page
    {
        public ObservableCollection<string> Horas { get; set; } = new();
        public ObservableCollection<Actividad> Actividades { get; set; } = new();

        public GestionActivty()
        {
            InitializeComponent();
            // Generar horas (ej: 6:00 - 22:00)
            for (int h = 6; h <= 22; h++)
            {
                Horas.Add($"{h:00}:00");
            }

            // Ejemplo de actividades
            Actividades.Add(new Actividad { Titulo = "Reunión de equipo", Inicio = new TimeSpan(8, 0, 0), Fin = new TimeSpan(9, 30, 0) });
            Actividades.Add(new Actividad { Titulo = "Diseño UI", Inicio = new TimeSpan(10, 0, 0), Fin = new TimeSpan(12, 0, 0) });
            Actividades.Add(new Actividad { Titulo = "Almuerzo", Inicio = new TimeSpan(13, 0, 0), Fin = new TimeSpan(14, 0, 0) });
            Actividades.Add(new Actividad { Titulo = "Codificación", Inicio = new TimeSpan(15, 0, 0), Fin = new TimeSpan(17, 30, 0) });

            this.DataContext = this;
            this.DataContext = this;

            // Dibujar eventos
            Loaded += (s, e) => DibujarEventos();
        }

        private void DibujarEventos()
        {
            EventosCanvas.Children.Clear();

            foreach (var act in Actividades)
            {
                var border = new Border
                {
                    Width = 200,
                    Height = act.Height,   // Alto proporcional a la duración
                    Background = new SolidColorBrush(Colors.LightSkyBlue),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(5),
                    Child = new TextBlock
                    {
                        Text = act.Titulo,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(5)
                    }
                };

                Canvas.SetTop(border, act.Top);
                Canvas.SetLeft(border, 0);

                EventosCanvas.Children.Add(border);
            }
        }
    }

    public class Actividad
    {
        public string Titulo { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fin { get; set; }

        public double Top => Inicio.TotalMinutes; // Posición en Canvas
        public double Height => (Fin - Inicio).TotalMinutes;
    }


}

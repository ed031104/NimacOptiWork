using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SkiaSharp;
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
    public sealed partial class Dashboard : Microsoft.UI.Xaml.Controls.Page
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public Dashboard()
        {
            InitializeComponent();

            loadDataChar();
        }

        private void loadDataChar()
        {

            DataContext = this; // 🔑 Necesario para Binding

            var horas = new[] { "8:00", "9:00", "10:00", "11:00", "12:00" };

            Series = new ISeries[]
            {
                new LineSeries<int>
                {
                    Values = new[] { 5, 8, 6, 7, 10 },
                    Name = "Usuario A",
                    Stroke = new SolidColorPaint(SKColors.Blue, 2),
                    Fill = null
                },
              //   acá puedes agregar más series si es necesario
                new LineSeries<int>
                {
                    Values = new[] { 3, 6, 4, 9, 12 },
                    Name = "Usuario B",
                    Stroke = new SolidColorPaint(SKColors.Red, 2),
                    Fill = null
                }
            };

            XAxes = new Axis[] // Changed from List<Axis> to Axis[]
            {
                new Axis
                {
                    Labels = horas,
                    Name = "Hora del día"
                }
            };

            YAxes = new Axis[] // Changed from List<Axis> to Axis[]
            {
                new Axis
                {
                    Name = "Registros",
                    MinLimit = 0
                }
            };
        }
    }
}

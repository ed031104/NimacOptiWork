using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI;
using Domain.Enums;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NimacOptiWork.Components;

public sealed partial class statusInvoice : UserControl
{
    public statusInvoice()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TypeStatusInvoiceProperty =
      DependencyProperty.Register(
          nameof(TypeStatusInvoice),
          typeof(StatusInvoicesE),
          typeof(statusInvoice),
          new PropertyMetadata(StatusInvoicesE.Invoice, OnTypeStatusInvoiceChanged));

    public StatusInvoicesE TypeStatusInvoice
    {
        get => (StatusInvoicesE)GetValue(TypeStatusInvoiceProperty);
        set => SetValue(TypeStatusInvoiceProperty, value);
    }

    private static void OnTypeStatusInvoiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as statusInvoice;
        control?.UpdateStatus((StatusInvoicesE)e.NewValue);
    }

    public void UpdateStatus(StatusInvoicesE value)
    {   
        
            switch (value)
            {
                case StatusInvoicesE.Invoice:
                    tittle.Text = "Invoice";
                    status.Fill = new SolidColorBrush(Colors.Green);
                    break;
                case StatusInvoicesE.Awaiting:
                    tittle.Text = "Awaiting";
                    status.Fill = new SolidColorBrush(Colors.Red);
                    break;
                default:
                    tittle.Text = "Unknown";
                    status.Fill = new SolidColorBrush(Colors.Gray);
                    break;
            }
    }
}



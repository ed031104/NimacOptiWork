using Application.Services.Generic;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Infraestructure;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace NimacOptiWork
{
    public partial class App : Microsoft.UI.Xaml.Application // Explicitly specify the namespace to avoid ambiguity
    {
        public static IHost AppHost { get; private set; }
        private Window? _window;

        public App()
        {
            InitializeComponent();

            string STRING_CONNECTION = "Server=.;Database=NimacOptiWork;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True;";

            AppHost = Host.CreateDefaultBuilder()
               .ConfigureServices((context, services) =>
               {
                   services.AddDbContext<NimacOptiWorkContext>(options =>
                        options.UseSqlServer(STRING_CONNECTION)
                   );

                   // Replace the problematic line with the following:
                   services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
                   services.AddTransient(typeof(IRepository<,>), typeof(GenericRepository<,>));
                   services.AddScoped(typeof(IServices<,>), typeof(ServicesGeneric<,>));
                   services.AddScoped<IRepositoryTask, RepositoryTask>();
                   services.AddScoped<IServicesTask, ServicesTask>();
                   services.AddScoped<IRepositoryLogin, RepositoryLogin>();
                   services.AddScoped<IServicesLogin, ServicesLogin>();
                   services.AddSingleton<UserSession>();
               })
               .Build();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}

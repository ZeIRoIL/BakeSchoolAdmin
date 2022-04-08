﻿namespace BakeSchoolAdmin_Gui
{

    using BakeSchoolAdmin_Gui.ViewModels;
    using Prism.Events;
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises Startup Event <see cref="System.Windows.Application.Startup"/> event
        /// </summary>
        /// Takes the  <param name="e"><see cref="System.Windows.StartupEventArgs"/>contains the event data</param> 
        protected override void OnStartup(StartupEventArgs e)
        {
            // get references to the current process
            Process currentProcess = Process.GetCurrentProcess();

            // Check if any other Process with same name is running
            if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
            {
                // if there 
                MessageBox.Show("Process already started! \n another instance already running!");
                Application.Current.Shutdown();
                return;
            }

            // Init event aggregator
            IEventAggregator eventAggregator = new EventAggregator();

            // init View and ViewModel
            MainWindow mainWindow = new MainWindow();
            MainViewModel mainViewModel = new MainViewModel(eventAggregator);

            // connect the mainWindow to the mainViewModel
            mainWindow.DataContext = mainViewModel;

            // show mainMinow
            mainWindow.Show();
        }
    }
}
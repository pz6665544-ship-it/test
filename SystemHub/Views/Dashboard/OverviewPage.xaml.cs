using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Dashboard
{
    public partial class OverviewPage : Page
    {
        private readonly ISystemInfoService _systemInfoService;

        public OverviewPage()
        {
            InitializeComponent();
            _systemInfoService = ServiceLocator.GetService<ISystemInfoService>();
            LoadSystemInfo();
        }

        private void LoadSystemInfo()
        {
            try
            {
                var overview = _systemInfoService.GetSystemOverview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadSystemInfo();
            MessageBox.Show("Dashboard refreshed!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SystemInfo_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("SystemInfo_Overview");
        }

        private void Performance_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("SystemInfo_Sensors");
        }

        private void Network_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("SystemInfo_Network");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("Settings_General");
        }
    }
}

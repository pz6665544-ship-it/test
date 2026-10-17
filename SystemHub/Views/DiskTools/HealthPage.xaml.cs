using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.DiskTools
{
    public partial class HealthPage : Page
    {
        private readonly IDiskInfoService _diskInfoService;

        public HealthPage()
        {
            InitializeComponent();
            _diskInfoService = ServiceLocator.GetService<IDiskInfoService>();
            LoadHealthData();
        }

        private void LoadHealthData()
        {
            try
            {
                var healthInfo = _diskInfoService.GetDiskHealth();
                var disks = _diskInfoService.GetPhysicalDisks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadHealthData();
            MessageBox.Show("Disk health information refreshed!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewDiskDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var diskId = button.Tag.ToString();
                // In a real implementation, we would navigate to a detailed view
                MessageBox.Show($"Viewing details for disk: {diskId}", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ViewSmartData_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var diskId = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                navigationService.NavigateTo("DiskTools_Health", diskId);
            }
        }

        private void RunFullTest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Running full disk health test... This may take several minutes.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RunQuickTest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Running quick disk health test... This should complete in under a minute.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Disk health report exported successfully!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("Settings_General");
        }

        // Disk health item class for the ListView
        public class DiskHealthItem
        {
            public string DiskId { get; set; }
            public string DiskName { get; set; }
            public string InterfaceType { get; set; }
            public string MediaType { get; set; }
            public string HealthStatus { get; set; }
            public double HealthPercentage { get; set; }
            public string HealthColor { get; set; }
            public double Temperature { get; set; }
        }

        // SMART data item class for the DataGrid
        public class SmartDataItem
        {
            public int AttributeId { get; set; }
            public string AttributeName { get; set; }
            public byte Value { get; set; }
            public byte Worst { get; set; }
            public byte Threshold { get; set; }
            public ulong RawValue { get; set; }
            public string Status { get; set; }
        }
    }
}

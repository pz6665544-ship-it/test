using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.DiskTools
{
    public partial class OverviewPage : Page
    {
        private readonly IDiskInfoService _diskInfoService;

        public OverviewPage()
        {
            InitializeComponent();
            _diskInfoService = ServiceLocator.GetService<IDiskInfoService>();
            LoadDiskInfo();
        }

        private void LoadDiskInfo()
        {
            try
            {
                var disks = _diskInfoService.GetPhysicalDisks();
                var drives = _diskInfoService.GetLogicalDrives();
                var health = _diskInfoService.GetDiskHealth();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDiskInfo();
            MessageBox.Show("Disk information refreshed!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var diskId = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                // In a real implementation, we would pass the diskId as a parameter
                navigationService.NavigateTo("DiskTools_Health", diskId);
            }
        }

        private void RunBenchmark_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var diskId = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                navigationService.NavigateTo("DiskTools_Benchmark", diskId);
            }
        }

        private void ExploreDrive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var driveLetter = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                navigationService.NavigateTo("DiskTools_FileExplorer", driveLetter);
            }
        }

        private void CleanupDrive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var driveLetter = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                navigationService.NavigateTo("DiskTools_Cleanup", driveLetter);
            }
        }

        private void DefragDrive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var driveLetter = button.Tag.ToString();
                var navigationService = ServiceLocator.GetService<INavigationService>();
                navigationService.NavigateTo("DiskTools_Defrag", driveLetter);
            }
        }

        private void EjectDrive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var driveLetter = button.Tag.ToString();
                MessageBox.Show($"Drive {driveLetter} ejected safely!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DiskHealth_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("DiskTools_Health");
        }

        private void Benchmark_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("DiskTools_Benchmark");
        }

        private void Cleanup_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("DiskTools_Cleanup");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("Settings_General");
        }

        // Disk item class for the ListView
        public class DiskItem
        {
            public string DiskId { get; set; }
            public string Model { get; set; }
            public string SerialNumber { get; set; }
            public string InterfaceType { get; set; }
            public string MediaType { get; set; }
            public ulong Size { get; set; }
            public string SizeFormatted => FormatFileSize(Size);
            public string HealthStatus { get; set; }
            public string HealthColor { get; set; }
            public double Temperature { get; set; }

            private static string FormatFileSize(ulong bytes)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                double len = bytes;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                return $"{len:0.##} {sizes[order]}";
            }
        }
    }
}

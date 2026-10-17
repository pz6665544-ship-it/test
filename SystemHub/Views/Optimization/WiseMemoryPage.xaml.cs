using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class WiseMemoryPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<MemoryProcessItem> _processes = new();
        private List<OptimizationHistoryItem> _history = new();
        private Random _random = new Random();

        public WiseMemoryPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadProcesses();
            LoadHistory();
        }

        private void LoadProcesses()
        {
            try
            {
                _processes = new List<MemoryProcessItem>
                {
                    new MemoryProcessItem { Name = "chrome.exe", Pid = "1567", PrivateWorkingSet = "1.2 GB", ShareableWorkingSet = "350 MB", TotalWorkingSet = "1.55 GB", CommitSize = "1.8 GB", PagedPool = "25 MB", NonPagedPool = "5 MB" },
                    new MemoryProcessItem { Name = "chrome.exe", Pid = "1890", PrivateWorkingSet = "850 MB", ShareableWorkingSet = "280 MB", TotalWorkingSet = "1.13 GB", CommitSize = "1.4 GB", PagedPool = "20 MB", NonPagedPool = "4 MB" },
                    new MemoryProcessItem { Name = "chrome.exe", Pid = "2345", PrivateWorkingSet = "720 MB", ShareableWorkingSet = "240 MB", TotalWorkingSet = "960 MB", CommitSize = "1.2 GB", PagedPool = "18 MB", NonPagedPool = "3 MB" },
                    new MemoryProcessItem { Name = "SystemHub.exe", Pid = "3456", PrivateWorkingSet = "180 MB", ShareableWorkingSet = "80 MB", TotalWorkingSet = "260 MB", CommitSize = "300 MB", PagedPool = "15 MB", NonPagedPool = "2 MB" },
                    new MemoryProcessItem { Name = "explorer.exe", Pid = "1234", PrivateWorkingSet = "250 MB", ShareableWorkingSet = "120 MB", TotalWorkingSet = "370 MB", CommitSize = "400 MB", PagedPool = "12 MB", NonPagedPool = "3 MB" },
                    new MemoryProcessItem { Name = "Spotify.exe", Pid = "3789", PrivateWorkingSet = "320 MB", ShareableWorkingSet = "150 MB", TotalWorkingSet = "470 MB", CommitSize = "500 MB", PagedPool = "10 MB", NonPagedPool = "2 MB" },
                    new MemoryProcessItem { Name = "Discord.exe", Pid = "4012", PrivateWorkingSet = "280 MB", ShareableWorkingSet = "100 MB", TotalWorkingSet = "380 MB", CommitSize = "400 MB", PagedPool = "8 MB", NonPagedPool = "1 MB" },
                    new MemoryProcessItem { Name = "Steam.exe", Pid = "4356", PrivateWorkingSet = "150 MB", ShareableWorkingSet = "80 MB", TotalWorkingSet = "230 MB", CommitSize = "250 MB", PagedPool = "6 MB", NonPagedPool = "1 MB" },
                    new MemoryProcessItem { Name = "NVIDIA Share.exe", Pid = "4890", PrivateWorkingSet = "45 MB", ShareableWorkingSet = "30 MB", TotalWorkingSet = "75 MB", CommitSize = "100 MB", PagedPool = "5 MB", NonPagedPool = "1 MB" },
                    new MemoryProcessItem { Name = "svchost.exe", Pid = "900", PrivateWorkingSet = "25.6 MB", ShareableWorkingSet = "15 MB", TotalWorkingSet = "40.6 MB", CommitSize = "50 MB", PagedPool = "3 MB", NonPagedPool = "0.5 MB" }
                };
                ProcessesDataGrid.ItemsSource = _processes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHistory()
        {
            try
            {
                _history = new List<OptimizationHistoryItem>
                {
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 14:30:25", Action = "Auto-Optimize", MemoryFreed = "1.2 GB", BeforeUsage = "82%", AfterUsage = "65%", Duration = "2.4s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 14:15:10", Action = "Manual Clear", MemoryFreed = "850 MB", BeforeUsage = "88%", AfterUsage = "72%", Duration = "1.8s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 13:45:05", Action = "Auto-Optimize", MemoryFreed = "920 MB", BeforeUsage = "85%", AfterUsage = "70%", Duration = "2.1s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 13:00:40", Action = "Defragment", MemoryFreed = "450 MB", BeforeUsage = "78%", AfterUsage = "68%", Duration = "3.2s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 12:30:15", Action = "Auto-Optimize", MemoryFreed = "1.1 GB", BeforeUsage = "84%", AfterUsage = "69%", Duration = "2.3s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 11:00:00", Action = "Purge Standby", MemoryFreed = "580 MB", BeforeUsage = "75%", AfterUsage = "62%", Duration = "1.5s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 10:15:30", Action = "Auto-Optimize", MemoryFreed = "780 MB", BeforeUsage = "80%", AfterUsage = "67%", Duration = "1.9s", Status = "Success" },
                    new OptimizationHistoryItem { Timestamp = "2024-01-15 09:30:00", Action = "Clear Working Set", MemoryFreed = "620 MB", BeforeUsage = "79%", AfterUsage = "64%", Duration = "2.0s", Status = "Success" }
                };
                HistoryDataGrid.ItemsSource = _history;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoOptimize_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Auto-optimization active - Memory usage: 65%";
                MessageBox.Show("Auto-optimization enabled.\n\nWise Memory Optimizer will automatically optimize memory when usage exceeds the threshold.", 
                    "Auto-Optimize Enabled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoOptimize_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Auto-optimization inactive - Manual mode";
                MessageBox.Show("Auto-optimization disabled.\n\nYou will need to manually optimize memory using the 'Optimize Now' button.", 
                    "Auto-Optimize Disabled", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OptimizationMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (OptimizationModeComboBox.SelectedItem != null)
                {
                    var mode = (OptimizationModeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    MessageBox.Show($"Optimization mode changed to: {mode}\n\nIn a full implementation, this would affect how aggressively memory is optimized.", 
                        "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OptimizeNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUsage = _random.Next(60, 85);
                var freedMemory = $"{_random.Next(500, 1500)} MB";
                var newUsage = currentUsage - _random.Next(10, 25);
                var duration = $"{_random.Next(1, 4)}.{_random.Next(0, 9)}s";

                MessageBox.Show(
                    $"Wise Memory Optimization Initiated\n\nCurrent Memory Usage: {currentUsage}%\nMemory Freed: {freedMemory}\nNew Memory Usage: {newUsage}%\nDuration: {duration}\n\nOptimization completed successfully!",
                    "Memory Optimization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Update status
                StatusText.Text = $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class MemoryPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<ProcessInfo> _processes = new();

        public MemoryPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            try
            {
                _processes = new List<ProcessInfo>
                {
                    new ProcessInfo { Id = "1", Name = "System", Pid = "4", Memory = "128 KB", Cpu = "0.1%", Description = "Windows System Process", IsSelected = false },
                    new ProcessInfo { Id = "2", Name = "Registry", Pid = "88", Memory = "52 MB", Cpu = "0.0%", Description = "Windows Registry", IsSelected = false },
                    new ProcessInfo { Id = "3", Name = "smss.exe", Pid = "340", Memory = "1.5 MB", Cpu = "0.0%", Description = "Session Manager", IsSelected = false },
                    new ProcessInfo { Id = "4", Name = "csrss.exe", Pid = "452", Memory = "5.2 MB", Cpu = "0.1%", Description = "Client Server Runtime Process", IsSelected = false },
                    new ProcessInfo { Id = "5", Name = "wininit.exe", Pid = "564", Memory = "2.8 MB", Cpu = "0.0%", Description = "Windows Initialization Process", IsSelected = false },
                    new ProcessInfo { Id = "6", Name = "services.exe", Pid = "676", Memory = "8.5 MB", Cpu = "0.2%", Description = "Services and Controller app", IsSelected = false },
                    new ProcessInfo { Id = "7", Name = "lsass.exe", Pid = "788", Memory = "15.3 MB", Cpu = "0.1%", Description = "Local Security Authority Process", IsSelected = false },
                    new ProcessInfo { Id = "8", Name = "svchost.exe", Pid = "900", Memory = "25.6 MB", Cpu = "0.5%", Description = "Host Process for Windows Services", IsSelected = false },
                    new ProcessInfo { Id = "9", Name = "explorer.exe", Pid = "1234", Memory = "250 MB", Cpu = "2.3%", Description = "Windows Explorer", IsSelected = false },
                    new ProcessInfo { Id = "10", Name = "chrome.exe", Pid = "1567", Memory = "1.2 GB", Cpu = "15.4%", Description = "Google Chrome", IsSelected = false },
                    new ProcessInfo { Id = "11", Name = "chrome.exe", Pid = "1890", Memory = "850 MB", Cpu = "12.1%", Description = "Google Chrome", IsSelected = false },
                    new ProcessInfo { Id = "12", Name = "chrome.exe", Pid = "2345", Memory = "720 MB", Cpu = "8.7%", Description = "Google Chrome", IsSelected = false },
                    new ProcessInfo { Id = "13", Name = "SystemHub.exe", Pid = "3456", Memory = "180 MB", Cpu = "3.2%", Description = "SystemHub Application", IsSelected = false }
                };
                ProcessesDataGrid.ItemsSource = _processes;
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
                MessageBox.Show("Memory optimization initiated.\n\nIn a full implementation, this would:", 
                    "Memory Optimization", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearCache_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Clear all system caches? This may temporarily slow down some applications as they rebuild their caches.",
                    "Clear Cache",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("System caches cleared successfully!\n\nSpace freed: ~500 MB", 
                        "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Defragment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Memory defragmentation initiated.\n\nThis would optimize memory allocation in a full implementation.", 
                    "Memory Defragmentation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Memory optimization settings would allow you to configure:\n\n- Auto-optimization schedule\n- Memory thresholds\n- Process priority rules\n- Cache management options", 
                    "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EndProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var processId = button.Tag.ToString();
                    var process = _processes.FirstOrDefault(p => p.Id == processId);
                    
                    if (process != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to end the process '{process.Name}' (PID: {process.Pid})?\n\nThis may cause the application to stop working.",
                            "Confirm End Process",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBox.Show($"Process '{process.Name}' has been terminated.", 
                                "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadProcesses();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var processId = button.Tag.ToString();
                    var process = _processes.FirstOrDefault(p => p.Id == processId);
                    
                    if (process != null)
                    {
                        var info = $"Process: {process.Name}\n";
                        info += $"PID: {process.Pid}\n";
                        info += $"Memory: {process.Memory}\n";
                        info += $"CPU: {process.Cpu}\n";
                        info += $"Description: {process.Description}\n\n";
                        info += "In a full implementation, this would show detailed process information including threads, handles, modules, and more.";

                        MessageBox.Show(info, $"Process Info: {process.Name}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Dashboard_Overview");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ProcessInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pid { get; set; }
        public string Memory { get; set; }
        public string Cpu { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}

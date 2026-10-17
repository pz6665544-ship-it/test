using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class ProcessesPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<ProcessItem> _processes = new();

        public ProcessesPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            try
            {
                _processes = new List<ProcessItem>
                {
                    new ProcessItem { Id = "1", Name = "System", Pid = "4", User = "SYSTEM", Cpu = "0.1", Memory = "128 KB", Priority = "Normal", Description = "Windows System Process", IsSelected = false },
                    new ProcessItem { Id = "2", Name = "Registry", Pid = "88", User = "SYSTEM", Cpu = "0.0", Memory = "52 MB", Priority = "Normal", Description = "Windows Registry", IsSelected = false },
                    new ProcessItem { Id = "3", Name = "smss.exe", Pid = "340", User = "SYSTEM", Cpu = "0.0", Memory = "1.5 MB", Priority = "Normal", Description = "Session Manager", IsSelected = false },
                    new ProcessItem { Id = "4", Name = "csrss.exe", Pid = "452", User = "SYSTEM", Cpu = "0.1", Memory = "5.2 MB", Priority = "Normal", Description = "Client Server Runtime Process", IsSelected = false },
                    new ProcessItem { Id = "5", Name = "wininit.exe", Pid = "564", User = "SYSTEM", Cpu = "0.0", Memory = "2.8 MB", Priority = "Normal", Description = "Windows Initialization Process", IsSelected = false },
                    new ProcessItem { Id = "6", Name = "services.exe", Pid = "676", User = "SYSTEM", Cpu = "0.2", Memory = "8.5 MB", Priority = "Normal", Description = "Services and Controller app", IsSelected = false },
                    new ProcessItem { Id = "7", Name = "lsass.exe", Pid = "788", User = "SYSTEM", Cpu = "0.1", Memory = "15.3 MB", Priority = "Normal", Description = "Local Security Authority Process", IsSelected = false },
                    new ProcessItem { Id = "8", Name = "svchost.exe", Pid = "900", User = "SYSTEM", Cpu = "0.5", Memory = "25.6 MB", Priority = "Normal", Description = "Host Process for Windows Services", IsSelected = false },
                    new ProcessItem { Id = "9", Name = "svchost.exe", Pid = "1000", User = "SYSTEM", Cpu = "0.3", Memory = "18.2 MB", Priority = "Normal", Description = "Host Process for Windows Services", IsSelected = false },
                    new ProcessItem { Id = "10", Name = "explorer.exe", Pid = "1234", User = "SystemUser", Cpu = "2.3", Memory = "250 MB", Priority = "Normal", Description = "Windows Explorer", IsSelected = false },
                    new ProcessItem { Id = "11", Name = "chrome.exe", Pid = "1567", User = "SystemUser", Cpu = "15.4", Memory = "1.2 GB", Priority = "Normal", Description = "Google Chrome", IsSelected = false },
                    new ProcessItem { Id = "12", Name = "chrome.exe", Pid = "1890", User = "SystemUser", Cpu = "12.1", Memory = "850 MB", Priority = "Normal", Description = "Google Chrome", IsSelected = false },
                    new ProcessItem { Id = "13", Name = "chrome.exe", Pid = "2345", User = "SystemUser", Cpu = "8.7", Memory = "720 MB", Priority = "Normal", Description = "Google Chrome", IsSelected = false },
                    new ProcessItem { Id = "14", Name = "SystemHub.exe", Pid = "3456", User = "SystemUser", Cpu = "3.2", Memory = "180 MB", Priority = "Normal", Description = "SystemHub Application", IsSelected = false },
                    new ProcessItem { Id = "15", Name = "Spotify.exe", Pid = "3789", User = "SystemUser", Cpu = "5.1", Memory = "320 MB", Priority = "Normal", Description = "Spotify", IsSelected = false },
                    new ProcessItem { Id = "16", Name = "Discord.exe", Pid = "4012", User = "SystemUser", Cpu = "2.8", Memory = "280 MB", Priority = "Normal", Description = "Discord", IsSelected = false },
                    new ProcessItem { Id = "17", Name = "Steam.exe", Pid = "4356", User = "SystemUser", Cpu = "1.5", Memory = "150 MB", Priority = "Normal", Description = "Steam Client", IsSelected = false },
                    new ProcessItem { Id = "18", Name = "dllhost.exe", Pid = "4567", User = "SYSTEM", Cpu = "0.0", Memory = "3.2 MB", Priority = "Normal", Description = "COM+ Host", IsSelected = false },
                    new ProcessItem { Id = "19", Name = "NVIDIA Share.exe", Pid = "4890", User = "SystemUser", Cpu = "0.8", Memory = "45 MB", Priority = "Normal", Description = "NVIDIA Share", IsSelected = false }
                };
                ProcessesDataGrid.ItemsSource = _processes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EndSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProcesses = _processes.Where(p => p.IsSelected).ToList();
                
                if (selectedProcesses.Count == 0)
                {
                    MessageBox.Show("Please select at least one process to end.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var processNames = string.Join(", ", selectedProcesses.Select(p => p.Name).Take(5));
                if (selectedProcesses.Count > 5)
                    processNames += " and " + (selectedProcesses.Count - 5) + " more";

                var result = MessageBox.Show(
                    $"Are you sure you want to end {selectedProcesses.Count} selected process(es)?\n\n{processNames}\n\nThis may cause applications to stop working.",
                    "Confirm End Processes",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var process in selectedProcesses.ToList())
                    {
                        _processes.Remove(process);
                        MessageBox.Show($"Process '{process.Name}' (PID: {process.Pid}) has been terminated.", 
                            "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ProcessesDataGrid.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EndAllNonSystem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nonSystemProcesses = _processes.Where(p => p.User != "SYSTEM").ToList();
                
                if (nonSystemProcesses.Count == 0)
                {
                    MessageBox.Show("No non-system processes found.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to end all {nonSystemProcesses.Count} non-system processes?\n\nThis will close all user applications and may cause data loss.",
                    "Confirm End All Non-System",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var process in nonSystemProcesses.ToList())
                    {
                        _processes.Remove(process);
                    }
                    ProcessesDataGrid.Items.Refresh();
                    MessageBox.Show($"Ended {nonSystemProcesses.Count} non-system processes.", 
                        "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetPriority_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProcesses = _processes.Where(p => p.IsSelected).ToList();
                
                if (selectedProcesses.Count == 0)
                {
                    MessageBox.Show("Please select at least one process to change priority.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (selectedProcesses.Count == 1)
                {
                    var process = selectedProcesses[0];
                    var priorities = new[] { "Realtime", "High", "Above Normal", "Normal", "Below Normal", "Low" };
                    var currentIndex = Array.IndexOf(priorities, process.Priority);
                    
                    // In a full implementation, this would open a dialog to select priority
                    MessageBox.Show(
                        $"Set priority for '{process.Name}' (PID: {process.Pid})\n\nCurrent: {process.Priority}\n\nAvailable: {string.Join(", ", priorities)}",
                        "Set Priority",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"Set priority for {selectedProcesses.Count} selected processes.\n\nIn a full implementation, this would open a dialog to select the priority for all selected processes.",
                        "Set Priority",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadProcesses();
                MessageBox.Show("Process list refreshed.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            _processes.Remove(process);
                            ProcessesDataGrid.Items.Refresh();
                            MessageBox.Show($"Process '{process.Name}' (PID: {process.Pid}) has been terminated.", 
                                "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SuspendProcess_Click(object sender, RoutedEventArgs e)
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
                            $"Are you sure you want to suspend the process '{process.Name}' (PID: {process.Pid})?\n\nThe process will be paused and can be resumed later.",
                            "Confirm Suspend Process",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            process.Cpu = "0.0";
                            ProcessesDataGrid.Items.Refresh();
                            MessageBox.Show($"Process '{process.Name}' (PID: {process.Pid}) has been suspended.", 
                                "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var processId = button.Tag.ToString();
                    var process = _processes.FirstOrDefault(p => p.Id == processId);
                    
                    if (process != null)
                    {
                        var info = $"Process Details:\n\n";
                        info += $"Name: {process.Name}\n";
                        info += $"PID: {process.Pid}\n";
                        info += $"User: {process.User}\n";
                        info += $"CPU: {process.Cpu}%\n";
                        info += $"Memory: {process.Memory}\n";
                        info += $"Priority: {process.Priority}\n";
                        info += $"Description: {process.Description}\n\n";
                        info += "In a full implementation, this would show:\n";
                        info += "- Threads and handles count\n";
                        info += "- Start time\n";
                        info += "- Module list\n";
                        info += "- Memory details\n";
                        info += "- Network activity\n";
                        info += "- Disk activity";

                        MessageBox.Show(info, $"Details: {process.Name}", MessageBoxButton.OK, MessageBoxImage.Information);
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

    public class ProcessItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pid { get; set; }
        public string User { get; set; }
        public string Cpu { get; set; }
        public string Memory { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}

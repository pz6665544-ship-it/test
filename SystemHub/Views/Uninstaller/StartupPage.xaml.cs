using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class StartupPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<StartupProgramItem> _startupPrograms = new();

        public StartupPage()
        {
            InitializeComponent();
            _uninstallerService = ServiceLocator.GetService<IUninstallerService>();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadStartupPrograms();
        }

        private void LoadStartupPrograms()
        {
            try
            {
                var programs = _uninstallerService.GetStartupPrograms();
                _startupPrograms = programs.Select(p => new StartupProgramItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Publisher = p.Publisher,
                    Command = p.Command,
                    Location = p.Location,
                    Type = p.Type.ToString(),
                    Enabled = p.Enabled,
                    IsWindowsService = p.IsWindowsService,
                    LastRunTime = p.LastRunTime,
                    RunCount = p.RunCount,
                    Status = p.Enabled ? "Enabled" : "Disabled",
                    StatusColor = p.Enabled ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gray),
                    ToggleIcon = p.Enabled ? "Power" : "PowerOff",
                    ToggleColor = p.Enabled ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green)
                }).ToList();

                StartupDataGrid.ItemsSource = _startupPrograms;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToggleStartup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var programId = button.Tag.ToString();
                    var program = _startupPrograms.FirstOrDefault(p => p.Id == programId);
                    
                    if (program != null)
                    {
                        var newEnabled = !program.Enabled;
                        var result = _uninstallerService.SetStartupProgramEnabled(programId, newEnabled);
                        
                        if (result)
                        {
                            program.Enabled = newEnabled;
                            program.Status = newEnabled ? "Enabled" : "Disabled";
                            program.StatusColor = newEnabled ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Gray);
                            program.ToggleIcon = newEnabled ? "Power" : "PowerOff";
                            program.ToggleColor = newEnabled ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
                            
                            StartupDataGrid.Items.Refresh();
                            
                            MessageBox.Show(
                                $"Startup program '{program.Name}' has been {(newEnabled ? "enabled" : "disabled")}.",
                                "SystemHub",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"Failed to {(newEnabled ? "enable" : "disable")} startup program '{program.Name}'.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RunNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var programId = button.Tag.ToString();
                    var program = _startupPrograms.FirstOrDefault(p => p.Id == programId);
                    
                    if (program != null)
                    {
                        MessageBox.Show(
                            $"Running startup program: {program.Name}\n\nCommand: {program.Command}",
                            "Run Program",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnableAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var disabledPrograms = _startupPrograms.Where(p => !p.Enabled).ToList();
                
                if (disabledPrograms.Count == 0)
                {
                    MessageBox.Show("All startup programs are already enabled.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to enable all {disabledPrograms.Count} disabled startup programs?",
                    "Confirm Enable All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var program in disabledPrograms)
                    {
                        _uninstallerService.SetStartupProgramEnabled(program.Id, true);
                        program.Enabled = true;
                        program.Status = "Enabled";
                        program.StatusColor = new SolidColorBrush(Colors.Green);
                        program.ToggleIcon = "Power";
                        program.ToggleColor = new SolidColorBrush(Colors.Red);
                    }
                    
                    StartupDataGrid.Items.Refresh();
                    MessageBox.Show($"Enabled {disabledPrograms.Count} startup programs.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisableAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var enabledPrograms = _startupPrograms.Where(p => p.Enabled).ToList();
                
                if (enabledPrograms.Count == 0)
                {
                    MessageBox.Show("All startup programs are already disabled.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to disable all {enabledPrograms.Count} enabled startup programs?\n\nThis may affect system functionality.",
                    "Confirm Disable All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var program in enabledPrograms)
                    {
                        _uninstallerService.SetStartupProgramEnabled(program.Id, false);
                        program.Enabled = false;
                        program.Status = "Disabled";
                        program.StatusColor = new SolidColorBrush(Colors.Gray);
                        program.ToggleIcon = "PowerOff";
                        program.ToggleColor = new SolidColorBrush(Colors.Green);
                    }
                    
                    StartupDataGrid.Items.Refresh();
                    MessageBox.Show($"Disabled {enabledPrograms.Count} startup programs.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStartupPrograms();
                MessageBox.Show("Startup programs list refreshed.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Add new startup program feature would open a dialog to add a custom startup entry.", 
                    "Add New", MessageBoxButton.OK, MessageBoxImage.Information);
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
                _navigationService.NavigateTo("Uninstaller_Programs");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class StartupProgramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Command { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }
        public bool IsWindowsService { get; set; }
        public DateTime? LastRunTime { get; set; }
        public int RunCount { get; set; }
        public string Status { get; set; }
        public SolidColorBrush StatusColor { get; set; }
        public string ToggleIcon { get; set; }
        public SolidColorBrush ToggleColor { get; set; }
    }
}

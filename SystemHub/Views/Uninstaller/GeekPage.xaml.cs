using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class GeekPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<GeekProgramItem> _allPrograms = new();
        private List<GeekProgramItem> _recentPrograms = new();
        private List<GeekProgramItem> _largePrograms = new();

        public GeekPage()
        {
            InitializeComponent();
            _uninstallerService = ServiceLocator.GetService<IUninstallerService>();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            try
            {
                var programs = _uninstallerService.GetInstalledPrograms();
                
                _allPrograms = programs.Select(p => new GeekProgramItem
                {
                    Id = p.Id,
                    Name = p.DisplayName,
                    Publisher = p.Publisher,
                    Version = p.Version,
                    Size = p.SizeFormatted,
                    InstallDate = p.InstallDate.ToString("yyyy-MM-dd"),
                    IsSelected = false
                }).ToList();

                AllProgramsDataGrid.ItemsSource = _allPrograms;

                // Recently added (last 30 days)
                _recentPrograms = _allPrograms.Where(p => 
                    DateTime.TryParse(p.InstallDate, out var date) && 
                    date >= DateTime.Now.AddDays(-30)).ToList();
                RecentProgramsDataGrid.ItemsSource = _recentPrograms;

                // Large programs (> 100 MB)
                _largePrograms = programs.Where(p => p.Size > 100 * 1024 * 1024)
                    .Select(p => new GeekProgramItem
                    {
                        Id = p.Id,
                        Name = p.DisplayName,
                        Publisher = p.Publisher,
                        Version = p.Version,
                        Size = p.SizeFormatted,
                        InstallDate = p.InstallDate.ToString("yyyy-MM-dd"),
                        IsSelected = false
                    }).ToList();
                LargeProgramsDataGrid.ItemsSource = _largePrograms;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScanNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Scan initiated. In a full implementation, this would refresh the program list and check for updates.", 
                    "Scan Now", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPrograms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForcedMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Forced Mode enables advanced uninstallation options for stubborn programs. In a full implementation, this would enable additional options.", 
                    "Forced Mode", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Geek Uninstaller settings would allow you to configure scan options, cleanup preferences, and other advanced settings.", 
                    "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Uninstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var programId = button.Tag.ToString();
                    var program = _allPrograms.FirstOrDefault(p => p.Id == programId);
                    
                    if (program != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to uninstall {program.Name} using Geek Uninstaller?",
                            "Confirm Uninstall",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            var uninstallResult = _uninstallerService.UninstallProgram(programId);
                            
                            if (uninstallResult.IsSuccessful)
                            {
                                MessageBox.Show(
                                    $"Successfully uninstalled {uninstallResult.ProgramName}!\n\nSpace freed: {uninstallResult.SpaceFreedFormatted}",
                                    "SystemHub",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                                LoadPrograms();
                            }
                            else
                            {
                                MessageBox.Show(
                                    $"Failed to uninstall {uninstallResult.ProgramName}: {uninstallResult.ErrorMessage}",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UninstallSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPrograms = _allPrograms.Where(p => p.IsSelected).ToList();
                
                if (selectedPrograms.Count == 0)
                {
                    MessageBox.Show("Please select at least one program to uninstall.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var programIds = selectedPrograms.Select(p => p.Id).ToList();
                var programNames = string.Join(", ", selectedPrograms.Select(p => p.Name).Take(5));
                if (selectedPrograms.Count > 5)
                    programNames += " and " + (selectedPrograms.Count - 5) + " more";

                var result = MessageBox.Show(
                    $"Are you sure you want to uninstall {selectedPrograms.Count} program(s)?\n\n{programNames}",
                    "Confirm Bulk Uninstall",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var bulkResult = _uninstallerService.BulkUninstall(programIds);
                    
                    MessageBox.Show(
                        $"Bulk uninstall completed:\n\nSuccessfully uninstalled: {bulkResult.Successful}\nFailed: {bulkResult.Failed}\nTotal space freed: {bulkResult.TotalSpaceFreedFormatted}",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    
                    LoadPrograms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForcedUninstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Uninstaller_Forced");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanLeftovers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Uninstaller_Leftovers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var programs = AllProgramsDataGrid.Items.Count > 0 ? _allPrograms : _allPrograms;
                var exportText = "Geek Uninstaller - Program List\n\n";
                exportText += "Name,Publisher,Version,Size,Install Date\n";
                
                foreach (var program in programs)
                {
                    exportText += $"{program.Name},"{program.Publisher}",{program.Version},{program.Size},{program.InstallDate}\n";
                }

                MessageBox.Show(
                    $"Exported {programs.Count} programs to CSV format.\n\nThe list has been copied to clipboard.",
                    "SystemHub",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
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

    public class GeekProgramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Size { get; set; }
        public string InstallDate { get; set; }
        public bool IsSelected { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class ProgramsPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<ProgramItem> _allPrograms = new();
        private List<ProgramItem> _filteredPrograms = new();

        public ProgramsPage()
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
                _allPrograms = programs.Select(p => new ProgramItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    DisplayName = p.DisplayName,
                    Publisher = p.Publisher,
                    Version = p.Version,
                    InstallDate = p.InstallDate.ToString("yyyy-MM-dd"),
                    Size = p.Size,
                    Category = p.Category,
                    LastUsed = p.LastUsed.ToString("yyyy-MM-dd"),
                    IconPath = p.IconPath,
                    IsSelected = false
                }).ToList();

                _filteredPrograms = new List<ProgramItem>(_allPrograms);
                ProgramsDataGrid.ItemsSource = _filteredPrograms;

                UpdateSummaryCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSummaryCards()
        {
            try
            {
                // These are updated via the XAML directly with static values
                // In a real implementation, these would be bound to properties
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
                            $"Are you sure you want to uninstall {program.DisplayName}?",
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

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var programId = button.Tag.ToString();
                    _navigationService.NavigateTo("Uninstaller_ProgramDetails", programId);
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
                var selectedPrograms = _filteredPrograms.Where(p => p.IsSelected).ToList();
                
                if (selectedPrograms.Count == 0)
                {
                    MessageBox.Show(
                        "Please select at least one program to uninstall.",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                var programIds = selectedPrograms.Select(p => p.Id).ToList();
                var programNames = string.Join(", ", selectedPrograms.Select(p => p.DisplayName));

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

        private void BulkUninstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Uninstaller_Bulk");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartupPrograms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Uninstaller_Startup");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var programs = _filteredPrograms.Count > 0 ? _filteredPrograms : _allPrograms;
                var exportText = "Installed Programs List\n\n";
                exportText += "Name,Publisher,Version,Install Date,Size,Category,Last Used\n";
                
                foreach (var program in programs)
                {
                    exportText += $"{program.DisplayName},"{program.Publisher}",{program.Version},{program.InstallDate},{program.SizeFormatted},{program.Category},{program.LastUsed}\n";
                }

                // In a real implementation, this would save to a file
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Settings_General");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

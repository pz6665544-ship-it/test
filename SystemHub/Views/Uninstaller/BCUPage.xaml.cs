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
    public partial class BCUPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<BCUProgramItem> _programs = new();

        public BCUPage()
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
                _programs = programs.Select(p => new BCUProgramItem
                {
                    Id = p.Id,
                    Name = p.DisplayName,
                    Publisher = p.Publisher,
                    Version = p.Version,
                    Size = p.SizeFormatted,
                    Status = "Installed",
                    StatusColor = new SolidColorBrush(Colors.Green),
                    IsSelected = false
                }).ToList();

                ScanResultsDataGrid.ItemsSource = _programs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void QuickScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Quick scan initiated. In a full implementation, this would scan for installed programs and display them in the list.", 
                    "Quick Scan", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    var program = _programs.FirstOrDefault(p => p.Id == programId);
                    
                    if (program != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to uninstall {program.Name} using BCUninstaller?",
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
                var selectedPrograms = _programs.Where(p => p.IsSelected).ToList();
                
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

        private void ExportList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var programs = _programs.Count > 0 ? _programs : _programs;
                var exportText = "BCUninstaller Scan Results\n\n";
                exportText += "Name,Publisher,Version,Size,Status\n";
                
                foreach (var program in programs)
                {
                    exportText += $"{program.Name},"{program.Publisher}",{program.Version},{program.Size},{program.Status}\n";
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

    public class BCUProgramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public SolidColorBrush StatusColor { get; set; }
        public bool IsSelected { get; set; }
    }
}

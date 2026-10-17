using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class RevoPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<RevoProgramItem> _programs = new();

        public RevoPage()
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
                _programs = programs.Select(p => new RevoProgramItem
                {
                    Id = p.Id,
                    Name = p.DisplayName,
                    Publisher = p.Publisher,
                    Size = p.SizeFormatted,
                    IsSelected = false
                }).ToList();

                ProgramsDataGrid.ItemsSource = _programs;
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
                            $"Are you sure you want to uninstall {program.Name} using Revo Uninstaller?\n\nSelect the uninstall mode:",
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

        private void HunterMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var programId = button.Tag.ToString();
                    MessageBox.Show(
                        $"Hunter Mode activated for program: {_programs.FirstOrDefault(p => p.Id == programId)?.Name}\n\nIn a full implementation, this would open a detailed view for tracking all changes made by the program.",
                        "Hunter Mode",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
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

        private void HunterMode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Hunter Mode allows you to monitor program installations and changes in real-time. In a full implementation, this would open a separate window for monitoring.", 
                    "Hunter Mode", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Logs would show the detailed history of all uninstallation operations performed by Revo Uninstaller.", 
                    "Logs", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Revo Uninstaller settings would allow you to configure scan modes, monitoring options, and other preferences.", 
                    "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
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

    public class RevoProgramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Size { get; set; }
        public bool IsSelected { get; set; }
    }
}

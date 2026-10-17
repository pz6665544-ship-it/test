using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class ForcedPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<ProgramItem> _programs = new();

        public ForcedPage()
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
                _programs = programs.Select(p => new ProgramItem
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

                ProgramComboBox.ItemsSource = _programs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ForceUninstall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProgramComboBox.SelectedItem is ProgramItem selectedProgram)
                {
                    var result = MessageBox.Show(
                        $"Are you sure you want to FORCE UNINSTALL {selectedProgram.DisplayName}?\n\nThis may cause system instability!",
                        "Confirm Forced Uninstall",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Starting forced uninstall of {selectedProgram.DisplayName}\n");
                        Application.Current.Dispatcher.Invoke(() => { });

                        var uninstallResult = _uninstallerService.ForcedUninstall(selectedProgram.Id);
                        
                        if (uninstallResult.IsSuccessful)
                        {
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Successfully uninstalled {uninstallResult.ProgramName}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Space freed: {uninstallResult.SpaceFreedFormatted}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Files removed: {uninstallResult.FilesRemoved.Count}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Registry keys removed: {uninstallResult.RegistryKeysRemoved.Count}\n");

                            if (uninstallResult.RequiresReboot)
                            {
                                LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] WARNING: System reboot required!\n");
                            }

                            MessageBox.Show(
                                $"Successfully forced uninstalled {uninstallResult.ProgramName}!\n\nSpace freed: {uninstallResult.SpaceFreedFormatted}",
                                "SystemHub",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                            LoadPrograms();
                        }
                        else
                        {
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] ERROR: Failed to uninstall {uninstallResult.ProgramName}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Error: {uninstallResult.ErrorMessage}\n");

                            MessageBox.Show(
                                $"Failed to forced uninstall {uninstallResult.ProgramName}: {uninstallResult.ErrorMessage}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a program first.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                if (ProgramComboBox.SelectedItem is ProgramItem selectedProgram)
                {
                    var result = MessageBox.Show(
                        $"Are you sure you want to clean up leftovers for {selectedProgram.DisplayName}?\n\nThis will remove residual files and registry entries.",
                        "Confirm Cleanup",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Starting leftover cleanup for {selectedProgram.DisplayName}\n");

                        var cleanupResult = _uninstallerService.CleanupLeftovers(selectedProgram.Id);
                        
                        if (cleanupResult.IsSuccessful)
                        {
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Successfully cleaned up leftovers\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Files removed: {cleanupResult.FilesRemoved.Count}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Registry keys removed: {cleanupResult.RegistryKeysRemoved.Count}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Folders removed: {cleanupResult.FoldersRemoved.Count}\n");
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] Space freed: {cleanupResult.SpaceFreedFormatted}\n");

                            MessageBox.Show(
                                $"Successfully cleaned up leftovers for {cleanupResult.ProgramName}!\n\nSpace freed: {cleanupResult.SpaceFreedFormatted}",
                                "SystemHub",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                        else
                        {
                            LogTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] ERROR: Failed to clean up leftovers\n");
                            MessageBox.Show("Failed to clean up leftovers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a program first.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
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
                _navigationService.NavigateTo("Uninstaller_Programs");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

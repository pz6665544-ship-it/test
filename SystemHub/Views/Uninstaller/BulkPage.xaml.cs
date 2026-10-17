using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class BulkPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;
        private List<ProgramItem> _programs = new();

        public BulkPage()
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

                ProgramsDataGrid.ItemsSource = _programs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var program in _programs)
                {
                    program.IsSelected = true;
                }
                ProgramsDataGrid.Items.Refresh();
                UpdateSelectionCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectNone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var program in _programs)
                {
                    program.IsSelected = false;
                }
                ProgramsDataGrid.Items.Refresh();
                UpdateSelectionCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InvertSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var program in _programs)
                {
                    program.IsSelected = !program.IsSelected;
                }
                ProgramsDataGrid.Items.Refresh();
                UpdateSelectionCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSelectionCount()
        {
            var count = _programs.Count(p => p.IsSelected);
            // In a real implementation, update the UI to show the count
        }

        private void UninstallSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPrograms = _programs.Where(p => p.IsSelected).ToList();
                
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
                var programNames = string.Join(", ", selectedPrograms.Select(p => p.DisplayName).Take(5));
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
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

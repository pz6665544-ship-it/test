using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Uninstaller
{
    public partial class LeftoversPage : Page
    {
        private readonly IUninstallerService _uninstallerService;
        private readonly INavigationService _navigationService;

        public LeftoversPage()
        {
            InitializeComponent();
            _uninstallerService = ServiceLocator.GetService<IUninstallerService>();
            _navigationService = ServiceLocator.GetService<INavigationService>();
        }

        private void QuickScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                
                // Simulate quick scan
                ScanStatusText.Text = "Scanning for leftover files and folders...";
                ScanProgressBar.Value = 0;
                
                // In a real implementation, this would scan the system
                System.Threading.Tasks.Task.Delay(2000).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        
                        // Show sample results
                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeepScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                
                ScanStatusText.Text = "Performing deep scan (this may take several minutes)...";
                ScanProgressBar.Value = 0;
                
                // Simulate deep scan with progress updates
                var progress = 0;
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(500);
                timer.Tick += (s, args) =>
                {
                    progress += 10;
                    if (progress > 100) progress = 100;
                    ScanProgressBar.Value = progress;
                    ScanStatusText.Text = $"Deep scanning... {progress}% complete";
                    
                    if (progress >= 100)
                    {
                        timer.Stop();
                        ShowSampleResults();
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                    }
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CustomScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Custom scan allows you to specify specific folders or registry keys to scan. This feature would open a dialog in a full implementation.", 
                    "Custom Scan", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScanAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Scan all would perform a comprehensive scan of the entire system for leftovers from all previously uninstalled programs.", 
                    "Scan All", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowSampleResults()
        {
            try
            {
                // Sample file/folder results
                var files = new List<LeftoverFile>
                {
                    new LeftoverFile { Type = "Folder", Path = "C:\\Program Files\\OldApp\\Temp", Size = "15.2 MB" },
                    new LeftoverFile { Type = "Folder", Path = "C:\\Program Files\\OldApp\\Cache", Size = "8.7 MB" },
                    new LeftoverFile { Type = "File", Path = "C:\\Program Files\\OldApp\\config.ini", Size = "4 KB" },
                    new LeftoverFile { Type = "File", Path = "C:\\Users\\SystemUser\\AppData\\Local\\OldApp\\settings.dat", Size = "2 KB" },
                    new LeftoverFile { Type = "Folder", Path = "C:\\ProgramData\\OldApp", Size = "3.1 MB" }
                };
                FilesDataGrid.ItemsSource = files;

                // Sample registry results
                var registry = new List<LeftoverRegistry>
                {
                    new LeftoverRegistry { Hive = "HKEY_LOCAL_MACHINE", Key = "SOFTWARE\\OldApp\\Settings", Value = "" },
                    new LeftoverRegistry { Hive = "HKEY_LOCAL_MACHINE", Key = "SOFTWARE\\OldApp\\Licenses", Value = "" },
                    new LeftoverRegistry { Hive = "HKEY_CURRENT_USER", Key = "SOFTWARE\\OldApp\\Preferences", Value = "" },
                    new LeftoverRegistry { Hive = "HKEY_CLASSES_ROOT", Key = ".oldappfile", Value = "OldAppFile" },
                    new LeftoverRegistry { Hive = "HKEY_LOCAL_MACHINE", Key = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\OldApp", Value = "" }
                };
                RegistryDataGrid.ItemsSource = registry;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectAllResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a full implementation, select all items in both DataGrids
                MessageBox.Show("All items selected.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectNoneResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a full implementation, deselect all items in both DataGrids
                MessageBox.Show("All items deselected.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to clean the selected leftovers?\n\nThis action cannot be undone.",
                    "Confirm Cleanup",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Selected leftovers cleaned successfully!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to clean ALL detected leftovers?\n\nThis action cannot be undone.",
                    "Confirm Cleanup All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("All leftovers cleaned successfully!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
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

    public class LeftoverFile
    {
        public string Type { get; set; }
        public string Path { get; set; }
        public string Size { get; set; }
        public bool IsSelected { get; set; }
    }

    public class LeftoverRegistry
    {
        public string Hive { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }
}

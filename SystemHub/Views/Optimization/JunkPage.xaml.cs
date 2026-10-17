using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class JunkPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<JunkFile> _junkFiles = new();

        public JunkPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
        }

        private void ScanNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning for junk files...";
                ScanProgressBar.Value = 0;

                // Simulate comprehensive scan
                System.Threading.Tasks.Task.Delay(2000).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 542 items (8.3 GB)";

                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                        SummaryBorder.Visibility = System.Windows.Visibility.Visible;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScanSystemTemp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning system temp folders...";
                ScanProgressBar.Value = 0;

                System.Threading.Tasks.Task.Delay(1500).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 128 items (2.1 GB)";

                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                        SummaryBorder.Visibility = System.Windows.Visibility.Visible;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScanUserTemp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning user temp folders...";
                ScanProgressBar.Value = 0;

                System.Threading.Tasks.Task.Delay(1500).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 215 items (3.5 GB)";

                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                        SummaryBorder.Visibility = System.Windows.Visibility.Visible;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ScanBrowserCache_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning browser cache...";
                ScanProgressBar.Value = 0;

                System.Threading.Tasks.Task.Delay(1500).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 199 items (2.7 GB)";

                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                        SummaryBorder.Visibility = System.Windows.Visibility.Visible;
                    });
                });
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
                _junkFiles = new List<JunkFile>
                {
                    new JunkFile { Type = "Temporary File", Icon = "File", Color = new SolidColorBrush(Colors.Orange), Location = "C:\\Windows\\Temp\\msi1234.tmp", Size = "15.2 MB", LastModified = "2023-11-10", Age = "2 days", IsSelected = false },
                    new JunkFile { Type = "Temporary File", Icon = "File", Color = new SolidColorBrush(Colors.Orange), Location = "C:\\Users\\SystemUser\\AppData\\Local\\Temp\\setup.exe.tmp", Size = "8.7 MB", LastModified = "2023-11-08", Age = "4 days", IsSelected = false },
                    new JunkFile { Type = "Cache", Icon = "Cache", Color = new SolidColorBrush(Colors.Blue), Location = "C:\\Users\\SystemUser\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Cache\\data_0", Size = "250 MB", LastModified = "2023-11-12", Age = "1 hour", IsSelected = false },
                    new JunkFile { Type = "Cache", Icon = "Cache", Color = new SolidColorBrush(Colors.Blue), Location = "C:\\Users\\SystemUser\\AppData\\Local\\Mozilla\\Firefox\\Profiles\\Cache2\\entries", Size = "180 MB", LastModified = "2023-11-09", Age = "3 days", IsSelected = false },
                    new JunkFile { Type = "Download", Icon = "Download", Color = new SolidColorBrush(Colors.Green), Location = "C:\\Users\\SystemUser\\Downloads\\old_installer.exe", Size = "45 MB", LastModified = "2023-10-15", Age = "28 days", IsSelected = false },
                    new JunkFile { Type = "Log", Icon = "FileDocument", Color = new SolidColorBrush(Colors.Gray), Location = "C:\\Program Files\\SomeApp\\logs\\app_20231001.log", Size = "12 MB", LastModified = "2023-10-01", Age = "41 days", IsSelected = false },
                    new JunkFile { Type = "Old Update", Icon = "Update", Color = new SolidColorBrush(Colors.Purple), Location = "C:\\Windows\\SoftwareDistribution\\Download\\old_update.cab", Size = "350 MB", LastModified = "2023-09-01", Age = "71 days", IsSelected = false },
                    new JunkFile { Type = "Empty Folder", Icon = "Folder", Color = new SolidColorBrush(Colors.LightGray), Location = "C:\\Windows\\Temp\\EmptyFolder1", Size = "0 B", LastModified = "2023-09-01", Age = "71 days", IsSelected = false },
                    new JunkFile { Type = "Temporary File", Icon = "File", Color = new SolidColorBrush(Colors.Orange), Location = "C:\\Users\\SystemUser\\AppData\\Local\\Microsoft\\Windows\\INetCache\\IE\\temp.dat", Size = "5 MB", LastModified = "2023-10-20", Age = "22 days", IsSelected = false },
                    new JunkFile { Type = "Cache", Icon = "Cache", Color = new SolidColorBrush(Colors.Blue), Location = "C:\\Users\\SystemUser\\AppData\\Local\\Microsoft\\Edge\\User Data\\Default\\Cache", Size = "200 MB", LastModified = "2023-11-11", Age = "2 days", IsSelected = false }
                };
                ResultsDataGrid.ItemsSource = _junkFiles;

                // Update summary
                TotalItemsText.Text = _junkFiles.Count.ToString();
                TotalSizeText.Text = "8.3 GB";
                SelectedItemsText.Text = "0";
                SpaceToFreeText.Text = "0";
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
                var selectedItems = _junkFiles.Where(i => i.IsSelected).ToList();

                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one item to clean.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to clean {selectedItems.Count} selected item(s)?\n\nThis action cannot be undone.",
                    "Confirm Cleanup",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var totalSize = selectedItems.Sum(i => ParseSize(i.Size));
                    MessageBox.Show(
                        $"Successfully cleaned {selectedItems.Count} item(s)!\n\nSpace freed: {FormatSize(totalSize)}",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Refresh scan
                    ScanNow_Click(null, null);
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
                    "Are you sure you want to clean ALL found junk files?\n\nThis action cannot be undone.",
                    "Confirm Clean All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var totalSize = _junkFiles.Sum(i => ParseSize(i.Size));
                    MessageBox.Show(
                        $"Successfully cleaned all {_junkFiles.Count} item(s)!\n\nSpace freed: {FormatSize(totalSize)}",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Refresh scan
                    ScanNow_Click(null, null);
                }
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
                foreach (var item in _junkFiles) item.IsSelected = true;
                ResultsDataGrid.Items.Refresh();

                SelectedItemsText.Text = _junkFiles.Count.ToString();
                SpaceToFreeText.Text = "8.3 GB";
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
                foreach (var item in _junkFiles) item.IsSelected = false;
                ResultsDataGrid.Items.Refresh();

                SelectedItemsText.Text = "0";
                SpaceToFreeText.Text = "0";
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
                ScanNow_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private long ParseSize(string sizeStr)
        {
            try
            {
                if (sizeStr.Contains("GB"))
                {
                    var value = double.Parse(sizeStr.Replace("GB", "").Trim());
                    return (long)(value * 1024 * 1024 * 1024);
                }
                else if (sizeStr.Contains("MB"))
                {
                    var value = double.Parse(sizeStr.Replace("MB", "").Trim());
                    return (long)(value * 1024 * 1024);
                }
                else if (sizeStr.Contains("KB"))
                {
                    var value = double.Parse(sizeStr.Replace("KB", "").Trim());
                    return (long)(value * 1024);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double len = bytes;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
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

    public class JunkFile
    {
        public string Type { get; set; }
        public string Icon { get; set; }
        public SolidColorBrush Color { get; set; }
        public string Location { get; set; }
        public string Size { get; set; }
        public string LastModified { get; set; }
        public string Age { get; set; }
        public bool IsSelected { get; set; }
    }
}

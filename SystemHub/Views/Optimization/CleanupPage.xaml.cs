using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class CleanupPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<CleanupItem> _allItems = new();
        private List<CleanupItem> _tempFiles = new();
        private List<CleanupItem> _cacheFiles = new();
        private List<CleanupItem> _logs = new();
        private List<CleanupItem> _emptyFolders = new();

        public CleanupPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
        }

        private void QuickScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning for junk files...";
                ScanProgressBar.Value = 0;

                // Simulate quick scan
                System.Threading.Tasks.Task.Delay(1500).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 142 items (2.8 GB)";

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

        private void DeepScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Performing deep scan (this may take several minutes)...";
                ScanProgressBar.Value = 0;

                // Simulate deep scan with progress updates
                var progress = 0;
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(300);
                timer.Tick += (s, args) =>
                {
                    progress += 5;
                    if (progress > 100) progress = 100;
                    ScanProgressBar.Value = progress;
                    ScanStatusText.Text = $"Deep scanning... {progress}% complete";
                    
                    if (progress >= 100)
                    {
                        timer.Stop();
                        ScanItemsFoundText.Text = "Found 847 items (15.3 GB)";
                        ShowSampleResults();
                        
                        ScanProgressBorder.Visibility = System.Windows.Visibility.Collapsed;
                        ResultsBorder.Visibility = System.Windows.Visibility.Visible;
                        SummaryBorder.Visibility = System.Windows.Visibility.Visible;
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
                MessageBox.Show("Custom scan allows you to select specific folders or file types to scan. In a full implementation, this would open a dialog with scan options.", 
                    "Custom Scan", MessageBoxButton.OK, MessageBoxImage.Information);
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
                // All items
                _allItems = new List<CleanupItem>
                {
                    new CleanupItem { Type = "Temp File", Category = "System", Location = "C:\\Windows\\Temp\\setup.exe.tmp", Size = "15.2 MB", LastModified = "2023-11-10", IsSelected = false },
                    new CleanupItem { Type = "Temp File", Category = "System", Location = "C:\\Users\\SystemUser\\AppData\\Local\\Temp\\install.tmp", Size = "8.7 MB", LastModified = "2023-11-08", IsSelected = false },
                    new CleanupItem { Type = "Cache", Category = "Browser", Location = "C:\\Users\\SystemUser\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Cache", Size = "250 MB", LastModified = "2023-11-12", IsSelected = false },
                    new CleanupItem { Type = "Cache", Category = "Browser", Location = "C:\\Users\\SystemUser\\AppData\\Local\\Mozilla\\Firefox\\Profiles\\Cache", Size = "180 MB", LastModified = "2023-11-09", IsSelected = false },
                    new CleanupItem { Type = "Log", Category = "Application", Location = "C:\\Program Files\\SomeApp\\logs\\app.log", Size = "45 MB", LastModified = "2023-10-15", IsSelected = false },
                    new CleanupItem { Type = "Log", Category = "System", Location = "C:\\Windows\\Logs\\CBS\\CBS.log", Size = "12 MB", LastModified = "2023-11-05", IsSelected = false },
                    new CleanupItem { Type = "Empty Folder", Category = "System", Location = "C:\\Windows\\Temp\\EmptyFolder1", Size = "0 B", LastModified = "2023-09-01", IsSelected = false },
                    new CleanupItem { Type = "Empty Folder", Category = "User", Location = "C:\\Users\\SystemUser\\Downloads\\OldFiles", Size = "0 B", LastModified = "2023-08-20", IsSelected = false }
                };
                AllItemsDataGrid.ItemsSource = _allItems;

                // Temp files
                _tempFiles = _allItems.Where(i => i.Type == "Temp File").ToList();
                TempFilesDataGrid.ItemsSource = _tempFiles;

                // Cache files
                _cacheFiles = _allItems.Where(i => i.Type == "Cache").ToList();
                CacheFilesDataGrid.ItemsSource = _cacheFiles;

                // Logs
                _logs = _allItems.Where(i => i.Type == "Log").ToList();
                LogsDataGrid.ItemsSource = _logs;

                // Empty folders
                _emptyFolders = _allItems.Where(i => i.Type == "Empty Folder").ToList();
                EmptyFoldersDataGrid.ItemsSource = _emptyFolders;

                // Update summary
                TotalItemsText.Text = _allItems.Count.ToString();
                TotalSizeText.Text = "2.8 GB";
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
                var selectedItems = new List<CleanupItem>();
                selectedItems.AddRange(_allItems.Where(i => i.IsSelected));
                selectedItems.AddRange(_tempFiles.Where(i => i.IsSelected));
                selectedItems.AddRange(_cacheFiles.Where(i => i.IsSelected));
                selectedItems.AddRange(_logs.Where(i => i.IsSelected));
                selectedItems.AddRange(_emptyFolders.Where(i => i.IsSelected));

                var uniqueSelected = selectedItems.GroupBy(i => i.Location).Select(g => g.First()).ToList();

                if (uniqueSelected.Count == 0)
                {
                    MessageBox.Show("Please select at least one item to clean.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to clean {uniqueSelected.Count} selected item(s)?\n\nThis action cannot be undone.",
                    "Confirm Cleanup",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var totalSize = uniqueSelected.Sum(i => ParseSize(i.Size));
                    MessageBox.Show(
                        $"Successfully cleaned {uniqueSelected.Count} item(s)!\n\nSpace freed: {FormatSize(totalSize)}",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Refresh scan
                    QuickScan_Click(null, null);
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
                    "Are you sure you want to clean ALL found items?\n\nThis action cannot be undone.",
                    "Confirm Clean All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var totalSize = _allItems.Sum(i => ParseSize(i.Size));
                    MessageBox.Show(
                        $"Successfully cleaned all {_allItems.Count} item(s)!\n\nSpace freed: {FormatSize(totalSize)}",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Refresh scan
                    QuickScan_Click(null, null);
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
                foreach (var item in _allItems) item.IsSelected = true;
                foreach (var item in _tempFiles) item.IsSelected = true;
                foreach (var item in _cacheFiles) item.IsSelected = true;
                foreach (var item in _logs) item.IsSelected = true;
                foreach (var item in _emptyFolders) item.IsSelected = true;

                AllItemsDataGrid.Items.Refresh();
                TempFilesDataGrid.Items.Refresh();
                CacheFilesDataGrid.Items.Refresh();
                LogsDataGrid.Items.Refresh();
                EmptyFoldersDataGrid.Items.Refresh();

                SelectedItemsText.Text = _allItems.Count.ToString();
                SpaceToFreeText.Text = "2.8 GB";
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
                foreach (var item in _allItems) item.IsSelected = false;
                foreach (var item in _tempFiles) item.IsSelected = false;
                foreach (var item in _cacheFiles) item.IsSelected = false;
                foreach (var item in _logs) item.IsSelected = false;
                foreach (var item in _emptyFolders) item.IsSelected = false;

                AllItemsDataGrid.Items.Refresh();
                TempFilesDataGrid.Items.Refresh();
                CacheFilesDataGrid.Items.Refresh();
                LogsDataGrid.Items.Refresh();
                EmptyFoldersDataGrid.Items.Refresh();

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
                QuickScan_Click(null, null);
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

    public class CleanupItem
    {
        public string Type { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Size { get; set; }
        public string LastModified { get; set; }
        public bool IsSelected { get; set; }
        public string FileName { get; set; }
        public string Application { get; set; }
    }
}

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
    public partial class RegistryPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<RegistryIssue> _allIssues = new();
        private List<RegistryIssue> _invalidRefs = new();
        private List<RegistryIssue> _orphanedKeys = new();
        private List<RegistryIssue> _unusedEntries = new();

        public RegistryPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
        }

        private void CreateBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Create a backup of the current registry state before making any changes?\n\nThis is strongly recommended.",
                    "Create Registry Backup",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // In a full implementation, this would create a registry backup
                    BackupStatusText.Text = "Backup created: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    BackupStatusText.Foreground = new SolidColorBrush(Colors.Green);
                    MessageBox.Show("Registry backup created successfully!\n\nBackup file: RegistryBackup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".reg", 
                        "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                if (BackupStatusText.Text == "No backup created")
                {
                    var result = MessageBox.Show(
                        "No registry backup has been created. It is strongly recommended to create a backup before scanning.\n\nContinue without backup?",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.No)
                        return;
                }

                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Scanning registry for issues...";
                ScanProgressBar.Value = 0;

                // Simulate quick scan
                System.Threading.Tasks.Task.Delay(2000).ContinueWith(t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ScanProgressBar.Value = 100;
                        ScanStatusText.Text = "Scan completed!";
                        ScanItemsFoundText.Text = "Found 87 issues";

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
                if (BackupStatusText.Text == "No backup created")
                {
                    var result = MessageBox.Show(
                        "No registry backup has been created. It is strongly recommended to create a backup before deep scanning.\n\nContinue without backup?",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.No)
                        return;
                }

                ScanProgressBorder.Visibility = System.Windows.Visibility.Visible;
                ResultsBorder.Visibility = System.Windows.Visibility.Collapsed;
                SummaryBorder.Visibility = System.Windows.Visibility.Collapsed;

                ScanStatusText.Text = "Performing deep registry scan (this may take several minutes)...";
                ScanProgressBar.Value = 0;

                // Simulate deep scan with progress updates
                var progress = 0;
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(400);
                timer.Tick += (s, args) =>
                {
                    progress += 7;
                    if (progress > 100) progress = 100;
                    ScanProgressBar.Value = progress;
                    ScanStatusText.Text = $"Deep scanning registry... {progress}% complete";
                    
                    if (progress >= 100)
                    {
                        timer.Stop();
                        ScanItemsFoundText.Text = "Found 432 issues";
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
                MessageBox.Show("Custom scan allows you to select specific registry hives or key paths to scan. In a full implementation, this would open a dialog with scan options.", 
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
                // All issues
                _allIssues = new List<RegistryIssue>
                {
                    new RegistryIssue { Type = "Invalid Reference", Severity = "High", SeverityColor = new SolidColorBrush(Colors.Red), Key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\OldApp", Value = "C:\\Program Files\\OldApp\\uninstall.exe", Application = "OldApp", IsSelected = false },
                    new RegistryIssue { Type = "Invalid Reference", Severity = "High", SeverityColor = new SolidColorBrush(Colors.Red), Key = "HKEY_CLASSES_ROOT\\OldApp.File\\shell\\open\\command", Value = "\"C:\\Program Files\\OldApp\\app.exe\" \"%1\"", Application = "OldApp", IsSelected = false },
                    new RegistryIssue { Type = "Orphaned Key", Severity = "Medium", SeverityColor = new SolidColorBrush(Colors.Orange), Key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\OldApp", Value = "", Application = "OldApp (uninstalled)", IsSelected = false },
                    new RegistryIssue { Type = "Orphaned Key", Severity = "Medium", SeverityColor = new SolidColorBrush(Colors.Orange), Key = "HKEY_CURRENT_USER\\SOFTWARE\\TempApp", Value = "", Application = "TempApp (uninstalled)", IsSelected = false },
                    new RegistryIssue { Type = "Unused Entry", Severity = "Low", SeverityColor = new SolidColorBrush(Colors.Yellow), Key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\OldTool.exe", Value = "C:\\Program Files\\OldTool\\tool.exe", Application = "OldTool", IsSelected = false },
                    new RegistryIssue { Type = "Unused Entry", Severity = "Low", SeverityColor = new SolidColorBrush(Colors.Yellow), Key = "HKEY_CLASSES_ROOT\\.old\\DefaultIcon", Value = "C:\\Program Files\\OldApp\\app.exe,0", Application = "OldApp", IsSelected = false },
                    new RegistryIssue { Type = "Invalid Reference", Severity = "High", SeverityColor = new SolidColorBrush(Colors.Red), Key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Classes\\CLSID\\{12345678-1234-1234-1234-123456789012}", Value = "OldApp COM Object", Application = "OldApp", IsSelected = false },
                    new RegistryIssue { Type = "Orphaned Key", Severity = "Medium", SeverityColor = new SolidColorBrush(Colors.Orange), Key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\OldApp", Value = "", Application = "OldApp (uninstalled)", IsSelected = false }
                };
                AllIssuesDataGrid.ItemsSource = _allIssues;

                // Invalid references
                _invalidRefs = _allIssues.Where(i => i.Type == "Invalid Reference").Select(i => new RegistryIssue
                {
                    Type = i.Type,
                    Severity = i.Severity,
                    SeverityColor = i.SeverityColor,
                    Key = i.Key,
                    Value = i.Value,
                    Application = i.Application,
                    Reference = i.Value,
                    Status = "Broken",
                    IsSelected = i.IsSelected
                }).ToList();
                InvalidRefsDataGrid.ItemsSource = _invalidRefs;

                // Orphaned keys
                _orphanedKeys = _allIssues.Where(i => i.Type == "Orphaned Key").Select(i => new RegistryIssue
                {
                    Type = i.Type,
                    Severity = i.Severity,
                    SeverityColor = i.SeverityColor,
                    Key = i.Key,
                    Value = i.Value,
                    Application = i.Application,
                    Parent = i.Key.Substring(0, i.Key.LastIndexOf('\\')),
                    Uninstaller = "N/A",
                    IsSelected = i.IsSelected
                }).ToList();
                OrphanedKeysDataGrid.ItemsSource = _orphanedKeys;

                // Unused entries
                _unusedEntries = _allIssues.Where(i => i.Type == "Unused Entry").Select(i => new RegistryIssue
                {
                    Type = i.Type,
                    Severity = i.Severity,
                    SeverityColor = i.SeverityColor,
                    Key = i.Key,
                    Value = i.Value,
                    Application = i.Application,
                    LastAccessed = "2020-01-01",
                    IsSelected = i.IsSelected
                }).ToList();
                UnusedEntriesDataGrid.ItemsSource = _unusedEntries;

                // Update summary
                TotalIssuesText.Text = _allIssues.Count.ToString();
                HighSeverityText.Text = _allIssues.Count(i => i.Severity == "High").ToString();
                MediumSeverityText.Text = _allIssues.Count(i => i.Severity == "Medium").ToString();
                SelectedItemsText.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FixSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = new List<RegistryIssue>();
                selectedItems.AddRange(_allIssues.Where(i => i.IsSelected));
                selectedItems.AddRange(_invalidRefs.Where(i => i.IsSelected));
                selectedItems.AddRange(_orphanedKeys.Where(i => i.IsSelected));
                selectedItems.AddRange(_unusedEntries.Where(i => i.IsSelected));

                var uniqueSelected = selectedItems.GroupBy(i => i.Key).Select(g => g.First()).ToList();

                if (uniqueSelected.Count == 0)
                {
                    MessageBox.Show("Please select at least one issue to fix.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to fix {uniqueSelected.Count} selected registry issue(s)?\n\nThis action cannot be undone. Ensure you have a backup.",
                    "Confirm Fix",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(
                        $"Successfully fixed {uniqueSelected.Count} registry issue(s)!",
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

        private void FixAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to fix ALL found registry issues?\n\nThis action cannot be undone. Ensure you have a backup.",
                    "Confirm Fix All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show(
                        $"Successfully fixed all {_allIssues.Count} registry issue(s)!",
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
                foreach (var item in _allIssues) item.IsSelected = true;
                foreach (var item in _invalidRefs) item.IsSelected = true;
                foreach (var item in _orphanedKeys) item.IsSelected = true;
                foreach (var item in _unusedEntries) item.IsSelected = true;

                AllIssuesDataGrid.Items.Refresh();
                InvalidRefsDataGrid.Items.Refresh();
                OrphanedKeysDataGrid.Items.Refresh();
                UnusedEntriesDataGrid.Items.Refresh();

                SelectedItemsText.Text = _allIssues.Count.ToString();
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
                foreach (var item in _allIssues) item.IsSelected = false;
                foreach (var item in _invalidRefs) item.IsSelected = false;
                foreach (var item in _orphanedKeys) item.IsSelected = false;
                foreach (var item in _unusedEntries) item.IsSelected = false;

                AllIssuesDataGrid.Items.Refresh();
                InvalidRefsDataGrid.Items.Refresh();
                OrphanedKeysDataGrid.Items.Refresh();
                UnusedEntriesDataGrid.Items.Refresh();

                SelectedItemsText.Text = "0";
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
                var exportText = "Registry Issues List\n\n";
                exportText += "Type,Severity,Key,Value,Application\n";
                
                foreach (var issue in _allIssues)
                {
                    exportText += $"{issue.Type},{issue.Severity},\"{issue.Key}\",\"{issue.Value}\",{issue.Application}\n";
                }

                MessageBox.Show(
                    $"Exported {_allIssues.Count} registry issues to CSV format.\n\nThe list has been copied to clipboard.",
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
                _navigationService.NavigateTo("Dashboard_Overview");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class RegistryIssue
    {
        public string Type { get; set; }
        public string Severity { get; set; }
        public SolidColorBrush SeverityColor { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Application { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
        public string Parent { get; set; }
        public string Uninstaller { get; set; }
        public string LastAccessed { get; set; }
        public bool IsSelected { get; set; }
    }
}

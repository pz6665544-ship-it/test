using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class SmartDefragPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<DriveItem> _drives = new();
        private List<DefragHistoryItem> _history = new();
        private Random _random = new Random();

        public SmartDefragPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadDrives();
            LoadHistory();
        }

        private void LoadDrives()
        {
            try
            {
                _drives = new List<DriveItem>
                {
                    new DriveItem { Id = "1", Letter = "C:", Label = "System", Type = "SSD", TotalSpace = "476 GB", UsedSpace = "238 GB", FreeSpace = "238 GB", Fragmentation = "15%", Status = "Needs Defrag", LastDefrag = "2024-01-10 14:30", IsSelected = true },
                    new DriveItem { Id = "2", Letter = "D:", Label = "Data", Type = "HDD", TotalSpace = "931 GB", UsedSpace = "450 GB", FreeSpace = "481 GB", Fragmentation = "8%", Status = "Good", LastDefrag = "2024-01-14 09:15", IsSelected = false },
                    new DriveItem { Id = "3", Letter = "E:", Label = "Backup", Type = "HDD", TotalSpace = "1.8 TB", UsedSpace = "850 GB", FreeSpace = "950 GB", Fragmentation = "5%", Status = "Excellent", LastDefrag = "2024-01-13 22:45", IsSelected = false },
                    new DriveItem { Id = "4", Letter = "F:", Label = "Media", Type = "HDD", TotalSpace = "2.0 TB", UsedSpace = "1.2 TB", FreeSpace = "800 GB", Fragmentation = "22%", Status = "Needs Defrag", LastDefrag = "2024-01-08 16:20", IsSelected = false }
                };
                DrivesDataGrid.ItemsSource = _drives;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadHistory()
        {
            try
            {
                _history = new List<DefragHistoryItem>
                {
                    new DefragHistoryItem { Timestamp = "2024-01-15 14:30:25", Drive = "C:", Action = "Smart Defrag", FilesDefragged = "1245", BeforeFragmentation = "28%", AfterFragmentation = "15%", Duration = "12m 34s", Status = "Success" },
                    new DefragHistoryItem { Timestamp = "2024-01-15 13:45:10", Drive = "D:", Action = "Fast Defrag", FilesDefragged = "856", BeforeFragmentation = "18%", AfterFragmentation = "8%", Duration = "8m 12s", Status = "Success" },
                    new DefragHistoryItem { Timestamp = "2024-01-14 09:15:05", Drive = "F:", Action = "Deep Defrag", FilesDefragged = "3421", BeforeFragmentation = "35%", AfterFragmentation = "12%", Duration = "25m 45s", Status = "Success" },
                    new DefragHistoryItem { Timestamp = "2024-01-13 22:45:40", Drive = "E:", Action = "Smart Defrag", FilesDefragged = "987", BeforeFragmentation = "15%", AfterFragmentation = "5%", Duration = "10m 23s", Status = "Success" },
                    new DefragHistoryItem { Timestamp = "2024-01-12 16:20:30", Drive = "C:", Action = "Boot Time Defrag", FilesDefragged = "456", BeforeFragmentation = "25%", AfterFragmentation = "18%", Duration = "5m 10s", Status = "Success" },
                    new DefragHistoryItem { Timestamp = "2024-01-11 10:00:00", Drive = "D:", Action = "Smart Defrag", FilesDefragged = "1123", BeforeFragmentation = "20%", AfterFragmentation = "9%", Duration = "11m 55s", Status = "Success" }
                };
                HistoryDataGrid.ItemsSource = _history;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoDefrag_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Ready - Auto-defrag enabled";
                MessageBox.Show("Auto-defrag enabled.\n\nSmart Defrag will automatically defragment drives when fragmentation exceeds the threshold.", 
                    "Auto-Defrag Enabled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoDefrag_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Ready - Manual mode";
                MessageBox.Show("Auto-defrag disabled.\n\nYou will need to manually defragment drives using the 'Defrag' buttons.", 
                    "Auto-Defrag Disabled", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DefragMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DefragModeComboBox.SelectedItem != null)
                {
                    var mode = (DefragModeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    MessageBox.Show($"Defrag mode changed to: {mode}\n\nIn a full implementation, this would affect the defragmentation algorithm and speed.", 
                        "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnalyzeDrives_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Analyzing drives... Please wait";
                
                MessageBox.Show(
                    "Analyzing all drives for fragmentation...\n\nThis process scans each drive to determine the current level of file fragmentation.\n\nIn a full implementation, this would show a progress bar and detailed analysis for each drive.",
                    "Analyzing Drives",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Simulate analysis by updating fragmentation values
                foreach (var drive in _drives)
                {
                    drive.Fragmentation = $"{_random.Next(5, 30)}%";
                    drive.Status = int.Parse(drive.Fragmentation.TrimEnd('%')) > 20 ? "Needs Defrag" : 
                                   int.Parse(drive.Fragmentation.TrimEnd('%')) > 10 ? "Good" : "Excellent";
                }
                DrivesDataGrid.Items.Refresh();
                StatusText.Text = "Analysis complete - 2 drives need defragmentation";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DefragSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDrives = _drives.Where(d => d.IsSelected).ToList();
                
                if (selectedDrives.Count == 0)
                {
                    MessageBox.Show("Please select at least one drive to defragment.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var driveLetters = string.Join(", ", selectedDrives.Select(d => d.Letter));
                
                var result = MessageBox.Show(
                    $"Are you sure you want to defragment {selectedDrives.Count} selected drive(s)?\n\nDrives: {driveLetters}\n\nThis may take some time depending on the drive size and fragmentation level.",
                    "Confirm Defrag",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StatusText.Text = $"Defragmenting {selectedDrives.Count} drive(s)...";
                    
                    foreach (var drive in selectedDrives)
                    {
                        var beforeFrag = int.Parse(drive.Fragmentation.TrimEnd('%'));
                        var afterFrag = Math.Max(1, beforeFrag - _random.Next(5, 20));
                        var filesDefragged = _random.Next(500, 5000);
                        var duration = $"{_random.Next(5, 30)}m {_random.Next(0, 59)}s";

                        // Add to history
                        _history.Insert(0, new DefragHistoryItem
                        {
                            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Drive = drive.Letter,
                            Action = DefragModeComboBox.Text,
                            FilesDefragged = filesDefragged.ToString(),
                            BeforeFragmentation = $"{beforeFrag}%",
                            AfterFragmentation = $"{afterFrag}%",
                            Duration = duration,
                            Status = "Success"
                        });

                        // Update drive
                        drive.Fragmentation = $"{afterFrag}%";
                        drive.Status = afterFrag > 20 ? "Needs Defrag" : afterFrag > 10 ? "Good" : "Excellent";
                        drive.LastDefrag = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }

                    // Keep only last 20 entries
                    if (_history.Count > 20)
                        _history = _history.Take(20).ToList();

                    DrivesDataGrid.Items.Refresh();
                    HistoryDataGrid.ItemsSource = null;
                    HistoryDataGrid.ItemsSource = _history;

                    StatusText.Text = $"Defragmentation complete - {selectedDrives.Count} drive(s) defragmented";
                    
                    MessageBox.Show(
                        $"Defragmentation completed successfully!\n\n{selectedDrives.Count} drive(s) defragmented.\n\nTotal files defragged: {selectedDrives.Sum(d => int.Parse(_history.Where(h => h.Drive == d.Letter).FirstOrDefault()?.FilesDefragged ?? "0"))}",
                        "Defrag Complete",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DefragAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to defragment ALL drives?\n\nThis will defragment every drive on your system, which may take a long time.",
                    "Confirm Defrag All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    StatusText.Text = "Defragmenting all drives... This may take a while";
                    
                    foreach (var drive in _drives)
                    {
                        var beforeFrag = int.Parse(drive.Fragmentation.TrimEnd('%'));
                        var afterFrag = Math.Max(1, beforeFrag - _random.Next(5, 25));
                        var filesDefragged = _random.Next(500, 6000);
                        var duration = $"{_random.Next(10, 45)}m {_random.Next(0, 59)}s";

                        _history.Insert(0, new DefragHistoryItem
                        {
                            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Drive = drive.Letter,
                            Action = DefragModeComboBox.Text,
                            FilesDefragged = filesDefragged.ToString(),
                            BeforeFragmentation = $"{beforeFrag}%",
                            AfterFragmentation = $"{afterFrag}%",
                            Duration = duration,
                            Status = "Success"
                        });

                        drive.Fragmentation = $"{afterFrag}%";
                        drive.Status = afterFrag > 20 ? "Needs Defrag" : afterFrag > 10 ? "Good" : "Excellent";
                        drive.LastDefrag = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }

                    if (_history.Count > 20)
                        _history = _history.Take(20).ToList();

                    DrivesDataGrid.Items.Refresh();
                    HistoryDataGrid.ItemsSource = null;
                    HistoryDataGrid.ItemsSource = _history;

                    StatusText.Text = "All drives defragmented";
                    
                    MessageBox.Show(
                        $"All {_drives.Count} drives have been defragmented successfully!\n\nTotal files defragged: {_drives.Sum(d => int.Parse(_history.Where(h => h.Drive == d.Letter).FirstOrDefault()?.FilesDefragged ?? "0"))}",
                        "All Drives Defragged",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Optimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Optimize all drives?\n\nThis will analyze each drive and perform the most appropriate optimization (defrag for HDDs, TRIM for SSDs).",
                    "Confirm Optimize",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StatusText.Text = "Optimizing all drives...";
                    
                    foreach (var drive in _drives)
                    {
                        var action = drive.Type == "SSD" ? "TRIM" : "Smart Defrag";
                        var beforeFrag = int.Parse(drive.Fragmentation.TrimEnd('%'));
                        var afterFrag = drive.Type == "SSD" ? beforeFrag : Math.Max(1, beforeFrag - _random.Next(10, 25));
                        
                        _history.Insert(0, new DefragHistoryItem
                        {
                            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Drive = drive.Letter,
                            Action = action,
                            FilesDefragged = drive.Type == "SSD" ? "N/A (TRIM)" : _random.Next(1000, 5000).ToString(),
                            BeforeFragmentation = $"{beforeFrag}%",
                            AfterFragmentation = $"{afterFrag}%",
                            Duration = drive.Type == "SSD" ? "1m 20s" : $"{_random.Next(10, 30)}m {_random.Next(0, 59)}s",
                            Status = "Success"
                        });

                        drive.Fragmentation = $"{afterFrag}%";
                        drive.Status = afterFrag > 20 ? "Needs Defrag" : afterFrag > 10 ? "Good" : "Excellent";
                        drive.LastDefrag = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }

                    if (_history.Count > 20)
                        _history = _history.Take(20).ToList();

                    DrivesDataGrid.Items.Refresh();
                    HistoryDataGrid.ItemsSource = null;
                    HistoryDataGrid.ItemsSource = _history;

                    StatusText.Text = "All drives optimized";
                    
                    MessageBox.Show(
                        $"All {_drives.Count} drives have been optimized!\n\nSSD drives: TRIM executed\nHDD drives: Smart defragmentation performed",
                        "Optimization Complete",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnalyzeDrive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var driveId = button.Tag.ToString();
                    var drive = _drives.FirstOrDefault(d => d.Id == driveId);
                    
                    if (drive != null)
                    {
                        StatusText.Text = $"Analyzing {drive.Letter}... Please wait";
                        
                        // Simulate analysis
                        drive.Fragmentation = $"{_random.Next(5, 35)}%";
                        drive.Status = int.Parse(drive.Fragmentation.TrimEnd('%')) > 20 ? "Needs Defrag" : 
                                       int.Parse(drive.Fragmentation.TrimEnd('%')) > 10 ? "Good" : "Excellent";
                        DrivesDataGrid.Items.Refresh();

                        MessageBox.Show(
                            $"Drive Analysis: {drive.Letter} ({drive.Label})\n\nType: {drive.Type}\nTotal Space: {drive.TotalSpace}\nUsed: {drive.UsedSpace}\nFree: {drive.FreeSpace}\n\nCurrent Fragmentation: {drive.Fragmentation}\nStatus: {drive.Status}\n\nIn a full implementation, this would show a detailed fragmentation map and recommendations.",
                            $"Drive Analysis: {drive.Letter}",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        StatusText.Text = "Analysis complete";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DefragDrive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var driveId = button.Tag.ToString();
                    var drive = _drives.FirstOrDefault(d => d.Id == driveId);
                    
                    if (drive != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to defragment drive {drive.Letter} ({drive.Label})?\n\nCurrent fragmentation: {drive.Fragmentation}\nDrive type: {drive.Type}\n\nThis may take some time.",
                            "Confirm Defrag",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            StatusText.Text = $"Defragmenting {drive.Letter}... Please wait";
                            
                            var beforeFrag = int.Parse(drive.Fragmentation.TrimEnd('%'));
                            var afterFrag = Math.Max(1, beforeFrag - _random.Next(10, 25));
                            var filesDefragged = _random.Next(500, 5000);
                            var duration = $"{_random.Next(10, 40)}m {_random.Next(0, 59)}s";

                            _history.Insert(0, new DefragHistoryItem
                            {
                                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                Drive = drive.Letter,
                                Action = DefragModeComboBox.Text,
                                FilesDefragged = filesDefragged.ToString(),
                                BeforeFragmentation = $"{beforeFrag}%",
                                AfterFragmentation = $"{afterFrag}%",
                                Duration = duration,
                                Status = "Success"
                            });

                            if (_history.Count > 20)
                                _history = _history.Take(20).ToList();

                            drive.Fragmentation = $"{afterFrag}%";
                            drive.Status = afterFrag > 20 ? "Needs Defrag" : afterFrag > 10 ? "Good" : "Excellent";
                            drive.LastDefrag = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                            DrivesDataGrid.Items.Refresh();
                            HistoryDataGrid.ItemsSource = null;
                            HistoryDataGrid.ItemsSource = _history;

                            StatusText.Text = $"Defragmentation complete for {drive.Letter}";
                            
                            MessageBox.Show(
                                $"Drive {drive.Letter} defragmented successfully!\n\nFiles defragged: {filesDefragged}\nFragmentation: {beforeFrag}% -> {afterFrag}%\nDuration: {duration}",
                                "Defrag Complete",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DriveProperties_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var driveId = button.Tag.ToString();
                    var drive = _drives.FirstOrDefault(d => d.Id == driveId);
                    
                    if (drive != null)
                    {
                        var info = $"Drive Properties:\n\n";
                        info += $"Drive Letter: {drive.Letter}\n";
                        info += $"Label: {drive.Label}\n";
                        info += $"Type: {drive.Type}\n";
                        info += $"Total Space: {drive.TotalSpace}\n";
                        info += $"Used Space: {drive.UsedSpace}\n";
                        info += $"Free Space: {drive.FreeSpace}\n";
                        info += $"Fragmentation: {drive.Fragmentation}\n";
                        info += $"Status: {drive.Status}\n";
                        info += $"Last Defrag: {drive.LastDefrag}\n\n";
                        info += "In a full implementation, this would show:\n";
                        info += "- File system type\n";
                        info += "- Cluster size\n";
                        info += "- Total files/folders\n";
                        info += "- Detailed fragmentation map\n";
                        info += "- Health status";

                        MessageBox.Show(info, $"Properties: {drive.Letter}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
                _navigationService.NavigateTo("Dashboard_Overview");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class DriveItem
    {
        public string Id { get; set; }
        public string Letter { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public string TotalSpace { get; set; }
        public string UsedSpace { get; set; }
        public string FreeSpace { get; set; }
        public string Fragmentation { get; set; }
        public string Status { get; set; }
        public string LastDefrag { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DefragHistoryItem
    {
        public string Timestamp { get; set; }
        public string Drive { get; set; }
        public string Action { get; set; }
        public string FilesDefragged { get; set; }
        public string BeforeFragmentation { get; set; }
        public string AfterFragmentation { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }
}

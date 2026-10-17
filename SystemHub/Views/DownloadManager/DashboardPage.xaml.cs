using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.DownloadManager
{
    public partial class DashboardPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<ActiveDownloadItem> _activeDownloads = new();
        private List<QueueItem> _queueItems = new();

        public DashboardPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            try
            {
                // Sample active downloads
                _activeDownloads = new List<ActiveDownloadItem>
                {
                    new ActiveDownloadItem
                    {
                        Id = "1",
                        FileName = "Windows_11_23H2.iso",
                        Size = "5.2 GB",
                        Progress = "78%",
                        Speed = "12.5 MB/s",
                        TimeLeft = "2m 34s",
                        Status = "Downloading",
                        StatusIcon = "ArrowDown",
                        StatusColor = new SolidColorBrush(Colors.Blue)
                    },
                    new ActiveDownloadItem
                    {
                        Id = "2",
                        FileName = "Visual_Studio_2022.iso",
                        Size = "25.1 GB",
                        Progress = "15%",
                        Speed = "8.2 MB/s",
                        TimeLeft = "52m 12s",
                        Status = "Downloading",
                        StatusIcon = "ArrowDown",
                        StatusColor = new SolidColorBrush(Colors.Blue)
                    }
                };
                ActiveDownloadsDataGrid.ItemsSource = _activeDownloads;

                // Sample queue items
                _queueItems = new List<QueueItem>
                {
                    new QueueItem { Id = "1", QueueNumber = "1", FileName = "Office_365_Setup.exe", Size = "3.2 GB", Status = "Waiting" },
                    new QueueItem { Id = "2", QueueNumber = "2", FileName = "Adobe_Photoshop_2024.exe", Size = "2.1 GB", Status = "Waiting" },
                    new QueueItem { Id = "3", QueueNumber = "3", FileName = "Game_Launcher.msi", Size = "150 MB", Status = "Waiting" }
                };
                QueueDataGrid.ItemsSource = _queueItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("DownloadManager_New");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauseAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var download in _activeDownloads)
                {
                    download.Status = "Paused";
                    download.StatusIcon = "Pause";
                    download.StatusColor = new SolidColorBrush(Colors.Orange);
                    download.Speed = "0 B/s";
                }
                ActiveDownloadsDataGrid.Items.Refresh();
                MessageBox.Show("All downloads paused.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResumeAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var download in _activeDownloads)
                {
                    download.Status = "Downloading";
                    download.StatusIcon = "ArrowDown";
                    download.StatusColor = new SolidColorBrush(Colors.Blue);
                    download.Speed = "10 MB/s";
                }
                ActiveDownloadsDataGrid.Items.Refresh();
                MessageBox.Show("All downloads resumed.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to cancel all active downloads?",
                    "Confirm Cancel All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _activeDownloads.Clear();
                    ActiveDownloadsDataGrid.Items.Refresh();
                    MessageBox.Show("All downloads cancelled.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauseDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var downloadId = button.Tag.ToString();
                    var download = _activeDownloads.FirstOrDefault(d => d.Id == downloadId);
                    
                    if (download != null)
                    {
                        download.Status = "Paused";
                        download.StatusIcon = "Pause";
                        download.StatusColor = new SolidColorBrush(Colors.Orange);
                        download.Speed = "0 B/s";
                        ActiveDownloadsDataGrid.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var downloadId = button.Tag.ToString();
                    var download = _activeDownloads.FirstOrDefault(d => d.Id == downloadId);
                    
                    if (download != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to cancel {download.FileName}?",
                            "Confirm Cancel",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            _activeDownloads.Remove(download);
                            ActiveDownloadsDataGrid.Items.Refresh();
                            MessageBox.Show($"{download.FileName} cancelled.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _queueItems.FirstOrDefault(q => q.Id == itemId);
                    
                    if (item != null)
                    {
                        var index = _queueItems.IndexOf(item);
                        if (index > 0)
                        {
                            _queueItems.RemoveAt(index);
                            _queueItems.Insert(index - 1, item);
                            UpdateQueueNumbers();
                            QueueDataGrid.Items.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _queueItems.FirstOrDefault(q => q.Id == itemId);
                    
                    if (item != null)
                    {
                        var index = _queueItems.IndexOf(item);
                        if (index < _queueItems.Count - 1)
                        {
                            _queueItems.RemoveAt(index);
                            _queueItems.Insert(index + 1, item);
                            UpdateQueueNumbers();
                            QueueDataGrid.Items.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveFromQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _queueItems.FirstOrDefault(q => q.Id == itemId);
                    
                    if (item != null)
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to remove {item.FileName} from the queue?",
                            "Confirm Remove",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            _queueItems.Remove(item);
                            UpdateQueueNumbers();
                            QueueDataGrid.Items.Refresh();
                            MessageBox.Show($"{item.FileName} removed from queue.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateQueueNumbers()
        {
            for (int i = 0; i < _queueItems.Count; i++)
            {
                _queueItems[i].QueueNumber = (i + 1).ToString();
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

    public class ActiveDownloadItem
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Progress { get; set; }
        public string Speed { get; set; }
        public string TimeLeft { get; set; }
        public string Status { get; set; }
        public string StatusIcon { get; set; }
        public SolidColorBrush StatusColor { get; set; }
    }

    public class QueueItem
    {
        public string Id { get; set; }
        public string QueueNumber { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
    }
}

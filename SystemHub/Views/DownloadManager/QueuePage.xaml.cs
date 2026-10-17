using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.DownloadManager
{
    public partial class QueuePage : Page
    {
        private readonly INavigationService _navigationService;
        private List<QueueItem> _queueItems = new();

        public QueuePage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            try
            {
                _queueItems = new List<QueueItem>
                {
                    new QueueItem { Id = "1", QueueNumber = "1", FileName = "Windows_11_23H2.iso", Url = "https://example.com/win11.iso", Size = "5.2 GB", Status = "Waiting", Progress = "0%", IsSelected = false },
                    new QueueItem { Id = "2", QueueNumber = "2", FileName = "Visual_Studio_2022.iso", Url = "https://example.com/vs2022.iso", Size = "25.1 GB", Status = "Downloading", Progress = "15%", IsSelected = false },
                    new QueueItem { Id = "3", QueueNumber = "3", FileName = "Office_365_Setup.exe", Url = "https://example.com/office.exe", Size = "3.2 GB", Status = "Paused", Progress = "0%", IsSelected = false },
                    new QueueItem { Id = "4", QueueNumber = "4", FileName = "Adobe_Photoshop_2024.exe", Url = "https://example.com/ps.exe", Size = "2.1 GB", Status = "Waiting", Progress = "0%", IsSelected = false },
                    new QueueItem { Id = "5", QueueNumber = "5", FileName = "Game_Launcher.msi", Url = "https://example.com/game.msi", Size = "150 MB", Status = "Waiting", Progress = "0%", IsSelected = false }
                };
                QueueDataGrid.ItemsSource = _queueItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in _queueItems.Where(i => i.Status == "Waiting"))
                {
                    item.Status = "Downloading";
                }
                QueueDataGrid.Items.Refresh();
                MessageBox.Show("All waiting downloads started.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in _queueItems.Where(i => i.Status == "Downloading"))
                {
                    item.Status = "Stopped";
                    item.Progress = item.Progress; // Keep current progress
                }
                QueueDataGrid.Items.Refresh();
                MessageBox.Show("All active downloads stopped.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearCompleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var completedItems = _queueItems.Where(i => i.Progress == "100%" && i.Status == "Completed").ToList();
                if (completedItems.Count == 0)
                {
                    MessageBox.Show("No completed downloads to clear.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to remove {completedItems.Count} completed download(s)?",
                    "Confirm Clear",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in completedItems)
                    {
                        _queueItems.Remove(item);
                    }
                    UpdateQueueNumbers();
                    QueueDataGrid.Items.Refresh();
                    MessageBox.Show($"Removed {completedItems.Count} completed download(s).", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to clear ALL items from the queue?",
                    "Confirm Clear All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _queueItems.Clear();
                    QueueDataGrid.Items.Refresh();
                    MessageBox.Show("All queue items cleared.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Import would allow you to load a list of downloads from a file or URL list.", 
                    "Import", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _queueItems.FirstOrDefault(q => q.Id == itemId);
                    
                    if (item != null)
                    {
                        MessageBox.Show(
                            $"Edit download settings for: {item.FileName}\n\nIn a full implementation, this would open an edit dialog.",
                            "Edit Download",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
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
                _navigationService.NavigateTo("DownloadManager_Dashboard");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class QueueItem
    {
        public string Id { get; set; }
        public string QueueNumber { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public bool IsSelected { get; set; }
    }
}

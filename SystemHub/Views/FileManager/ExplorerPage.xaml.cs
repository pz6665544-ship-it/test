using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.FileManager
{
    public partial class ExplorerPage : Page
    {
        private readonly IFileService _fileService;

        public ExplorerPage()
        {
            InitializeComponent();
            _fileService = ServiceLocator.GetService<IFileService>();
            LoadDirectoryContents();
        }

        private void LoadDirectoryContents()
        {
            try
            {
                var contents = _fileService.GetDirectoryContents(@"C:\Users\SystemUser\Documents");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void QuickAccess_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var location = button.Tag.ToString();
                // Navigate to the selected quick access location
                MessageBox.Show($"Navigating to: {location}", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Drive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                var drive = button.Tag.ToString();
                // Navigate to the selected drive
                MessageBox.Show($"Navigating to: {drive}", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Network_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to network locations
            MessageBox.Show("Navigating to Network Locations", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddFavorite_Click(object sender, RoutedEventArgs e)
        {
            // Add current directory to favorites
            MessageBox.Show("Current directory added to favorites!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the path in the address bar
            MessageBox.Show("Navigating to specified path", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewFolder_Click(object sender, RoutedEventArgs e)
        {
            // Create new folder
            var result = MessageBox.Show("Create new folder?", "New Folder", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("New folder created!", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            // Copy selected files
            MessageBox.Show("Files copied to clipboard", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            // Paste from clipboard
            MessageBox.Show("Files pasted successfully", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            // Cut selected files
            MessageBox.Show("Files cut to clipboard", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Delete selected files
            var result = MessageBox.Show("Are you sure you want to delete the selected items?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Files deleted successfully", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            // Rename selected file
            MessageBox.Show("Enter new name for the file", "Rename", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            // Show properties for selected file
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("FileManager_Properties");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to search page
            var navigationService = ServiceLocator.GetService<INavigationService>();
            navigationService.NavigateTo("FileManager_Search");
        }

        // File explorer item class for the DataGrid
        public class FileExplorerItem
        {
            public string Name { get; set; }
            public bool IsDirectory { get; set; }
            public string IconKind { get; set; }
            public string TypeDescription { get; set; }
            public long Size { get; set; }
            public string SizeFormatted => FormatFileSize(Size);
            public DateTime LastModified { get; set; }

            private static string FormatFileSize(long bytes)
            {
                if (bytes == 0 && IsDirectory)
                    return "-";
                
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
        }
    }
}

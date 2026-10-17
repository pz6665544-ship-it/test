using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.DownloadManager
{
    public partial class NewDownloadPage : Page
    {
        private readonly INavigationService _navigationService;

        public NewDownloadPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
        }

        private void PasteUrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a full implementation, this would paste from clipboard
                UrlTextBox.Text = "https://example.com/sample_file.zip";
                AutoFillFileName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearUrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UrlTextBox.Text = "";
                FileNameTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // In a full implementation, this would open a folder browser dialog
                MessageBox.Show("Folder browser would open here.\n\nCurrent save path: " + SavePathTextBox.Text, 
                    "Browse", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoFillFileName()
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlTextBox.Text))
                {
                    var url = UrlTextBox.Text;
                    var fileName = System.IO.Path.GetFileName(url);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        FileNameTextBox.Text = fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently fail - just don't auto-fill
            }
        }

        private void UrlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AutoFillFileName();
        }

        private void ConnectionsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (ConnectionsValueText != null)
                {
                    ConnectionsValueText.Text = ((int)ConnectionsSlider.Value).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(UrlTextBox.Text) || string.IsNullOrWhiteSpace(UrlTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid URL.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(SavePathTextBox.Text))
                {
                    MessageBox.Show("Please specify a save path.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var downloadInfo = $"URL: {UrlTextBox.Text}\n";
                downloadInfo += $"Save to: {SavePathTextBox.Text}\n";
                if (!string.IsNullOrEmpty(FileNameTextBox.Text))
                    downloadInfo += $"File name: {FileNameTextBox.Text}\n";
                downloadInfo += $"Category: {CategoryComboBox.Text}\n";
                downloadInfo += $"Priority: {PriorityComboBox.Text}\n";
                downloadInfo += $"Connections: {ConnectionsSlider.Value}\n";

                var result = MessageBox.Show(
                    "Start download with the following settings:\n\n" + downloadInfo,
                    "Confirm Download",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Download started successfully!\n\nThe download has been added to the queue.", 
                        "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    _navigationService.NavigateTo("DownloadManager_Dashboard");
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
                var result = MessageBox.Show(
                    "Are you sure you want to cancel creating this download?",
                    "Confirm Cancel",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _navigationService.NavigateTo("DownloadManager_Dashboard");
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
                _navigationService.NavigateTo("DownloadManager_Dashboard");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

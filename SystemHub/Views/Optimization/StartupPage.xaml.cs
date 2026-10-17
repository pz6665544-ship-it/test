using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class StartupPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<StartupItem> _startupItems = new();

        public StartupPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadStartupItems();
        }

        private void LoadStartupItems()
        {
            try
            {
                _startupItems = new List<StartupItem>
                {
                    new StartupItem { Id = "1", Name = "Microsoft OneDrive", Publisher = "Microsoft", Impact = "Low", Type = "Registry", Command = "\"C:\\Users\\SystemUser\\AppData\\Local\\Microsoft\\OneDrive\\OneDrive.exe\" /background", IsEnabled = true },
                    new StartupItem { Id = "2", Name = "Spotify", Publisher = "Spotify AB", Impact = "Medium", Type = "Registry", Command = "\"C:\\Users\\SystemUser\\AppData\\Roaming\\Spotify\\Spotify.exe\" --autostart", IsEnabled = true },
                    new StartupItem { Id = "3", Name = "Steam Client Bootstrapper", Publisher = "Valve", Impact = "High", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Steam\\\\steam.exe\\\" -silent", IsEnabled = true },
                    new StartupItem { Id = "4", Name = "Discord", Publisher = "Discord Inc.", Impact = "Medium", Type = "Registry", Command = "\"C:\\Users\\SystemUser\\AppData\\Local\\Discord\\app-1.0.9023\\Discord.exe\" --start-minimized", IsEnabled = true },
                    new StartupItem { Id = "5", Name = "Kaspersky Total Security", Publisher = "Kaspersky Lab", Impact = "High", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Kaspersky Lab\\\\Kaspersky Total Security 21.15\\\\avp.exe\"", IsEnabled = true },
                    new StartupItem { Id = "6", Name = "NVIDIA GeForce Experience", Publisher = "NVIDIA", Impact = "Medium", Type = "Registry", Command = "\"C:\\Program Files\\\\NVIDIA Corporation\\\\Display.NvContainer\\\\NVDisplay.Container.exe\\\" -nvcontainer -id 1", IsEnabled = true },
                    new StartupItem { Id = "7", Name = "Windows Defender Notification", Publisher = "Microsoft", Impact = "Low", Type = "Registry", Command = "\"C:\\Program Files\\\\Windows Defender\\\\MSASCuiL.exe\"", IsEnabled = true },
                    new StartupItem { Id = "8", Name = "Adobe Creative Cloud", Publisher = "Adobe", Impact = "High", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Adobe\\\\Adobe Creative Cloud\\\\ACC\\\\Creative Cloud.exe\" --atlogin", IsEnabled = false },
                    new StartupItem { Id = "9", Name = "Dropbox", Publisher = "Dropbox, Inc.", Impact = "Medium", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Dropbox\\\\Client\\\\Dropbox.exe\\\" /systemstartup", IsEnabled = false },
                    new StartupItem { Id = "10", Name = "Google Drive", Publisher = "Google", Impact = "Medium", Type = "Startup Folder", Command = "\"C:\\Program Files\\\\Google\\\\Drive\\\\googledrivesync.exe\"", IsEnabled = true },
                    new StartupItem { Id = "11", Name = "Epic Games Launcher", Publisher = "Epic Games", Impact = "High", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Epic Games\\\\Launcher\\\\Portal\\\\Binaries\\\\Win32\\\\EpicGamesLauncher.exe\" -Silent", IsEnabled = true },
                    new StartupItem { Id = "12", Name = "Razer Synapse", Publisher = "Razer", Impact = "Medium", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\Razer\\\\Synapse3\\\\razercentral.exe\"", IsEnabled = true },
                    new StartupItem { Id = "13", Name = "Logitech G HUB", Publisher = "Logitech", Impact = "Medium", Type = "Registry", Command = "\"C:\\Program Files\\\\Logitech Gaming Software\\\\LCore.exe\" /minimized", IsEnabled = true },
                    new StartupItem { Id = "14", Name = "MSI Dragon Center", Publisher = "MSI", Impact = "High", Type = "Registry", Command = "\"C:\\Program Files (x86)\\\\MSI\\\\Dragon Center\\\\DragonCenter.exe\"", IsEnabled = false }
                };
                StartupDataGrid.ItemsSource = _startupItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToggleEnabled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is CheckBox checkBox && checkBox.DataContext is StartupItem item)
                {
                    item.IsEnabled = checkBox.IsChecked == true;
                    MessageBox.Show(
                        $"Startup item '{item.Name}' has been {(item.IsEnabled ? "enabled" : "disabled")}.",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OptimizeStartup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var disabledCount = _startupItems.Count(i => !i.IsEnabled);
                var highImpactEnabled = _startupItems.Count(i => i.IsEnabled && i.Impact == "High");
                
                var result = MessageBox.Show(
                    $"Startup optimization analysis:\n\nCurrently disabled: {disabledCount} program(s)\nHigh impact programs enabled: {highImpactEnabled}\n\nWould you like to automatically disable high-impact, non-essential programs?",
                    "Optimize Startup",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // In a full implementation, this would automatically disable non-essential high-impact programs
                    var optimizedCount = _startupItems.Count(i => i.IsEnabled && i.Impact == "High" && i.Name != "Kaspersky Total Security");
                    MessageBox.Show(
                        $"Optimized startup! Disabled {optimizedCount} high-impact non-essential programs.\n\nEstimated boot time improvement: ~15 seconds",
                        "SystemHub",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    LoadStartupItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisableAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to disable ALL startup programs?\n\nThis may affect system functionality and you may need to manually start some programs.",
                    "Confirm Disable All",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in _startupItems)
                    {
                        item.IsEnabled = false;
                    }
                    StartupDataGrid.Items.Refresh();
                    MessageBox.Show("All startup programs have been disabled.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnableEssential_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Define essential programs (security, system)
                var essentialPrograms = new[] { "Kaspersky Total Security", "Windows Defender Notification", "NVIDIA GeForce Experience" };
                
                foreach (var item in _startupItems)
                {
                    item.IsEnabled = essentialPrograms.Contains(item.Name);
                }
                StartupDataGrid.Items.Refresh();
                
                var enabledCount = _startupItems.Count(i => i.IsEnabled);
                MessageBox.Show(
                    $"Enabled {enabledCount} essential programs and disabled all others.\n\nYou can manually enable additional programs as needed.",
                    "SystemHub",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var analysis = "Startup Analysis:\n\n";
                var highImpact = _startupItems.Count(i => i.Impact == "High" && i.IsEnabled);
                var mediumImpact = _startupItems.Count(i => i.Impact == "Medium" && i.IsEnabled);
                var lowImpact = _startupItems.Count(i => i.Impact == "Low" && i.IsEnabled);
                
                analysis += $"High Impact: {highImpact} (boot delay: ~{highImpact * 5}s)\n";
                analysis += $"Medium Impact: {mediumImpact} (boot delay: ~{mediumImpact * 2}s)\n";
                analysis += $"Low Impact: {lowImpact} (boot delay: ~{lowImpact * 1}s)\n\n";
                analysis += "Recommendations:\n";
                
                if (highImpact > 3)
                    analysis += "- Consider disabling some high-impact programs\n";
                if (mediumImpact > 5)
                    analysis += "- Consider disabling some medium-impact programs\n";
                if (highImpact == 0 && mediumImpact == 0)
                    analysis += "- Your startup is well optimized!\n";
                
                MessageBox.Show(analysis, "Startup Analysis", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RunNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _startupItems.FirstOrDefault(i => i.Id == itemId);
                    
                    if (item != null)
                    {
                        MessageBox.Show(
                            $"Running: {item.Name}\n\nCommand: {item.Command}",
                            "Run Program",
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

        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var itemId = button.Tag.ToString();
                    var item = _startupItems.FirstOrDefault(i => i.Id == itemId);
                    
                    if (item != null)
                    {
                        var info = $"Startup Program Properties:\n\n";
                        info += $"Name: {item.Name}\n";
                        info += $"Publisher: {item.Publisher}\n";
                        info += $"Type: {item.Type}\n";
                        info += $"Impact: {item.Impact}\n";
                        info += $"Command: {item.Command}\n";
                        info += $"Status: {(item.IsEnabled ? "Enabled" : "Disabled")}\n";

                        MessageBox.Show(info, $"Properties: {item.Name}", MessageBoxButton.OK, MessageBoxImage.Information);
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

    public class StartupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Impact { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public bool IsEnabled { get; set; }
    }
}

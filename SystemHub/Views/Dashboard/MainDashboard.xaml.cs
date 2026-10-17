using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Dashboard
{
    public partial class MainDashboard : Window
    {
        private readonly INavigationService _navigationService;

        public MainDashboard()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            
            // Register all pages
            RegisterPages();
            
            // Set initial page
            _navigationService.Navigated += OnNavigated;
        }

        private void RegisterPages()
        {
            // Dashboard
            _navigationService.RegisterPage("Dashboard_Overview", typeof(OverviewPage));
            _navigationService.RegisterPage("Dashboard_SystemHealth", typeof(SystemHealthPage));

            // System Information
            _navigationService.RegisterPage("SystemInfo_Overview", typeof(SystemInfo.OverviewPage));
            _navigationService.RegisterPage("SystemInfo_CPU", typeof(SystemInfo.CpuPage));
            _navigationService.RegisterPage("SystemInfo_Memory", typeof(SystemInfo.MemoryPage));
            _navigationService.RegisterPage("SystemInfo_Motherboard", typeof(SystemInfo.MotherboardPage));
            _navigationService.RegisterPage("SystemInfo_GPU", typeof(SystemInfo.GpuPage));
            _navigationService.RegisterPage("SystemInfo_Storage", typeof(SystemInfo.StoragePage));
            _navigationService.RegisterPage("SystemInfo_Network", typeof(SystemInfo.NetworkPage));
            _navigationService.RegisterPage("SystemInfo_OS", typeof(SystemInfo.OsPage));
            _navigationService.RegisterPage("SystemInfo_Sensors", typeof(SystemInfo.SensorsPage));

            // Disk Tools
            _navigationService.RegisterPage("DiskTools_Overview", typeof(DiskTools.OverviewPage));
            _navigationService.RegisterPage("DiskTools_Health", typeof(DiskTools.HealthPage));
            _navigationService.RegisterPage("DiskTools_Usage", typeof(DiskTools.UsagePage));
            _navigationService.RegisterPage("DiskTools_Partitions", typeof(DiskTools.PartitionsPage));
            _navigationService.RegisterPage("DiskTools_Benchmark", typeof(DiskTools.BenchmarkPage));
            _navigationService.RegisterPage("DiskTools_Cleanup", typeof(DiskTools.CleanupPage));
            _navigationService.RegisterPage("DiskTools_Defrag", typeof(DiskTools.DefragPage));
            _navigationService.RegisterPage("DiskTools_FileExplorer", typeof(DiskTools.FileExplorerPage));
            _navigationService.RegisterPage("DiskTools_WizTree", typeof(DiskTools.WizTreePage));
            _navigationService.RegisterPage("DiskTools_TreeSize", typeof(DiskTools.TreeSizePage));
            _navigationService.RegisterPage("DiskTools_SpaceSniffer", typeof(DiskTools.SpaceSnifferPage));

            // File Manager
            _navigationService.RegisterPage("FileManager_Explorer", typeof(FileManager.ExplorerPage));
            _navigationService.RegisterPage("FileManager_Search", typeof(FileManager.SearchPage));
            _navigationService.RegisterPage("FileManager_Duplicates", typeof(FileManager.DuplicatesPage));
            _navigationService.RegisterPage("FileManager_LargeFiles", typeof(FileManager.LargeFilesPage));
            _navigationService.RegisterPage("FileManager_Comparison", typeof(FileManager.ComparisonPage));
            _navigationService.RegisterPage("FileManager_Rename", typeof(FileManager.RenamePage));
            _navigationService.RegisterPage("FileManager_SplitJoin", typeof(FileManager.SplitJoinPage));
            _navigationService.RegisterPage("FileManager_Archive", typeof(FileManager.ArchivePage));

            // Uninstaller
            _navigationService.RegisterPage("Uninstaller_Programs", typeof(Uninstaller.ProgramsPage));
            _navigationService.RegisterPage("Uninstaller_Bulk", typeof(Uninstaller.BulkPage));
            _navigationService.RegisterPage("Uninstaller_Forced", typeof(Uninstaller.ForcedPage));
            _navigationService.RegisterPage("Uninstaller_Leftovers", typeof(Uninstaller.LeftoversPage));
            _navigationService.RegisterPage("Uninstaller_Startup", typeof(Uninstaller.StartupPage));
            _navigationService.RegisterPage("Uninstaller_BCU", typeof(Uninstaller.BCUPage));
            _navigationService.RegisterPage("Uninstaller_Revo", typeof(Uninstaller.RevoPage));
            _navigationService.RegisterPage("Uninstaller_Geek", typeof(Uninstaller.GeekPage));

            // Download Manager
            _navigationService.RegisterPage("DownloadManager_Dashboard", typeof(DownloadManager.DashboardPage));
            _navigationService.RegisterPage("DownloadManager_New", typeof(DownloadManager.NewDownloadPage));
            _navigationService.RegisterPage("DownloadManager_Queue", typeof(DownloadManager.QueuePage));
            _navigationService.RegisterPage("DownloadManager_Completed", typeof(DownloadManager.CompletedPage));
            _navigationService.RegisterPage("DownloadManager_Settings", typeof(DownloadManager.SettingsPage));
            _navigationService.RegisterPage("DownloadManager_FDM", typeof(DownloadManager.FDMPage));
            _navigationService.RegisterPage("DownloadManager_IDM", typeof(DownloadManager.IDMPage));

            // Optimization
            _navigationService.RegisterPage("Optimization_Cleanup", typeof(Optimization.CleanupPage));
            _navigationService.RegisterPage("Optimization_Registry", typeof(Optimization.RegistryPage));
            _navigationService.RegisterPage("Optimization_Junk", typeof(Optimization.JunkPage));
            _navigationService.RegisterPage("Optimization_Memory", typeof(Optimization.MemoryPage));
            _navigationService.RegisterPage("Optimization_Startup", typeof(Optimization.StartupPage));
            _navigationService.RegisterPage("Optimization_Processes", typeof(Optimization.ProcessesPage));
            _navigationService.RegisterPage("Optimization_Services", typeof(Optimization.ServicesPage));
            _navigationService.RegisterPage("Optimization_WiseMemory", typeof(Optimization.WiseMemoryPage));
            _navigationService.RegisterPage("Optimization_SmartDefrag", typeof(Optimization.SmartDefragPage));

            // Network Tools
            _navigationService.RegisterPage("NetworkTools_Overview", typeof(NetworkTools.OverviewPage));
            _navigationService.RegisterPage("NetworkTools_PortScanner", typeof(NetworkTools.PortScannerPage));
            _navigationService.RegisterPage("NetworkTools_Ping", typeof(NetworkTools.PingPage));
            _navigationService.RegisterPage("NetworkTools_Traceroute", typeof(NetworkTools.TraceroutePage));
            _navigationService.RegisterPage("NetworkTools_Bandwidth", typeof(NetworkTools.BandwidthPage));
            _navigationService.RegisterPage("NetworkTools_Wireshark", typeof(NetworkTools.WiresharkPage));
            _navigationService.RegisterPage("NetworkTools_NetLimiter", typeof(NetworkTools.NetLimiterPage));
            _navigationService.RegisterPage("NetworkTools_iperf3", typeof(NetworkTools.iperf3Page));
            _navigationService.RegisterPage("NetworkTools_AnyDesk", typeof(NetworkTools.AnyDeskPage));
            _navigationService.RegisterPage("NetworkTools_KDEConnect", typeof(NetworkTools.KDEConnectPage));

            // Multimedia
            _navigationService.RegisterPage("Multimedia_Player", typeof(Multimedia.PlayerPage));
            _navigationService.RegisterPage("Multimedia_AudioRecorder", typeof(Multimedia.AudioRecorderPage));
            _navigationService.RegisterPage("Multimedia_ScreenRecorder", typeof(Multimedia.ScreenRecorderPage));
            _navigationService.RegisterPage("Multimedia_Screenshot", typeof(Multimedia.ScreenshotPage));
            _navigationService.RegisterPage("Multimedia_ShareX", typeof(Multimedia.ShareXPage));
            _navigationService.RegisterPage("Multimedia_Audacity", typeof(Multimedia.AudacityPage));
            _navigationService.RegisterPage("Multimedia_VLC", typeof(Multimedia.VLCPage));

            // Process Manager
            _navigationService.RegisterPage("ProcessManager_List", typeof(ProcessManager.ListPage));
            _navigationService.RegisterPage("ProcessManager_Explorer", typeof(ProcessManager.ExplorerPage));
            _navigationService.RegisterPage("ProcessManager_Monitor", typeof(ProcessManager.MonitorPage));
            _navigationService.RegisterPage("ProcessManager_RAMMap", typeof(ProcessManager.RAMMapPage));
            _navigationService.RegisterPage("ProcessManager_TaskManager", typeof(ProcessManager.TaskManagerPage));

            // Data Recovery
            _navigationService.RegisterPage("DataRecovery_File", typeof(DataRecovery.FilePage));
            _navigationService.RegisterPage("DataRecovery_Partition", typeof(DataRecovery.PartitionPage));
            _navigationService.RegisterPage("DataRecovery_Recuva", typeof(DataRecovery.RecuvaPage));
            _navigationService.RegisterPage("DataRecovery_TestDisk", typeof(DataRecovery.TestDiskPage));
            _navigationService.RegisterPage("DataRecovery_PhotoRec", typeof(DataRecovery.PhotoRecPage));
            _navigationService.RegisterPage("DataRecovery_DMDE", typeof(DataRecovery.DMDEPage));
            _navigationService.RegisterPage("DataRecovery_RStudio", typeof(DataRecovery.RStudioPage));
            _navigationService.RegisterPage("DataRecovery_DiskGenius", typeof(DataRecovery.DiskGeniusPage));

            // Utilities
            _navigationService.RegisterPage("Utilities_PowerToys", typeof(Utilities.PowerToysPage));
            _navigationService.RegisterPage("Utilities_Sysinternals", typeof(Utilities.SysinternalsPage));
            _navigationService.RegisterPage("Utilities_Clipboard", typeof(Utilities.ClipboardPage));
            _navigationService.RegisterPage("Utilities_AutoHotkey", typeof(Utilities.AutoHotkeyPage));
            _navigationService.RegisterPage("Utilities_Wallpaper", typeof(Utilities.WallpaperPage));
            _navigationService.RegisterPage("Utilities_DisplayFusion", typeof(Utilities.DisplayFusionPage));
            _navigationService.RegisterPage("Utilities_TwinkleTray", typeof(Utilities.TwinkleTrayPage));
            _navigationService.RegisterPage("Utilities_TranslucentTB", typeof(Utilities.TranslucentTBPage));
            _navigationService.RegisterPage("Utilities_LivelyWallpaper", typeof(Utilities.LivelyWallpaperPage));
            _navigationService.RegisterPage("Utilities_WinGet", typeof(Utilities.WinGetPage));
            _navigationService.RegisterPage("Utilities_UniGetUI", typeof(Utilities.UniGetUIPage));
            _navigationService.RegisterPage("Utilities_DriverBooster", typeof(Utilities.DriverBoosterPage));
            _navigationService.RegisterPage("Utilities_PCManager", typeof(Utilities.PCManagerPage));

            // Settings
            _navigationService.RegisterPage("Settings_General", typeof(Settings.GeneralPage));
            _navigationService.RegisterPage("Settings_Appearance", typeof(Settings.AppearancePage));
            _navigationService.RegisterPage("Settings_Notifications", typeof(Settings.NotificationsPage));
            _navigationService.RegisterPage("Settings_Updates", typeof(Settings.UpdatesPage));
            _navigationService.RegisterPage("Settings_About", typeof(Settings.AboutPage));
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                _navigationService.NavigateTo(button.Tag.ToString());
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Update MainFrame to show the current page
            var page = _navigationService.GetCurrentPage();
            if (page != null)
            {
                MainFrame.Content = page;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized 
                ? WindowState.Normal 
                : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

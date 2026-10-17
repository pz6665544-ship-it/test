using System;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Dashboard
{
    public partial class SystemHealthPage : Page
    {
        private readonly ISystemInfoService _systemInfoService;

        public SystemHealthPage()
        {
            InitializeComponent();
            _systemInfoService = ServiceLocator.GetService<ISystemInfoService>();
            LoadHealthData();
        }

        private void LoadHealthData()
        {
            try
            {
                // Load system health information
                var overview = _systemInfoService.GetSystemOverview();
                var cpuInfo = _systemInfoService.GetCpuInfo();
                var memoryInfo = _systemInfoService.GetMemoryInfo();
                var gpuInfo = _systemInfoService.GetGpuInfo();
                var sensors = _systemInfoService.GetTemperatureSensors();
                var fans = _systemInfoService.GetFanSensors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Recommendation item class for the ListView
        public class RecommendationItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Priority { get; set; }
        }
    }
}

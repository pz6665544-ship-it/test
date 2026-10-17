using System;
using System.Collections.Generic;
using SystemHub.Models;

namespace SystemHub.Services.Interfaces
{
    public interface ISystemInfoService
    {
        // System Overview
        SystemOverviewModel GetSystemOverview();
        
        // CPU Information
        CpuInfoModel GetCpuInfo();
        List<CpuCoreInfo> GetCpuCores();
        
        // Memory Information
        MemoryInfoModel GetMemoryInfo();
        
        // Motherboard Information
        MotherboardInfoModel GetMotherboardInfo();
        
        // GPU Information
        List<GpuInfoModel> GetGpuInfo();
        
        // Operating System Information
        OsInfoModel GetOsInfo();
        
        // Network Information
        List<NetworkAdapterInfo> GetNetworkAdapters();
        
        // BIOS Information
        BiosInfoModel GetBiosInfo();
        
        // Temperature Monitoring
        List<TemperatureSensor> GetTemperatureSensors();
        
        // Fan Speed Monitoring
        List<FanSensor> GetFanSensors();
        
        // Voltage Monitoring
        List<VoltageSensor> GetVoltageSensors();
        
        // System Uptime
        TimeSpan GetSystemUptime();
        
        // Performance Metrics
        PerformanceMetrics GetPerformanceMetrics();
    }

    // Models will be defined in Models folder
}

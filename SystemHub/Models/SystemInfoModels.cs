using System;
using System.Collections.Generic;

namespace SystemHub.Models
{
    // System Overview Model
    public class SystemOverviewModel
    {
        public string ComputerName { get; set; } = "DESKTOP-PC";
        public string UserName { get; set; } = "SystemUser";
        public string OsName { get; set; } = "Windows 11 Pro";
        public string OsVersion { get; set; } = "22H2";
        public string OsBuild { get; set; } = "22621.2428";
        public string SystemType { get; set; } = "64-bit Operating System, x64-based processor";
        public string SystemManufacturer { get; set; } = "Dell Inc.";
        public string SystemModel { get; set; } = "XPS 15 9520";
        public string SystemSerialNumber { get; set; } = "ABC123XYZ";
        public string BiosVersion { get; set; } = "Dell Inc. 1.20.0, 2023-01-15";
        public TimeSpan Uptime { get; set; } = TimeSpan.FromHours(48);
        public DateTime LastBootTime { get; set; } = DateTime.Now.AddHours(-48);
        
        public double CpuUsage { get; set; } = 25.5;
        public double MemoryUsage { get; set; } = 65.3;
        public double DiskUsage { get; set; } = 45.2;
        public double GpuUsage { get; set; } = 15.8;
        
        public List<PerformanceMetric> PerformanceMetrics { get; set; } = new();
    }

    // CPU Information
    public class CpuInfoModel
    {
        public string Name { get; set; } = "Intel(R) Core(TM) i9-12900H";
        public string Manufacturer { get; set; } = "Intel";
        public string Architecture { get; set; } = "x64";
        public int Cores { get; set; } = 14;
        public int LogicalProcessors { get; set; } = 20;
        public double BaseClock { get; set; } = 2.5; // GHz
        public double MaxClock { get; set; } = 5.0; // GHz
        public double CurrentClock { get; set; } = 3.2; // GHz
        public string Socket { get; set; } = "BGA 1744";
        public string Process { get; set; } = "Intel 7";
        public string Tdp { get; set; } = "45W";
        public string InstructionSet { get; set; } = "x86-64, MMX, SSE, SSE2, SSE3, SSSE3, SSE4.1, SSE4.2, AVX, AVX2, AVX-512";
        public string Virtualization { get; set; } = "VT-x, VT-d";
        public string L1Cache { get; set; } = "1.4 MB";
        public string L2Cache { get; set; } = "14 MB";
        public string L3Cache { get; set; } = "24 MB";
        public double Temperature { get; set; } = 45.2; // Celsius
        public double Usage { get; set; } = 25.5; // Percentage
        public double PowerConsumption { get; set; } = 35.2; // Watts
        
        public List<CpuCoreInfo> CoresInfo { get; set; } = new();
    }

    public class CpuCoreInfo
    {
        public int CoreNumber { get; set; }
        public double Usage { get; set; } = 20.0;
        public double ClockSpeed { get; set; } = 3.2; // GHz
        public double Temperature { get; set; } = 42.0; // Celsius
        public bool IsPerformanceCore { get; set; } = true;
        public string Threads { get; set; } = "0-1";
    }

    // Memory Information
    public class MemoryInfoModel
    {
        public ulong TotalPhysicalMemory { get; set; } = 32 * 1024 * 1024 * 1024; // 32 GB
        public ulong AvailablePhysicalMemory { get; set; } = 12 * 1024 * 1024 * 1024; // 12 GB
        public ulong UsedPhysicalMemory => TotalPhysicalMemory - AvailablePhysicalMemory;
        public double MemoryUsagePercentage => (double)UsedPhysicalMemory / TotalPhysicalMemory * 100;
        
        public ulong TotalVirtualMemory { get; set; } = 128 * 1024 * 1024 * 1024; // 128 GB
        public ulong AvailableVirtualMemory { get; set; } = 80 * 1024 * 1024 * 1024; // 80 GB
        public ulong UsedVirtualMemory => TotalVirtualMemory - AvailableVirtualMemory;
        
        public string MemoryType { get; set; } = "DDR5";
        public int MemorySpeed { get; set; } = 4800; // MHz
        public int MemoryChannels { get; set; } = 2;
        public int MemorySlots { get; set; } = 4;
        public int MemorySlotsUsed { get; set; } = 2;
        
        public List<MemoryModuleInfo> MemoryModules { get; set; } = new();
        public List<MemoryUsageHistory> UsageHistory { get; set; } = new();
    }

    public class MemoryModuleInfo
    {
        public string Slot { get; set; } = "Slot 1";
        public string Manufacturer { get; set; } = "Samsung";
        public string PartNumber { get; set; } = "M425R1GA3BB0-CQKOL";
        public string SerialNumber { get; set; } = "ABC12345678";
        public ulong Size { get; set; } = 16 * 1024 * 1024 * 1024; // 16 GB
        public string Type { get; set; } = "DDR5";
        public int Speed { get; set; } = 4800; // MHz
        public int Voltage { get; set; } = 1150; // mV
        public string Timings { get; set; } = "18-22-22-42";
    }

    public class MemoryUsageHistory
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public double UsagePercentage { get; set; } = 65.0;
        public ulong UsedMemory { get; set; } = 20 * 1024 * 1024 * 1024; // 20 GB
    }

    // Motherboard Information
    public class MotherboardInfoModel
    {
        public string Manufacturer { get; set; } = "Dell Inc.";
        public string Product { get; set; } = "XPS 15 9520";
        public string Version { get; set; } = "A00";
        public string SerialNumber { get; set; } = "ABC123XYZ";
        public string AssetTag { get; set; } = "DELL-2023";
        public string FormFactor { get; set; } = "Laptop";
        public string Chipset { get; set; } = "Intel Alder Lake";
        public string Southbridge { get; set; } = "Intel HM670";
        public string BiosVersion { get; set; } = "Dell Inc. 1.20.0";
        public DateTime BiosDate { get; set; } = new DateTime(2023, 1, 15);
        public string BiosSize { get; set; } = "16 MB";
        public string BiosType { get; set; } = "UEFI";
        public string BiosLanguage { get; set; } = "English";
        
        public List<MotherboardSensor> Sensors { get; set; } = new();
    }

    public class MotherboardSensor
    {
        public string Name { get; set; } = "System Temperature";
        public string Type { get; set; } = "Temperature";
        public double Value { get; set; } = 38.5; // Celsius
        public string Unit { get; set; } = "°C";
    }

    // GPU Information
    public class GpuInfoModel
    {
        public string Name { get; set; } = "NVIDIA GeForce RTX 3050 Ti";
        public string Manufacturer { get; set; } = "NVIDIA";
        public string Vendor { get; set; } = "NVIDIA";
        public string DeviceId { get; set; } = "0x2580";
        public string SubSystemId { get; set; } = "0x3F061028";
        public string Revision { get; set; } = "A1";
        public ulong Memory { get; set; } = 4 * 1024 * 1024 * 1024; // 4 GB
        public string MemoryType { get; set; } = "GDDR6";
        public int MemoryBusWidth { get; set; } = 128; // bits
        public double CoreClock { get; set; } = 1035; // MHz
        public double MemoryClock { get; set; } = 1500; // MHz
        public double Temperature { get; set; } = 45.0; // Celsius
        public double Usage { get; set; } = 15.8; // Percentage
        public double GpuUsage { get; set; } = 15.8; // Percentage
        public double MemoryUsage { get; set; } = 25.0; // Percentage
        public double FanSpeed { get; set; } = 30.0; // Percentage
        public double PowerConsumption { get; set; } = 45.0; // Watts
        public int CudaCores { get; set; } = 2560;
        public string DriverVersion { get; set; } = "536.99";
        public DateTime DriverDate { get; set; } = new DateTime(2023, 8, 15);
        public string DirectXVersion { get; set; } = "12 Ultimate";
        public string OpenGLVersion { get; set; } = "4.6";
        public string VulkanVersion { get; set; } = "1.3";
        
        public List<GpuSensor> Sensors { get; set; } = new();
    }

    public class GpuSensor
    {
        public string Name { get; set; } = "GPU Core";
        public string Type { get; set; } = "Temperature";
        public double Value { get; set; } = 45.0; // Celsius
        public string Unit { get; set; } = "°C";
    }

    // OS Information
    public class OsInfoModel
    {
        public string Name { get; set; } = "Windows 11 Pro";
        public string Version { get; set; } = "22H2";
        public string Build { get; set; } = "22621.2428";
        public string Edition { get; set; } = "Professional";
        public string ProductId { get; set; } = "00330-80000-00000-AAOEM";
        public string ProductKey { get; set; } = "XXXX-XXXX-XXXX-XXXX-XXXX";
        public string InstallationDate { get; set; } = "2023-01-01";
        public string InstallLanguage { get; set; } = "English (United States)";
        public string SystemLanguage { get; set; } = "English (United States)";
        public string TimeZone { get; set; } = "(UTC-05:00) Eastern Time (US & Canada)";
        public bool IsActivated { get; set; } = true;
        public string ActivationStatus { get; set; } = "Permanently activated";
        public string ActivationTechnology { get; set; } = "Digital License";
        public string LicenseChannel { get; set; } = "OEM";
        public string RegisteredOwner { get; set; } = "SystemUser";
        public string RegisteredOrganization { get; set; } = "MyCompany";
        public string WindowsDirectory { get; set; } = "C:\\Windows";
        public string SystemDirectory { get; set; } = "C:\\Windows\\System32";
        public string BootDevice { get; set; } = "\\Device\\HarddiskVolume1";
        
        public List<OsFeature> Features { get; set; } = new();
        public List<OsUpdate> Updates { get; set; } = new();
    }

    public class OsFeature
    {
        public string Name { get; set; } = "Virtual Machine Platform";
        public bool IsEnabled { get; set; } = true;
    }

    public class OsUpdate
    {
        public string KBId { get; set; } = "KB5029244";
        public string Title { get; set; } = "2023-09 Cumulative Update for Windows 11 Version 22H2";
        public DateTime InstallDate { get; set; } = new DateTime(2023, 9, 12);
        public string Type { get; set; } = "Quality Update";
        public string Size { get; set; } = "450 MB";
    }

    // Network Information
    public class NetworkAdapterInfo
    {
        public string Name { get; set; } = "Wi-Fi 6";
        public string Description { get; set; } = "Intel(R) Wi-Fi 6 AX210 160MHz";
        public string Manufacturer { get; set; } = "Intel";
        public string Type { get; set; } = "Wireless";
        public string MacAddress { get; set; } = "00-11-22-33-44-55";
        public string IpAddress { get; set; } = "192.168.1.100";
        public string Ipv6Address { get; set; } = "2001:db8:85a3::8a2e:370:7334";
        public string SubnetMask { get; set; } = "255.255.255.0";
        public string DefaultGateway { get; set; } = "192.168.1.1";
        public string DnsServers { get; set; } = "192.168.1.1, 8.8.8.8";
        public string DhcpServer { get; set; } = "192.168.1.1";
        public bool IsConnected { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public string ConnectionType { get; set; } = "Wi-Fi";
        public string Ssid { get; set; } = "MyWiFiNetwork";
        public string SignalStrength { get; set; } = "Excellent";
        public double Speed { get; set; } = 866.0; // Mbps
        public ulong BytesSent { get; set; } = 1024 * 1024 * 100; // 100 MB
        public ulong BytesReceived { get; set; } = 1024 * 1024 * 500; // 500 MB
        
        public List<NetworkStatistics> Statistics { get; set; } = new();
    }

    public class NetworkStatistics
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public ulong BytesSent { get; set; } = 1024 * 1024; // 1 MB
        public ulong BytesReceived { get; set; } = 1024 * 1024 * 5; // 5 MB
        public double UploadSpeed { get; set; } = 10.5; // Mbps
        public double DownloadSpeed { get; set; } = 50.2; // Mbps
    }

    // BIOS Information
    public class BiosInfoModel
    {
        public string Manufacturer { get; set; } = "Dell Inc.";
        public string Version { get; set; } = "1.20.0";
        public DateTime Date { get; set; } = new DateTime(2023, 1, 15);
        public string Size { get; set; } = "16 MB";
        public string Type { get; set; } = "UEFI";
        public string Language { get; set; } = "English";
        public string SerialNumber { get; set; } = "ABC123XYZ";
        public string AssetTag { get; set; } = "DELL-2023";
        public string ProductName { get; set; } = "XPS 15 9520";
        public string BoardManufacturer { get; set; } = "Dell Inc.";
        public string BoardProduct { get; set; } = "XPS 15 9520";
        public string BoardVersion { get; set; } = "A00";
        
        public List<BiosFeature> Features { get; set; } = new();
    }

    public class BiosFeature
    {
        public string Name { get; set; } = "Secure Boot";
        public bool IsEnabled { get; set; } = true;
        public bool IsSupported { get; set; } = true;
    }

    // Temperature Sensor
    public class TemperatureSensor
    {
        public string Name { get; set; } = "CPU Package";
        public string Type { get; set; } = "CPU";
        public double Value { get; set; } = 45.2; // Celsius
        public string Unit { get; set; } = "°C";
        public double Min { get; set; } = 20.0;
        public double Max { get; set; } = 100.0;
        public double Critical { get; set; } = 95.0;
        public bool IsCritical => Value >= Critical;
        public bool IsWarning => Value >= Max * 0.8;
    }

    // Fan Sensor
    public class FanSensor
    {
        public string Name { get; set; } = "CPU Fan";
        public string Type { get; set; } = "Fan";
        public double Value { get; set; } = 1200.0; // RPM
        public string Unit { get; set; } = "RPM";
        public double Percentage { get; set; } = 30.0; // Percentage
        public double Min { get; set; } = 500.0;
        public double Max { get; set; } = 4000.0;
    }

    // Voltage Sensor
    public class VoltageSensor
    {
        public string Name { get; set; } = "CPU Core";
        public string Type { get; set; } = "Voltage";
        public double Value { get; set; } = 1.25; // Volts
        public string Unit { get; set; } = "V";
        public double Min { get; set; } = 0.8;
        public double Max { get; set; } = 1.5;
    }

    // Performance Metrics
    public class PerformanceMetric
    {
        public string Name { get; set; } = "CPU Usage";
        public string Category { get; set; } = "Processor";
        public double Value { get; set; } = 25.5;
        public string Unit { get; set; } = "%";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

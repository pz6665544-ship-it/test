using System;
using System.Collections.Generic;
using SystemHub.Models;
using SystemHub.Services.Interfaces;

namespace SystemHub.Services
{
    public class SystemInfoService : ISystemInfoService
    {
        private readonly Random _random = new();

        public SystemOverviewModel GetSystemOverview()
        {
            return new SystemOverviewModel
            {
                ComputerName = "DESKTOP-PC",
                UserName = "SystemUser",
                OsName = "Windows 11 Pro",
                OsVersion = "22H2",
                OsBuild = "22621.2428",
                SystemType = "64-bit Operating System, x64-based processor",
                SystemManufacturer = "Dell Inc.",
                SystemModel = "XPS 15 9520",
                SystemSerialNumber = "ABC123XYZ",
                BiosVersion = "Dell Inc. 1.20.0, 2023-01-15",
                Uptime = TimeSpan.FromHours(48),
                LastBootTime = DateTime.Now.AddHours(-48),
                CpuUsage = Math.Round(_random.NextDouble() * 100, 1),
                MemoryUsage = Math.Round(_random.NextDouble() * 100, 1),
                DiskUsage = Math.Round(_random.NextDouble() * 100, 1),
                GpuUsage = Math.Round(_random.NextDouble() * 100, 1),
                PerformanceMetrics = GeneratePerformanceMetrics()
            };
        }

        public CpuInfoModel GetCpuInfo()
        {
            return new CpuInfoModel
            {
                Name = "Intel(R) Core(TM) i9-12900H",
                Manufacturer = "Intel",
                Architecture = "x64",
                Cores = 14,
                LogicalProcessors = 20,
                BaseClock = 2.5,
                MaxClock = 5.0,
                CurrentClock = 2.5 + (_random.NextDouble() * 2.5),
                Socket = "BGA 1744",
                Process = "Intel 7",
                Tdp = "45W",
                InstructionSet = "x86-64, MMX, SSE, SSE2, SSE3, SSSE3, SSE4.1, SSE4.2, AVX, AVX2, AVX-512",
                Virtualization = "VT-x, VT-d",
                L1Cache = "1.4 MB",
                L2Cache = "14 MB",
                L3Cache = "24 MB",
                Temperature = Math.Round(30 + (_random.NextDouble() * 40), 1),
                Usage = Math.Round(_random.NextDouble() * 100, 1),
                PowerConsumption = Math.Round(_random.NextDouble() * 100, 1),
                CoresInfo = GenerateCpuCores()
            };
        }

        public List<CpuCoreInfo> GetCpuCores()
        {
            return GenerateCpuCores();
        }

        public MemoryInfoModel GetMemoryInfo()
        {
            return new MemoryInfoModel
            {
                TotalPhysicalMemory = 32 * 1024 * 1024 * 1024,
                AvailablePhysicalMemory = (ulong)(32 * 1024 * 1024 * 1024 * (1 - _random.NextDouble() * 0.8)),
                MemoryType = "DDR5",
                MemorySpeed = 4800,
                MemoryChannels = 2,
                MemorySlots = 4,
                MemorySlotsUsed = 2,
                MemoryModules = GenerateMemoryModules(),
                UsageHistory = GenerateMemoryUsageHistory()
            };
        }

        public MotherboardInfoModel GetMotherboardInfo()
        {
            return new MotherboardInfoModel
            {
                Manufacturer = "Dell Inc.",
                Product = "XPS 15 9520",
                Version = "A00",
                SerialNumber = "ABC123XYZ",
                AssetTag = "DELL-2023",
                FormFactor = "Laptop",
                Chipset = "Intel Alder Lake",
                Southbridge = "Intel HM670",
                BiosVersion = "Dell Inc. 1.20.0",
                BiosDate = new DateTime(2023, 1, 15),
                BiosSize = "16 MB",
                BiosType = "UEFI",
                BiosLanguage = "English",
                Sensors = GenerateMotherboardSensors()
            };
        }

        public List<GpuInfoModel> GetGpuInfo()
        {
            return new List<GpuInfoModel>
            {
                new GpuInfoModel
                {
                    Name = "NVIDIA GeForce RTX 3050 Ti",
                    Manufacturer = "NVIDIA",
                    Vendor = "NVIDIA",
                    DeviceId = "0x2580",
                    SubSystemId = "0x3F061028",
                    Revision = "A1",
                    Memory = 4 * 1024 * 1024 * 1024,
                    MemoryType = "GDDR6",
                    MemoryBusWidth = 128,
                    CoreClock = 1035,
                    MemoryClock = 1500,
                    Temperature = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Usage = Math.Round(_random.NextDouble() * 100, 1),
                    GpuUsage = Math.Round(_random.NextDouble() * 100, 1),
                    MemoryUsage = Math.Round(_random.NextDouble() * 100, 1),
                    FanSpeed = Math.Round(_random.NextDouble() * 100, 1),
                    PowerConsumption = Math.Round(_random.NextDouble() * 100, 1),
                    CudaCores = 2560,
                    DriverVersion = "536.99",
                    DriverDate = new DateTime(2023, 8, 15),
                    DirectXVersion = "12 Ultimate",
                    OpenGLVersion = "4.6",
                    VulkanVersion = "1.3",
                    Sensors = GenerateGpuSensors()
                }
            };
        }

        public OsInfoModel GetOsInfo()
        {
            return new OsInfoModel
            {
                Name = "Windows 11 Pro",
                Version = "22H2",
                Build = "22621.2428",
                Edition = "Professional",
                ProductId = "00330-80000-00000-AAOEM",
                ProductKey = "XXXX-XXXX-XXXX-XXXX-XXXX",
                InstallationDate = "2023-01-01",
                InstallLanguage = "English (United States)",
                SystemLanguage = "English (United States)",
                TimeZone = "(UTC-05:00) Eastern Time (US & Canada)",
                IsActivated = true,
                ActivationStatus = "Permanently activated",
                ActivationTechnology = "Digital License",
                LicenseChannel = "OEM",
                RegisteredOwner = "SystemUser",
                RegisteredOrganization = "MyCompany",
                WindowsDirectory = "C:\\Windows",
                SystemDirectory = "C:\\Windows\\System32",
                BootDevice = "\\Device\\HarddiskVolume1",
                Features = GenerateOsFeatures(),
                Updates = GenerateOsUpdates()
            };
        }

        public List<NetworkAdapterInfo> GetNetworkAdapters()
        {
            return new List<NetworkAdapterInfo>
            {
                new NetworkAdapterInfo
                {
                    Name = "Wi-Fi 6",
                    Description = "Intel(R) Wi-Fi 6 AX210 160MHz",
                    Manufacturer = "Intel",
                    Type = "Wireless",
                    MacAddress = "00-11-22-33-44-55",
                    IpAddress = "192.168.1.100",
                    Ipv6Address = "2001:db8:85a3::8a2e:370:7334",
                    SubnetMask = "255.255.255.0",
                    DefaultGateway = "192.168.1.1",
                    DnsServers = "192.168.1.1, 8.8.8.8",
                    DhcpServer = "192.168.1.1",
                    IsConnected = true,
                    IsEnabled = true,
                    ConnectionType = "Wi-Fi",
                    Ssid = "MyWiFiNetwork",
                    SignalStrength = "Excellent",
                    Speed = Math.Round(500 + (_random.NextDouble() * 500), 1),
                    BytesSent = 1024 * 1024 * 100,
                    BytesReceived = 1024 * 1024 * 500,
                    Statistics = GenerateNetworkStatistics()
                },
                new NetworkAdapterInfo
                {
                    Name = "Ethernet",
                    Description = "Intel(R) Ethernet Connection I219-V",
                    Manufacturer = "Intel",
                    Type = "Ethernet",
                    MacAddress = "00-11-22-33-44-56",
                    IpAddress = string.Empty,
                    Ipv6Address = string.Empty,
                    SubnetMask = string.Empty,
                    DefaultGateway = string.Empty,
                    DnsServers = string.Empty,
                    DhcpServer = string.Empty,
                    IsConnected = false,
                    IsEnabled = true,
                    ConnectionType = "Ethernet",
                    Ssid = string.Empty,
                    SignalStrength = "N/A",
                    Speed = 0,
                    BytesSent = 0,
                    BytesReceived = 0,
                    Statistics = new List<NetworkStatistics>()
                }
            };
        }

        public BiosInfoModel GetBiosInfo()
        {
            return new BiosInfoModel
            {
                Manufacturer = "Dell Inc.",
                Version = "1.20.0",
                Date = new DateTime(2023, 1, 15),
                Size = "16 MB",
                Type = "UEFI",
                Language = "English",
                SerialNumber = "ABC123XYZ",
                AssetTag = "DELL-2023",
                ProductName = "XPS 15 9520",
                BoardManufacturer = "Dell Inc.",
                BoardProduct = "XPS 15 9520",
                BoardVersion = "A00",
                Features = GenerateBiosFeatures()
            };
        }

        public List<TemperatureSensor> GetTemperatureSensors()
        {
            return new List<TemperatureSensor>
            {
                new TemperatureSensor
                {
                    Name = "CPU Package",
                    Type = "CPU",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C",
                    Min = 20.0,
                    Max = 100.0,
                    Critical = 95.0
                },
                new TemperatureSensor
                {
                    Name = "CPU Core 1",
                    Type = "CPU",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C",
                    Min = 20.0,
                    Max = 100.0,
                    Critical = 95.0
                },
                new TemperatureSensor
                {
                    Name = "CPU Core 2",
                    Type = "CPU",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C",
                    Min = 20.0,
                    Max = 100.0,
                    Critical = 95.0
                },
                new TemperatureSensor
                {
                    Name = "GPU Core",
                    Type = "GPU",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C",
                    Min = 20.0,
                    Max = 100.0,
                    Critical = 95.0
                },
                new TemperatureSensor
                {
                    Name = "System",
                    Type = "Motherboard",
                    Value = Math.Round(30 + (_random.NextDouble() * 30), 1),
                    Unit = "°C",
                    Min = 20.0,
                    Max = 80.0,
                    Critical = 75.0
                }
            };
        }

        public List<FanSensor> GetFanSensors()
        {
            return new List<FanSensor>
            {
                new FanSensor
                {
                    Name = "CPU Fan",
                    Type = "Fan",
                    Value = Math.Round(1000 + (_random.NextDouble() * 3000), 0),
                    Unit = "RPM",
                    Percentage = Math.Round(_random.NextDouble() * 100, 1),
                    Min = 500.0,
                    Max = 4000.0
                },
                new FanSensor
                {
                    Name = "GPU Fan",
                    Type = "Fan",
                    Value = Math.Round(1000 + (_random.NextDouble() * 3000), 0),
                    Unit = "RPM",
                    Percentage = Math.Round(_random.NextDouble() * 100, 1),
                    Min = 500.0,
                    Max = 4000.0
                },
                new FanSensor
                {
                    Name = "System Fan",
                    Type = "Fan",
                    Value = Math.Round(1000 + (_random.NextDouble() * 3000), 0),
                    Unit = "RPM",
                    Percentage = Math.Round(_random.NextDouble() * 100, 1),
                    Min = 500.0,
                    Max = 4000.0
                }
            };
        }

        public List<VoltageSensor> GetVoltageSensors()
        {
            return new List<VoltageSensor>
            {
                new VoltageSensor
                {
                    Name = "CPU Core",
                    Type = "Voltage",
                    Value = Math.Round(0.8 + (_random.NextDouble() * 0.7), 2),
                    Unit = "V",
                    Min = 0.8,
                    Max = 1.5
                },
                new VoltageSensor
                {
                    Name = "CPU VCCIO",
                    Type = "Voltage",
                    Value = Math.Round(0.8 + (_random.NextDouble() * 0.7), 2),
                    Unit = "V",
                    Min = 0.8,
                    Max = 1.5
                },
                new VoltageSensor
                {
                    Name = "CPU VCCSA",
                    Type = "Voltage",
                    Value = Math.Round(0.8 + (_random.NextDouble() * 0.7), 2),
                    Unit = "V",
                    Min = 0.8,
                    Max = 1.5
                },
                new VoltageSensor
                {
                    Name = "DRAM",
                    Type = "Voltage",
                    Value = Math.Round(1.1 + (_random.NextDouble() * 0.4), 2),
                    Unit = "V",
                    Min = 0.8,
                    Max = 1.5
                }
            };
        }

        public TimeSpan GetSystemUptime()
        {
            return TimeSpan.FromHours(48);
        }

        public PerformanceMetrics GetPerformanceMetrics()
        {
            return new PerformanceMetrics
            {
                CpuUsage = Math.Round(_random.NextDouble() * 100, 1),
                MemoryUsage = Math.Round(_random.NextDouble() * 100, 1),
                DiskUsage = Math.Round(_random.NextDouble() * 100, 1),
                GpuUsage = Math.Round(_random.NextDouble() * 100, 1),
                NetworkUsage = Math.Round(_random.NextDouble() * 100, 1),
                Timestamp = DateTime.Now
            };
        }

        #region Helper Methods

        private List<PerformanceMetric> GeneratePerformanceMetrics()
        {
            var metrics = new List<PerformanceMetric>();
            var now = DateTime.Now;
            
            for (int i = 0; i < 10; i++)
            {
                metrics.Add(new PerformanceMetric
                {
                    Name = "CPU Usage",
                    Category = "Processor",
                    Value = Math.Round(20 + (_random.NextDouble() * 60), 1),
                    Unit = "%",
                    Timestamp = now.AddMinutes(-i * 5)
                });
                
                metrics.Add(new PerformanceMetric
                {
                    Name = "Memory Usage",
                    Category = "Memory",
                    Value = Math.Round(40 + (_random.NextDouble() * 40), 1),
                    Unit = "%",
                    Timestamp = now.AddMinutes(-i * 5)
                });
            }
            
            return metrics;
        }

        private List<CpuCoreInfo> GenerateCpuCores()
        {
            var cores = new List<CpuCoreInfo>();
            
            for (int i = 0; i < 14; i++)
            {
                cores.Add(new CpuCoreInfo
                {
                    CoreNumber = i,
                    Usage = Math.Round(_random.NextDouble() * 100, 1),
                    ClockSpeed = Math.Round(2.5 + (_random.NextDouble() * 2.5), 2),
                    Temperature = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    IsPerformanceCore = i < 6,
                    Threads = i < 6 ? $"{i * 2}-{i * 2 + 1}" : i.ToString()
                });
            }
            
            return cores;
        }

        private List<MemoryModuleInfo> GenerateMemoryModules()
        {
            return new List<MemoryModuleInfo>
            {
                new MemoryModuleInfo
                {
                    Slot = "Slot 1",
                    Manufacturer = "Samsung",
                    PartNumber = "M425R1GA3BB0-CQKOL",
                    SerialNumber = "ABC12345678",
                    Size = 16 * 1024 * 1024 * 1024,
                    Type = "DDR5",
                    Speed = 4800,
                    Voltage = 1150,
                    Timings = "18-22-22-42"
                },
                new MemoryModuleInfo
                {
                    Slot = "Slot 2",
                    Manufacturer = "Samsung",
                    PartNumber = "M425R1GA3BB0-CQKOL",
                    SerialNumber = "DEF98765432",
                    Size = 16 * 1024 * 1024 * 1024,
                    Type = "DDR5",
                    Speed = 4800,
                    Voltage = 1150,
                    Timings = "18-22-22-42"
                }
            };
        }

        private List<MemoryUsageHistory> GenerateMemoryUsageHistory()
        {
            var history = new List<MemoryUsageHistory>();
            var now = DateTime.Now;
            
            for (int i = 0; i < 20; i++)
            {
                history.Add(new MemoryUsageHistory
                {
                    Timestamp = now.AddMinutes(-i * 2),
                    UsagePercentage = Math.Round(40 + (_random.NextDouble() * 40), 1),
                    UsedMemory = (ulong)(20 * 1024 * 1024 * 1024 * _random.NextDouble())
                });
            }
            
            return history;
        }

        private List<MotherboardSensor> GenerateMotherboardSensors()
        {
            return new List<MotherboardSensor>
            {
                new MotherboardSensor
                {
                    Name = "System Temperature",
                    Type = "Temperature",
                    Value = Math.Round(30 + (_random.NextDouble() * 30), 1),
                    Unit = "°C"
                },
                new MotherboardSensor
                {
                    Name = "EC Temperature",
                    Type = "Temperature",
                    Value = Math.Round(30 + (_random.NextDouble() * 30), 1),
                    Unit = "°C"
                }
            };
        }

        private List<GpuSensor> GenerateGpuSensors()
        {
            return new List<GpuSensor>
            {
                new GpuSensor
                {
                    Name = "GPU Core",
                    Type = "Temperature",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C"
                },
                new GpuSensor
                {
                    Name = "GPU Memory",
                    Type = "Temperature",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C"
                },
                new GpuSensor
                {
                    Name = "GPU Hot Spot",
                    Type = "Temperature",
                    Value = Math.Round(30 + (_random.NextDouble() * 40), 1),
                    Unit = "°C"
                }
            };
        }

        private List<OsFeature> GenerateOsFeatures()
        {
            return new List<OsFeature>
            {
                new OsFeature { Name = "Virtual Machine Platform", IsEnabled = true },
                new OsFeature { Name = "Windows Subsystem for Linux", IsEnabled = true },
                new OsFeature { Name = "Windows Sandbox", IsEnabled = false },
                new OsFeature { Name = "Containers", IsEnabled = true },
                new OsFeature { Name = "Hyper-V", IsEnabled = false }
            };
        }

        private List<OsUpdate> GenerateOsUpdates()
        {
            return new List<OsUpdate>
            {
                new OsUpdate
                {
                    KBId = "KB5029244",
                    Title = "2023-09 Cumulative Update for Windows 11 Version 22H2",
                    InstallDate = new DateTime(2023, 9, 12),
                    Type = "Quality Update",
                    Size = "450 MB"
                },
                new OsUpdate
                {
                    KBId = "KB5028166",
                    Title = "2023-08 Cumulative Update for Windows 11 Version 22H2",
                    InstallDate = new DateTime(2023, 8, 8),
                    Type = "Quality Update",
                    Size = "420 MB"
                },
                new OsUpdate
                {
                    KBId = "KB5027231",
                    Title = "2023-07 Cumulative Update for Windows 11 Version 22H2",
                    InstallDate = new DateTime(2023, 7, 11),
                    Type = "Quality Update",
                    Size = "410 MB"
                }
            };
        }

        private List<NetworkStatistics> GenerateNetworkStatistics()
        {
            var stats = new List<NetworkStatistics>();
            var now = DateTime.Now;
            
            for (int i = 0; i < 10; i++)
            {
                stats.Add(new NetworkStatistics
                {
                    Timestamp = now.AddMinutes(-i),
                    BytesSent = (ulong)(1024 * 1024 * _random.NextDouble() * 10),
                    BytesReceived = (ulong)(1024 * 1024 * _random.NextDouble() * 50),
                    UploadSpeed = Math.Round(_random.NextDouble() * 50, 1),
                    DownloadSpeed = Math.Round(_random.NextDouble() * 200, 1)
                });
            }
            
            return stats;
        }

        private List<BiosFeature> GenerateBiosFeatures()
        {
            return new List<BiosFeature>
            {
                new BiosFeature { Name = "Secure Boot", IsEnabled = true, IsSupported = true },
                new BiosFeature { Name = "Virtualization Technology", IsEnabled = true, IsSupported = true },
                new BiosFeature { Name = "Intel VT-d", IsEnabled = true, IsSupported = true },
                new BiosFeature { Name = "Fast Boot", IsEnabled = true, IsSupported = true },
                new BiosFeature { Name = "USB Boot", IsEnabled = true, IsSupported = true }
            };
        }

        #endregion
    }

    public class PerformanceMetrics
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public double GpuUsage { get; set; }
        public double NetworkUsage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

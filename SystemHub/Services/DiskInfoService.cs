using System;
using System.Collections.Generic;
using SystemHub.Models;
using SystemHub.Services.Interfaces;

namespace SystemHub.Services
{
    public class DiskInfoService : IDiskInfoService
    {
        private readonly Random _random = new();

        public List<PhysicalDiskInfo> GetPhysicalDisks()
        {
            return new List<PhysicalDiskInfo>
            {
                new PhysicalDiskInfo
                {
                    DiskNumber = 0,
                    DiskId = "\\\\.\\PHYSICALDRIVE0",
                    Model = "Samsung SSD 980 PRO 1TB",
                    Manufacturer = "Samsung",
                    SerialNumber = "S5ABC123456789",
                    Firmware = "3B2QGXA7",
                    Size = 1000 * 1024 * 1024 * 1024,
                    InterfaceType = "NVMe",
                    BusType = "PCIe 4.0 x4",
                    MediaType = "SSD",
                    PartitionStyle = "GPT",
                    IsBootDisk = true,
                    IsSystemDisk = true,
                    IsRemovable = false,
                    IsOnline = true,
                    HealthStatus = "Healthy",
                    OperationalStatus = "Online",
                    Temperature = Math.Round(30 + (_random.NextDouble() * 20), 1),
                    BytesPerSector = 512,
                    TotalSectors = 1953125000,
                    RotationRate = "N/A (SSD)",
                    FormFactor = "M.2",
                    Partitions = GeneratePartitions(),
                    SmartData = GenerateSmartData()
                },
                new PhysicalDiskInfo
                {
                    DiskNumber = 1,
                    DiskId = "\\\\.\\PHYSICALDRIVE1",
                    Model = "Seagate Barracuda 2TB",
                    Manufacturer = "Seagate",
                    SerialNumber = "ZFL12345678",
                    Firmware = "CC45",
                    Size = 2000 * 1024 * 1024 * 1024,
                    InterfaceType = "SATA",
                    BusType = "SATA 6Gb/s",
                    MediaType = "HDD",
                    PartitionStyle = "GPT",
                    IsBootDisk = false,
                    IsSystemDisk = false,
                    IsRemovable = false,
                    IsOnline = true,
                    HealthStatus = "Warning",
                    OperationalStatus = "Online",
                    Temperature = Math.Round(35 + (_random.NextDouble() * 15), 1),
                    BytesPerSector = 4096,
                    TotalSectors = 3907029168,
                    RotationRate = "7200 RPM",
                    FormFactor = "3.5"",
                    Partitions = GeneratePartitions(1),
                    SmartData = GenerateSmartData(1)
                },
                new PhysicalDiskInfo
                {
                    DiskNumber = 2,
                    DiskId = "\\\\.\\PHYSICALDRIVE2",
                    Model = "SanDisk Ultra 128GB",
                    Manufacturer = "SanDisk",
                    SerialNumber = "SD123456789",
                    Firmware = "1.0.0",
                    Size = 128 * 1024 * 1024 * 1024,
                    InterfaceType = "USB",
                    BusType = "USB 3.2",
                    MediaType = "SSD",
                    PartitionStyle = "MBR",
                    IsBootDisk = false,
                    IsSystemDisk = false,
                    IsRemovable = true,
                    IsOnline = true,
                    HealthStatus = "Healthy",
                    OperationalStatus = "Online",
                    Temperature = 0,
                    BytesPerSector = 512,
                    TotalSectors = 250069680,
                    RotationRate = "N/A (SSD)",
                    FormFactor = "2.5"",
                    Partitions = GeneratePartitions(2),
                    SmartData = GenerateSmartData(2)
                }
            };
        }

        public List<LogicalDriveInfo> GetLogicalDrives()
        {
            return new List<LogicalDriveInfo>
            {
                new LogicalDriveInfo
                {
                    DriveLetter = "C",
                    DriveType = "Fixed",
                    FileSystem = "NTFS",
                    Label = "OS",
                    TotalSize = 500 * 1024 * 1024 * 1024,
                    TotalFreeSpace = (ulong)(500 * 1024 * 1024 * 1024 * (1 - 0.45)),
                    IsReady = true,
                    IsNetworkDrive = false,
                    IsRemovable = false,
                    IsCDRom = false,
                    VolumeName = "OS",
                    VolumeSerialNumber = "ABCD-1234",
                    Format = "NTFS",
                    IsCompressed = false,
                    IsEncrypted = false,
                    IsBitLockerEncrypted = false,
                    FileSystemInfo = GenerateFileSystemInfo("C", "NTFS")
                },
                new LogicalDriveInfo
                {
                    DriveLetter = "D",
                    DriveType = "Fixed",
                    FileSystem = "NTFS",
                    Label = "Data",
                    TotalSize = 500 * 1024 * 1024 * 1024,
                    TotalFreeSpace = (ulong)(500 * 1024 * 1024 * 1024 * (1 - 0.65)),
                    IsReady = true,
                    IsNetworkDrive = false,
                    IsRemovable = false,
                    IsCDRom = false,
                    VolumeName = "Data",
                    VolumeSerialNumber = "EFGH-5678",
                    Format = "NTFS",
                    IsCompressed = false,
                    IsEncrypted = false,
                    IsBitLockerEncrypted = false,
                    FileSystemInfo = GenerateFileSystemInfo("D", "NTFS")
                },
                new LogicalDriveInfo
                {
                    DriveLetter = "E",
                    DriveType = "Fixed",
                    FileSystem = "NTFS",
                    Label = "Storage",
                    TotalSize = 2000 * 1024 * 1024 * 1024,
                    TotalFreeSpace = (ulong)(2000 * 1024 * 1024 * 1024 * (1 - 0.75)),
                    IsReady = true,
                    IsNetworkDrive = false,
                    IsRemovable = false,
                    IsCDRom = false,
                    VolumeName = "Storage",
                    VolumeSerialNumber = "IJKL-9012",
                    Format = "NTFS",
                    IsCompressed = false,
                    IsEncrypted = false,
                    IsBitLockerEncrypted = false,
                    FileSystemInfo = GenerateFileSystemInfo("E", "NTFS")
                },
                new LogicalDriveInfo
                {
                    DriveLetter = "F",
                    DriveType = "Removable",
                    FileSystem = "exFAT",
                    Label = "USB Drive",
                    TotalSize = 128 * 1024 * 1024 * 1024,
                    TotalFreeSpace = (ulong)(128 * 1024 * 1024 * 1024 * (1 - 0.30)),
                    IsReady = true,
                    IsNetworkDrive = false,
                    IsRemovable = true,
                    IsCDRom = false,
                    VolumeName = "USB Drive",
                    VolumeSerialNumber = "MNOP-3456",
                    Format = "exFAT",
                    IsCompressed = false,
                    IsEncrypted = false,
                    IsBitLockerEncrypted = false,
                    FileSystemInfo = GenerateFileSystemInfo("F", "exFAT")
                }
            };
        }

        public List<DiskHealthInfo> GetDiskHealth()
        {
            return new List<DiskHealthInfo>
            {
                new DiskHealthInfo
                {
                    DiskId = "\\\\.\\PHYSICALDRIVE0",
                    DiskName = "Samsung SSD 980 PRO 1TB",
                    HealthStatus = "Good",
                    HealthPercentage = 95.0,
                    HealthDescription = "The disk is in good health.",
                    TotalPowerOnHours = 5000,
                    PowerCycleCount = 500,
                    TotalBytesRead = 1024 * 1024 * 1024 * 1024 * 5,
                    TotalBytesWritten = 1024 * 1024 * 1024 * 1024 * 3,
                    TotalBytesReadFromCache = 1024 * 1024 * 1024 * 100,
                    TotalBytesWrittenToCache = 1024 * 1024 * 1024 * 50,
                    Temperature = 35,
                    AvailableSpare = 100,
                    AvailableSpareThreshold = 10,
                    PercentageUsed = 25,
                    PredictedFailureDate = null,
                    RemainingLifetimePercentage = 95,
                    SmartAttributes = GenerateSmartAttributes(),
                    Warnings = new List<DiskHealthWarning>()
                },
                new DiskHealthInfo
                {
                    DiskId = "\\\\.\\PHYSICALDRIVE1",
                    DiskName = "Seagate Barracuda 2TB",
                    HealthStatus = "Warning",
                    HealthPercentage = 75.0,
                    HealthDescription = "The disk shows signs of wear and has some bad sectors.",
                    TotalPowerOnHours = 45000,
                    PowerCycleCount = 4500,
                    TotalBytesRead = 1024 * 1024 * 1024 * 1024 * 50,
                    TotalBytesWritten = 1024 * 1024 * 1024 * 1024 * 30,
                    TotalBytesReadFromCache = 1024 * 1024 * 1024 * 1000,
                    TotalBytesWrittenToCache = 1024 * 1024 * 1024 * 500,
                    Temperature = 42,
                    AvailableSpare = 75,
                    AvailableSpareThreshold = 10,
                    PercentageUsed = 70,
                    PredictedFailureDate = DateTime.Now.AddMonths(6),
                    RemainingLifetimePercentage = 75,
                    SmartAttributes = GenerateSmartAttributes(1),
                    Warnings = GenerateDiskWarnings()
                }
            };
        }

        public List<DiskUsageInfo> GetDiskUsage()
        {
            return new List<DiskUsageInfo>
            {
                new DiskUsageInfo
                {
                    DriveLetter = "C",
                    FileSystem = "NTFS",
                    TotalSize = 500 * 1024 * 1024 * 1024,
                    UsedSpace = (ulong)(500 * 1024 * 1024 * 1024 * 0.45),
                    FreeSpace = (ulong)(500 * 1024 * 1024 * 1024 * 0.55),
                    FileTypeUsage = GenerateFileTypeUsage("C"),
                    FolderUsage = GenerateFolderUsage("C"),
                    UsageHistory = GenerateUsageHistory("C")
                },
                new DiskUsageInfo
                {
                    DriveLetter = "D",
                    FileSystem = "NTFS",
                    TotalSize = 500 * 1024 * 1024 * 1024,
                    UsedSpace = (ulong)(500 * 1024 * 1024 * 1024 * 0.65),
                    FreeSpace = (ulong)(500 * 1024 * 1024 * 1024 * 0.35),
                    FileTypeUsage = GenerateFileTypeUsage("D"),
                    FolderUsage = GenerateFolderUsage("D"),
                    UsageHistory = GenerateUsageHistory("D")
                },
                new DiskUsageInfo
                {
                    DriveLetter = "E",
                    FileSystem = "NTFS",
                    TotalSize = 2000 * 1024 * 1024 * 1024,
                    UsedSpace = (ulong)(2000 * 1024 * 1024 * 1024 * 0.75),
                    FreeSpace = (ulong)(2000 * 1024 * 1024 * 1024 * 0.25),
                    FileTypeUsage = GenerateFileTypeUsage("E"),
                    FolderUsage = GenerateFolderUsage("E"),
                    UsageHistory = GenerateUsageHistory("E")
                }
            };
        }

        public List<PartitionInfo> GetPartitions()
        {
            var partitions = new List<PartitionInfo>();
            
            // Partitions for Disk 0 (Samsung SSD)
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 1,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "Recovery",
                FileSystem = "NTFS",
                Size = 500 * 1024 * 1024, // 500 MB
                Offset = 1024 * 1024, // 1 MB
                IsBootable = false,
                IsActive = false,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = string.Empty,
                Label = "Recovery",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 2,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "System",
                FileSystem = "FAT32",
                Size = 100 * 1024 * 1024, // 100 MB
                Offset = 501 * 1024 * 1024, // 501 MB
                IsBootable = true,
                IsActive = true,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = string.Empty,
                Label = "EFI",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 3,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "Primary",
                FileSystem = "NTFS",
                Size = 500 * 1024 * 1024 * 1024, // 500 GB
                Offset = 601 * 1024 * 1024, // 601 MB
                IsBootable = false,
                IsActive = false,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = "C",
                Label = "OS",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 4,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "Primary",
                FileSystem = "NTFS",
                Size = 500 * 1024 * 1024 * 1024, // 500 GB
                Offset = 500601 * 1024 * 1024, // 500.6 GB
                IsBootable = false,
                IsActive = false,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = "D",
                Label = "Data",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            // Partitions for Disk 1 (Seagate HDD)
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 1,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "Primary",
                FileSystem = "NTFS",
                Size = 2000 * 1024 * 1024 * 1024, // 2 TB
                Offset = 1024 * 1024, // 1 MB
                IsBootable = false,
                IsActive = false,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = "E",
                Label = "Storage",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            // Partitions for Disk 2 (SanDisk USB)
            partitions.Add(new PartitionInfo
            {
                PartitionNumber = 1,
                PartitionId = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}",
                Type = "Primary",
                FileSystem = "exFAT",
                Size = 128 * 1024 * 1024 * 1024, // 128 GB
                Offset = 0,
                IsBootable = false,
                IsActive = false,
                IsPrimary = true,
                IsLogical = false,
                DriveLetter = "F",
                Label = "USB Drive",
                VolumeName = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\"
            });
            
            return partitions;
        }

        public List<SmartData> GetSmartData(string diskId)
        {
            if (diskId == "\\\\.\\PHYSICALDRIVE0")
                return GenerateSmartData();
            else if (diskId == "\\\\.\\PHYSICALDRIVE1")
                return GenerateSmartData(1);
            else
                return GenerateSmartData(2);
        }

        public List<FileSystemInfo> GetFileSystems()
        {
            return new List<FileSystemInfo>
            {
                GenerateFileSystemInfo("C", "NTFS"),
                GenerateFileSystemInfo("D", "NTFS"),
                GenerateFileSystemInfo("E", "NTFS"),
                GenerateFileSystemInfo("F", "exFAT")
            };
        }

        public BenchmarkResult RunDiskBenchmark(string diskId, BenchmarkType type)
        {
            return new BenchmarkResult
            {
                DiskId = diskId,
                DiskName = GetDiskName(diskId),
                BenchmarkType = type,
                Timestamp = DateTime.Now,
                Score = Math.Round(2000 + (_random.NextDouble() * 2000), 0),
                ReadSpeed = Math.Round(3000 + (_random.NextDouble() * 4000), 0),
                WriteSpeed = Math.Round(2000 + (_random.NextDouble() * 3000), 0),
                IoPs = Math.Round(500000 + (_random.NextDouble() * 500000), 0),
                AccessTime = Math.Round(0.05 + (_random.NextDouble() * 0.15), 2),
                Latency = Math.Round(0.01 + (_random.NextDouble() * 0.09), 2),
                CpuUsage = Math.Round(_random.NextDouble() * 30, 1),
                DataPoints = GenerateBenchmarkDataPoints(type)
            };
        }

        public DiskCleanupResult RunDiskCleanup(string driveLetter)
        {
            return new DiskCleanupResult
            {
                DriveLetter = driveLetter,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                TotalSpaceFreed = (ulong)(5 * 1024 * 1024 * 1024),
                FilesDeleted = 15000,
                FoldersDeleted = 500,
                Duration = TimeSpan.FromSeconds(30),
                Categories = GenerateCleanupCategories()
            };
        }

        public DefragResult RunDefragmentation(string driveLetter)
        {
            return new DefragResult
            {
                DriveLetter = driveLetter,
                Timestamp = DateTime.Now,
                IsSuccessful = true,
                InitialFragmentation = Math.Round(40 + (_random.NextDouble() * 30), 1),
                FinalFragmentation = Math.Round(_random.NextDouble() * 5, 1),
                TotalSpaceDefragged = (ulong)(200 * 1024 * 1024 * 1024),
                FilesProcessed = 50000,
                Duration = TimeSpan.FromMinutes(15),
                Type = DefragType.Full
            };
        }

        public List<LargeFileInfo> GetLargestFiles(string path, int topN = 100)
        {
            var files = new List<LargeFileInfo>();
            var extensions = new[] { ".iso", ".zip", ".exe", ".dll", ".dat", ".mp4", ".mk4", ".avi", ".mov" };
            var directories = new[] { "Program Files", "Games", "Downloads", "Videos", "Temp", "Backup" };
            
            for (int i = 0; i < Math.Min(topN, 50); i++)
            {
                files.Add(new LargeFileInfo
                {
                    FilePath = $@"{path}\{directories[_random.Next(directories.Length)]}\{Guid.NewGuid()}{extensions[_random.Next(extensions.Length)]}",
                    FileName = $@"file_{i}{extensions[_random.Next(extensions.Length)]}",
                    Directory = path,
                    FileSize = (ulong)((1 + (_random.NextDouble() * 10)) * 1024 * 1024 * 1024),
                    LastModified = DateTime.Now.AddDays(-_random.Next(365)),
                    Created = DateTime.Now.AddDays(-_random.Next(730)),
                    LastAccessed = DateTime.Now.AddDays(-_random.Next(30)),
                    FileType = extensions[_random.Next(extensions.Length)].TrimStart('.'),
                    Attributes = "Archive"
                });
            }
            
            return files;
        }

        public List<FolderSizeInfo> GetFolderSizes(string path, int depth = 3)
        {
            var folders = new List<FolderSizeInfo>();
            var rootFolders = new[] { "Program Files", "Windows", "Users", "ProgramData", "Temp" };
            
            for (int i = 0; i < rootFolders.Length; i++)
            {
                var folder = new FolderSizeInfo
                {
                    FolderPath = $@"{path}\{rootFolders[i]}",
                    FolderName = rootFolders[i],
                    FolderSize = (ulong)((10 + (_random.NextDouble() * 100)) * 1024 * 1024 * 1024),
                    FileCount = _random.Next(10000, 100000),
                    FolderCount = _random.Next(100, 1000),
                    Depth = 0,
                    SubFolders = GenerateSubFolders($@"{path}\{rootFolders[i]}", depth - 1)
                };
                folders.Add(folder);
            }
            
            return folders;
        }

        #region Helper Methods

        private string GetDiskName(string diskId)
        {
            switch (diskId)
            {
                case "\\\\.\\PHYSICALDRIVE0": return "Samsung SSD 980 PRO 1TB";
                case "\\\\.\\PHYSICALDRIVE1": return "Seagate Barracuda 2TB";
                case "\\\\.\\PHYSICALDRIVE2": return "SanDisk Ultra 128GB";
                default: return "Unknown Disk";
            }
        }

        private List<PartitionInfo> GeneratePartitions(int diskIndex = 0)
        {
            var partitions = new List<PartitionInfo>();
            
            if (diskIndex == 0)
            {
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 1,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "Recovery",
                    FileSystem = "NTFS",
                    Size = 500 * 1024 * 1024,
                    Offset = 1024 * 1024,
                    IsBootable = false,
                    IsActive = false,
                    IsPrimary = true,
                    DriveLetter = string.Empty,
                    Label = "Recovery"
                });
                
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 2,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "System",
                    FileSystem = "FAT32",
                    Size = 100 * 1024 * 1024,
                    Offset = 501 * 1024 * 1024,
                    IsBootable = true,
                    IsActive = true,
                    IsPrimary = true,
                    DriveLetter = string.Empty,
                    Label = "EFI"
                });
                
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 3,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "Primary",
                    FileSystem = "NTFS",
                    Size = 500 * 1024 * 1024 * 1024,
                    Offset = 601 * 1024 * 1024,
                    IsBootable = false,
                    IsActive = false,
                    IsPrimary = true,
                    DriveLetter = "C",
                    Label = "OS"
                });
                
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 4,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "Primary",
                    FileSystem = "NTFS",
                    Size = 500 * 1024 * 1024 * 1024,
                    Offset = 500601 * 1024 * 1024,
                    IsBootable = false,
                    IsActive = false,
                    IsPrimary = true,
                    DriveLetter = "D",
                    Label = "Data"
                });
            }
            else if (diskIndex == 1)
            {
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 1,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "Primary",
                    FileSystem = "NTFS",
                    Size = 2000 * 1024 * 1024 * 1024,
                    Offset = 1024 * 1024,
                    IsBootable = false,
                    IsActive = false,
                    IsPrimary = true,
                    DriveLetter = "E",
                    Label = "Storage"
                });
            }
            else
            {
                partitions.Add(new PartitionInfo
                {
                    PartitionNumber = 1,
                    PartitionId = Guid.NewGuid().ToString(),
                    Type = "Primary",
                    FileSystem = "exFAT",
                    Size = 128 * 1024 * 1024 * 1024,
                    Offset = 0,
                    IsBootable = false,
                    IsActive = false,
                    IsPrimary = true,
                    DriveLetter = "F",
                    Label = "USB Drive"
                });
            }
            
            return partitions;
        }

        private List<SmartData> GenerateSmartData(int diskIndex = 0)
        {
            var smartData = new List<SmartData>();
            var attributeNames = new[]
            {
                "Raw Read Error Rate", "Spin Up Time", "Start/Stop Count", "Reallocated Sectors Count",
                "Seek Error Rate", "Seek Time Performance", "Power-On Hours", "Spin Retry Count",
                "Power Cycle Count", "G-Sense Error Rate", "Current Pending Sector Count", "Uncorrectable Error Count",
                "UltraDMA CRC Error Count", "Write Error Rate", "Media Wearout Indicator", "Read Error Retry Rate",
                "Total Host Writes", "Total NAND Writes", "Available Reserved Space", "Temperature"
            };
            var attributeFlags = new[] { "Pre-failure", "Old age", "Pre-failure", "Pre-failure" };
            
            for (int i = 0; i < 20; i++)
            {
                smartData.Add(new SmartData
                {
                    AttributeId = i + 1,
                    AttributeName = attributeNames[i % attributeNames.Length],
                    AttributeFlag = attributeFlags[i % attributeFlags.Length],
                    Value = (byte)(diskIndex == 1 && i % 5 == 0 ? 50 : 100),
                    Worst = (byte)(diskIndex == 1 && i % 5 == 0 ? 45 : 100),
                    Threshold = (byte)(i % 3 == 0 ? 50 : 10),
                    RawValue = (ulong)(_random.Next(1000)),
                    Status = diskIndex == 1 && i % 5 == 0 ? "Warning" : "Good"
                });
            }
            
            return smartData;
        }

        private List<SmartAttribute> GenerateSmartAttributes(int diskIndex = 0)
        {
            var attributes = new List<SmartAttribute>();
            var categories = new[] { "Error Rate", "Spin Up", "Reallocated", "Seek", "Power", "Temperature" };
            
            for (int i = 0; i < 10; i++)
            {
                attributes.Add(new SmartAttribute
                {
                    Id = i + 1,
                    Name = $"Attribute {i + 1}",
                    Category = categories[i % categories.Length],
                    Value = (byte)(diskIndex == 1 && i % 3 == 0 ? 50 : 100),
                    Worst = (byte)(diskIndex == 1 && i % 3 == 0 ? 45 : 100),
                    Threshold = (byte)(i % 2 == 0 ? 50 : 10),
                    RawValue = (ulong)(_random.Next(1000)),
                    Status = diskIndex == 1 && i % 3 == 0 ? "Warning" : "Good"
                });
            }
            
            return attributes;
        }

        private List<DiskHealthWarning> GenerateDiskWarnings()
        {
            return new List<DiskHealthWarning>
            {
                new DiskHealthWarning
                {
                    Type = "Warning",
                    Message = "High number of reallocated sectors",
                    Description = "The disk has 50 reallocated sectors, which may indicate potential failure.",
                    Timestamp = DateTime.Now.AddHours(-1),
                    Severity = "High"
                },
                new DiskHealthWarning
                {
                    Type = "Warning",
                    Message = "High temperature",
                    Description = "The disk temperature has exceeded 40°C multiple times.",
                    Timestamp = DateTime.Now.AddHours(-2),
                    Severity = "Medium"
                },
                new DiskHealthWarning
                {
                    Type = "Warning",
                    Message = "High power-on hours",
                    Description = "The disk has been powered on for over 45,000 hours.",
                    Timestamp = DateTime.Now.AddHours(-3),
                    Severity = "Low"
                }
            };
        }

        private FileSystemInfo GenerateFileSystemInfo(string driveLetter, string fileSystem)
        {
            return new FileSystemInfo
            {
                FileSystem = fileSystem,
                DriveLetter = driveLetter,
                TotalClusters = fileSystem == "NTFS" ? 244190592 : 31250000,
                FreeClusters = fileSystem == "NTFS" ? 134400000 : 21875000,
                TotalBytes = fileSystem == "NTFS" ? 1000 * 1024 * 1024 * 1024 : 128 * 1024 * 1024 * 1024,
                FreeBytes = fileSystem == "NTFS" ? (ulong)(550 * 1024 * 1024 * 1024) : (ulong)(90 * 1024 * 1024 * 1024),
                BytesPerCluster = fileSystem == "NTFS" ? 4096 : 32768,
                VolumeName = $@"{driveLetter}:",
                VolumeSerialNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                IsCompressed = false,
                IsEncrypted = false,
                SupportsHardLinks = fileSystem == "NTFS",
                SupportsSymbolicLinks = fileSystem == "NTFS",
                SupportsReparsePoints = fileSystem == "NTFS",
                SupportsSparseFiles = fileSystem == "NTFS",
                SupportsTransactions = fileSystem == "NTFS",
                SupportsUsnJournal = fileSystem == "NTFS",
                SupportsQuotas = fileSystem == "NTFS"
            };
        }

        private List<FileTypeUsage> GenerateFileTypeUsage(string driveLetter)
        {
            var fileTypes = new List<FileTypeUsage>();
            var types = new[]
            {
                new { Ext = ".exe", Category = "Executable", Count = 1500, Size = 5.0 },
                new { Ext = ".dll", Category = "Library", Count = 5000, Size = 8.0 },
                new { Ext = ".iso", Category = "Archive", Count = 50, Size = 20.0 },
                new { Ext = ".zip", Category = "Archive", Count = 200, Size = 10.0 },
                new { Ext = ".mp4", Category = "Video", Count = 100, Size = 15.0 },
                new { Ext = ".mp3", Category = "Audio", Count = 500, Size = 3.0 },
                new { Ext = ".jpg", Category = "Image", Count = 2000, Size = 2.0 },
                new { Ext = ".png", Category = "Image", Count = 1500, Size = 1.5 },
                new { Ext = ".docx", Category = "Document", Count = 300, Size = 1.0 },
                new { Ext = ".pdf", Category = "Document", Count = 200, Size = 2.0 }
            };
            
            foreach (var type in types)
            {
                fileTypes.Add(new FileTypeUsage
                {
                    FileType = type.Ext,
                    Category = type.Category,
                    FileCount = type.Count,
                    TotalSize = (ulong)(type.Size * 1024 * 1024 * 1024),
                    PercentageOfTotal = Math.Round(type.Size / (driveLetter == "C" ? 500.0 : driveLetter == "D" ? 500.0 : 2000.0) * 100, 1)
                });
            }
            
            return fileTypes;
        }

        private List<FolderUsage> GenerateFolderUsage(string driveLetter)
        {
            var folders = new List<FolderUsage>();
            var folderNames = new[] { "Program Files", "Windows", "Users", "ProgramData", "Temp", "Downloads" };
            var sizes = new[] { 100.0, 50.0, 80.0, 20.0, 10.0, 30.0 };
            var fileCounts = new[] { 10000, 50000, 20000, 5000, 5000, 8000 };
            var folderCounts = new[] { 500, 1000, 200, 100, 50, 200 };
            
            for (int i = 0; i < folderNames.Length; i++)
            {
                folders.Add(new FolderUsage
                {
                    FolderPath = $@"{driveLetter}:\{folderNames[i]}",
                    FolderName = folderNames[i],
                    FileCount = fileCounts[i],
                    FolderCount = folderCounts[i],
                    TotalSize = (ulong)(sizes[i] * 1024 * 1024 * 1024),
                    PercentageOfTotal = Math.Round(sizes[i] / (driveLetter == "C" ? 500.0 : driveLetter == "D" ? 500.0 : 2000.0) * 100, 1)
                });
            }
            
            return folders;
        }

        private List<DiskUsageHistory> GenerateUsageHistory(string driveLetter)
        {
            var history = new List<DiskUsageHistory>();
            var now = DateTime.Now;
            var baseUsage = driveLetter == "C" ? 0.45 : driveLetter == "D" ? 0.65 : 0.75;
            
            for (int i = 0; i < 30; i++)
            {
                var usageVariation = (_random.NextDouble() - 0.5) * 0.1;
                var usage = Math.Max(0.1, Math.Min(0.95, baseUsage + usageVariation));
                
                history.Add(new DiskUsageHistory
                {
                    Timestamp = now.AddDays(-i),
                    UsedSpace = (ulong)((driveLetter == "C" ? 500.0 : driveLetter == "D" ? 500.0 : 2000.0) * 1024 * 1024 * 1024 * usage),
                    FreeSpace = (ulong)((driveLetter == "C" ? 500.0 : driveLetter == "D" ? 500.0 : 2000.0) * 1024 * 1024 * 1024 * (1 - usage)),
                    UsagePercentage = Math.Round(usage * 100, 1)
                });
            }
            
            return history;
        }

        private List<BenchmarkDataPoint> GenerateBenchmarkDataPoints(BenchmarkType type)
        {
            var dataPoints = new List<BenchmarkDataPoint>();
            var blockSizes = new[] { 4, 8, 16, 32, 64, 128, 256, 512, 1024 };
            
            foreach (var blockSize in blockSizes)
            {
                dataPoints.Add(new BenchmarkDataPoint
                {
                    BlockSize = blockSize,
                    ReadSpeed = Math.Round(1000 + (_random.NextDouble() * 6000), 0),
                    WriteSpeed = Math.Round(500 + (_random.NextDouble() * 4000), 0),
                    IoPs = Math.Round(100000 + (_random.NextDouble() * 900000), 0),
                    AccessTime = Math.Round(0.05 + (_random.NextDouble() * 0.15), 2)
                });
            }
            
            return dataPoints;
        }

        private List<CleanupCategory> GenerateCleanupCategories()
        {
            return new List<CleanupCategory>
            {
                new CleanupCategory
                {
                    Name = "Temporary Files",
                    Description = "Files that can be safely deleted",
                    SpaceFreed = 2 * 1024 * 1024 * 1024,
                    FilesDeleted = 10000,
                    WasSelected = true
                },
                new CleanupCategory
                {
                    Name = "Download Cache",
                    Description = "Cached files from web browsers",
                    SpaceFreed = 1 * 1024 * 1024 * 1024,
                    FilesDeleted = 3000,
                    WasSelected = true
                },
                new CleanupCategory
                {
                    Name = "Recycle Bin",
                    Description = "Files in the Recycle Bin",
                    SpaceFreed = 1.5 * 1024 * 1024 * 1024,
                    FilesDeleted = 1500,
                    WasSelected = true
                },
                new CleanupCategory
                {
                    Name = "Windows Update Cleanup",
                    Description = "Old Windows update files",
                    SpaceFreed = 0.5 * 1024 * 1024 * 1024,
                    FilesDeleted = 200,
                    WasSelected = false
                }
            };
        }

        private List<FolderSizeInfo> GenerateSubFolders(string parentPath, int depth)
        {
            var folders = new List<FolderSizeInfo>();
            
            if (depth <= 0)
                return folders;
            
            var subFolderNames = new[] { "AppData", "Documents", "Pictures", "Videos", "Downloads" };
            
            for (int i = 0; i < subFolderNames.Length; i++)
            {
                var folder = new FolderSizeInfo
                {
                    FolderPath = $@"{parentPath}\{subFolderNames[i]}",
                    FolderName = subFolderNames[i],
                    FolderSize = (ulong)((5 + (_random.NextDouble() * 20)) * 1024 * 1024 * 1024),
                    FileCount = _random.Next(1000, 10000),
                    FolderCount = _random.Next(50, 500),
                    Depth = depth,
                    SubFolders = GenerateSubFolders($@"{parentPath}\{subFolderNames[i]}", depth - 1)
                };
                folders.Add(folder);
            }
            
            return folders;
        }

        #endregion
    }
}

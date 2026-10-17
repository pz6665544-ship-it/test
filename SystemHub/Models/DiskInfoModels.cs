using System;
using System.Collections.Generic;

namespace SystemHub.Models
{
    // Physical Disk Information
    public class PhysicalDiskInfo
    {
        public int DiskNumber { get; set; } = 0;
        public string DiskId { get; set; } = "\\\\.\\PHYSICALDRIVE0";
        public string Model { get; set; } = "Samsung SSD 980 PRO 1TB";
        public string Manufacturer { get; set; } = "Samsung";
        public string SerialNumber { get; set; } = "S5ABC123456789";
        public string Firmware { get; set; } = "3B2QGXA7";
        public ulong Size { get; set; } = 1000 * 1024 * 1024 * 1024; // 1 TB
        public string InterfaceType { get; set; } = "NVMe";
        public string BusType { get; set; } = "PCIe 4.0 x4";
        public string MediaType { get; set; } = "SSD";
        public string PartitionStyle { get; set; } = "GPT";
        public bool IsBootDisk { get; set; } = true;
        public bool IsSystemDisk { get; set; } = true;
        public bool IsRemovable { get; set; } = false;
        public bool IsOnline { get; set; } = true;
        public string HealthStatus { get; set; } = "Healthy";
        public string OperationalStatus { get; set; } = "Online";
        public double Temperature { get; set; } = 35.0; // Celsius
        public ulong BytesPerSector { get; set; } = 512;
        public ulong TotalSectors { get; set; } = 1953125000;
        public string RotationRate { get; set; } = "N/A (SSD)";
        public string FormFactor { get; set; } = "M.2";
        
        public List<PartitionInfo> Partitions { get; set; } = new();
        public List<SmartData> SmartData { get; set; } = new();
    }

    // Logical Drive Information
    public class LogicalDriveInfo
    {
        public string DriveLetter { get; set; } = "C";
        public string DriveType { get; set; } = "Fixed";
        public string FileSystem { get; set; } = "NTFS";
        public string Label { get; set; } = "OS";
        public ulong TotalSize { get; set; } = 1000 * 1024 * 1024 * 1024; // 1 TB
        public ulong TotalFreeSpace { get; set; } = 450 * 1024 * 1024 * 1024; // 450 GB
        public ulong TotalUsedSpace => TotalSize - TotalFreeSpace;
        public double UsagePercentage => (double)TotalUsedSpace / TotalSize * 100;
        public bool IsReady { get; set; } = true;
        public bool IsNetworkDrive { get; set; } = false;
        public bool IsRemovable { get; set; } = false;
        public bool IsCDRom { get; set; } = false;
        public string VolumeName { get; set; } = "OS";
        public string VolumeSerialNumber { get; set; } = "ABCD-1234";
        public string Format { get; set; } = "NTFS";
        public bool IsCompressed { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;
        public bool IsBitLockerEncrypted { get; set; } = false;
        
        public List<FileSystemInfo> FileSystemInfo { get; set; } = new();
    }

    // Partition Information
    public class PartitionInfo
    {
        public int PartitionNumber { get; set; } = 1;
        public string PartitionId { get; set; } = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}";
        public string Type { get; set; } = "Basic";
        public string FileSystem { get; set; } = "NTFS";
        public ulong Size { get; set; } = 500 * 1024 * 1024 * 1024; // 500 GB
        public ulong Offset { get; set; } = 1024 * 1024; // 1 MB
        public bool IsBootable { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool IsPrimary { get; set; } = true;
        public bool IsLogical { get; set; } = false;
        public string DriveLetter { get; set; } = "C";
        public string Label { get; set; } = "OS";
        public string VolumeName { get; set; } = "\\?\\Volume{abc12345-6789-0abc-def0-123456789abc}\\\n        ";
    }

    // SMART Data
    public class SmartData
    {
        public int AttributeId { get; set; } = 1;
        public string AttributeName { get; set; } = "Raw Read Error Rate";
        public string AttributeFlag { get; set; } = "Pre-failure";
        public byte Value { get; set; } = 100;
        public byte Worst { get; set; } = 100;
        public byte Threshold { get; set; } = 50;
        public ulong RawValue { get; set; } = 0;
        public string Status { get; set; } = "Good";
        public bool IsCritical => Value <= Threshold;
        public bool IsWarning => Value <= Threshold * 2;
    }

    // File System Information
    public class FileSystemInfo
    {
        public string FileSystem { get; set; } = "NTFS";
        public string DriveLetter { get; set; } = "C";
        public ulong TotalClusters { get; set; } = 244190592;
        public ulong FreeClusters { get; set; } = 110000000;
        public ulong TotalBytes { get; set; } = 1000 * 1024 * 1024 * 1024; // 1 TB
        public ulong FreeBytes { get; set; } = 450 * 1024 * 1024 * 1024; // 450 GB
        public ulong BytesPerCluster { get; set; } = 4096;
        public string VolumeName { get; set; } = "OS";
        public string VolumeSerialNumber { get; set; } = "ABCD-1234";
        public bool IsCompressed { get; set; } = false;
        public bool IsEncrypted { get; set; } = false;
        public bool SupportsHardLinks { get; set; } = true;
        public bool SupportsSymbolicLinks { get; set; } = true;
        public bool SupportsReparsePoints { get; set; } = true;
        public bool SupportsSparseFiles { get; set; } = true;
        public bool SupportsTransactions { get; set; } = true;
        public bool SupportsUsnJournal { get; set; } = true;
        public bool SupportsQuotas { get; set; } = true;
    }

    // Disk Health Information
    public class DiskHealthInfo
    {
        public string DiskId { get; set; } = "\\\\.\\PHYSICALDRIVE0";
        public string DiskName { get; set; } = "Samsung SSD 980 PRO 1TB";
        public string HealthStatus { get; set; } = "Good";
        public double HealthPercentage { get; set; } = 95.0;
        public string HealthDescription { get; set; } = "The disk is in good health.";
        public int TotalPowerOnHours { get; set; } = 5000;
        public int PowerCycleCount { get; set; } = 500;
        public ulong TotalBytesRead { get; set; } = 1024 * 1024 * 1024 * 1024 * 5; // 5 TB
        public ulong TotalBytesWritten { get; set; } = 1024 * 1024 * 1024 * 1024 * 3; // 3 TB
        public ulong TotalBytesReadFromCache { get; set; } = 1024 * 1024 * 1024 * 100; // 100 GB
        public ulong TotalBytesWrittenToCache { get; set; } = 1024 * 1024 * 1024 * 50; // 50 GB
        public int Temperature { get; set; } = 35;
        public int AvailableSpare { get; set; } = 100;
        public int AvailableSpareThreshold { get; set; } = 10;
        public int PercentageUsed { get; set; } = 25;
        public DateTime? PredictedFailureDate { get; set; } = null;
        public int RemainingLifetimePercentage { get; set; } = 95;
        
        public List<SmartAttribute> SmartAttributes { get; set; } = new();
        public List<DiskHealthWarning> Warnings { get; set; } = new();
    }

    public class SmartAttribute
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Raw Read Error Rate";
        public string Category { get; set; } = "Error Rate";
        public byte Value { get; set; } = 100;
        public byte Worst { get; set; } = 100;
        public byte Threshold { get; set; } = 50;
        public ulong RawValue { get; set; } = 0;
        public string Status { get; set; } = "Good";
    }

    public class DiskHealthWarning
    {
        public string Type { get; set; } = "Warning";
        public string Message { get; set; } = "High temperature detected";
        public string Description { get; set; } = "The disk temperature has exceeded the recommended threshold.";
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Severity { get; set; } = "Medium";
    }

    // Disk Usage Information
    public class DiskUsageInfo
    {
        public string DriveLetter { get; set; } = "C";
        public string FileSystem { get; set; } = "NTFS";
        public ulong TotalSize { get; set; } = 1000 * 1024 * 1024 * 1024; // 1 TB
        public ulong UsedSpace { get; set; } = 550 * 1024 * 1024 * 1024; // 550 GB
        public ulong FreeSpace { get; set; } = 450 * 1024 * 1024 * 1024; // 450 GB
        public double UsagePercentage => (double)UsedSpace / TotalSize * 100;
        public double FreePercentage => 100 - UsagePercentage;
        
        public List<FileTypeUsage> FileTypeUsage { get; set; } = new();
        public List<FolderUsage> FolderUsage { get; set; } = new();
        public List<DiskUsageHistory> UsageHistory { get; set; } = new();
    }

    public class FileTypeUsage
    {
        public string FileType { get; set; } = ".exe";
        public string Category { get; set; } = "Executable";
        public int FileCount { get; set; } = 1500;
        public ulong TotalSize { get; set; } = 5 * 1024 * 1024 * 1024; // 5 GB
        public double PercentageOfTotal { get; set; } = 1.5;
    }

    public class FolderUsage
    {
        public string FolderPath { get; set; } = "C:\\Program Files";
        public string FolderName { get; set; } = "Program Files";
        public int FileCount { get; set; } = 10000;
        public int FolderCount { get; set; } = 500;
        public ulong TotalSize { get; set; } = 100 * 1024 * 1024 * 1024; // 100 GB
        public double PercentageOfTotal { get; set; } = 20.0;
    }

    public class DiskUsageHistory
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public ulong UsedSpace { get; set; } = 550 * 1024 * 1024 * 1024; // 550 GB
        public ulong FreeSpace { get; set; } = 450 * 1024 * 1024 * 1024; // 450 GB
        public double UsagePercentage { get; set; } = 55.0;
    }

    // Benchmark Result
    public class BenchmarkResult
    {
        public string DiskId { get; set; } = "\\\\.\\PHYSICALDRIVE0";
        public string DiskName { get; set; } = "Samsung SSD 980 PRO 1TB";
        public BenchmarkType BenchmarkType { get; set; } = BenchmarkType.SequentialRead;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public double Score { get; set; } = 3500.0; // MB/s
        public double ReadSpeed { get; set; } = 7000.0; // MB/s
        public double WriteSpeed { get; set; } = 5000.0; // MB/s
        public double IoPs { get; set; } = 1000000.0; // IO operations per second
        public double AccessTime { get; set; } = 0.1; // ms
        public double Latency { get; set; } = 0.05; // ms
        public double CpuUsage { get; set; } = 15.0; // Percentage
        
        public List<BenchmarkDataPoint> DataPoints { get; set; } = new();
    }

    public enum BenchmarkType
    {
        SequentialRead,
        SequentialWrite,
        RandomRead4K,
        RandomWrite4K,
        Mixed,
        Full
    }

    public class BenchmarkDataPoint
    {
        public int BlockSize { get; set; } = 4; // KB
        public double ReadSpeed { get; set; } = 7000.0; // MB/s
        public double WriteSpeed { get; set; } = 5000.0; // MB/s
        public double IoPs { get; set; } = 1000000.0;
        public double AccessTime { get; set; } = 0.1; // ms
    }

    // Disk Cleanup Result
    public class DiskCleanupResult
    {
        public string DriveLetter { get; set; } = "C";
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsSuccessful { get; set; } = true;
        public ulong TotalSpaceFreed { get; set; } = 5 * 1024 * 1024 * 1024; // 5 GB
        public int FilesDeleted { get; set; } = 15000;
        public int FoldersDeleted { get; set; } = 500;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(30);
        
        public List<CleanupCategory> Categories { get; set; } = new();
    }

    public class CleanupCategory
    {
        public string Name { get; set; } = "Temporary Files";
        public string Description { get; set; } = "Files that can be safely deleted";
        public ulong SpaceFreed { get; set; } = 2 * 1024 * 1024 * 1024; // 2 GB
        public int FilesDeleted { get; set; } = 10000;
        public bool WasSelected { get; set; } = true;
    }

    // Defrag Result
    public class DefragResult
    {
        public string DriveLetter { get; set; } = "D";
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsSuccessful { get; set; } = true;
        public double InitialFragmentation { get; set; } = 45.0; // Percentage
        public double FinalFragmentation { get; set; } = 5.0; // Percentage
        public ulong TotalSpaceDefragged { get; set; } = 200 * 1024 * 1024 * 1024; // 200 GB
        public int FilesProcessed { get; set; } = 50000;
        public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(15);
        public DefragType Type { get; set; } = DefragType.Full;
    }

    public enum DefragType
    {
        Full,
        Quick,
        Optimize
    }

    // Large File Information
    public class LargeFileInfo
    {
        public string FilePath { get; set; } = "C:\\Program Files\\MyApp\\data.bin";
        public string FileName { get; set; } = "data.bin";
        public string Directory { get; set; } = "C:\\Program Files\\MyApp";
        public ulong FileSize { get; set; } = 2 * 1024 * 1024 * 1024; // 2 GB
        public string FileSizeFormatted => FormatFileSize(FileSize);
        public DateTime LastModified { get; set; } = DateTime.Now.AddDays(-30);
        public DateTime Created { get; set; } = DateTime.Now.AddDays(-90);
        public DateTime LastAccessed { get; set; } = DateTime.Now.AddDays(-7);
        public string FileType { get; set; } = "Binary";
        public string Attributes { get; set; } = "Archive";
        
        private static string FormatFileSize(ulong bytes)
        {
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

    // Folder Size Information
    public class FolderSizeInfo
    {
        public string FolderPath { get; set; } = "C:\\Program Files";
        public string FolderName { get; set; } = "Program Files";
        public ulong FolderSize { get; set; } = 100 * 1024 * 1024 * 1024; // 100 GB
        public string FolderSizeFormatted => FormatFileSize(FolderSize);
        public int FileCount { get; set; } = 50000;
        public int FolderCount { get; set; } = 1000;
        public int Depth { get; set; } = 0;
        public List<FolderSizeInfo> SubFolders { get; set; } = new();
        
        private static string FormatFileSize(ulong bytes)
        {
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

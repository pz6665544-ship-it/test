using System;
using System.Collections.Generic;
using SystemHub.Models;

namespace SystemHub.Services.Interfaces
{
    public interface IDiskInfoService
    {
        // Get all physical disks
        List<PhysicalDiskInfo> GetPhysicalDisks();
        
        // Get all logical drives
        List<LogicalDriveInfo> GetLogicalDrives();
        
        // Get disk health information
        List<DiskHealthInfo> GetDiskHealth();
        
        // Get disk usage statistics
        List<DiskUsageInfo> GetDiskUsage();
        
        // Get partition information
        List<PartitionInfo> GetPartitions();
        
        // Get SMART data
        List<SmartData> GetSmartData(string diskId);
        
        // Get file system information
        List<FileSystemInfo> GetFileSystems();
        
        // Disk benchmarking
        BenchmarkResult RunDiskBenchmark(string diskId, BenchmarkType type);
        
        // Disk cleanup
        DiskCleanupResult RunDiskCleanup(string driveLetter);
        
        // Disk defragmentation
        DefragResult RunDefragmentation(string driveLetter);
        
        // Get largest files
        List<LargeFileInfo> GetLargestFiles(string path, int topN = 100);
        
        // Get folder sizes
        List<FolderSizeInfo> GetFolderSizes(string path, int depth = 3);
    }

    public enum BenchmarkType
    {
        SequentialRead,
        SequentialWrite,
        RandomRead4K,
        RandomWrite4K,
        Mixed
    }
}

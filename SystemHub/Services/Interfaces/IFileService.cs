using System;
using System.Collections.Generic;
using SystemHub.Models;

namespace SystemHub.Services.Interfaces
{
    public interface IFileService
    {
        // File Explorer
        List<FileSystemItem> GetDirectoryContents(string path);
        List<DriveInfo> GetDrives();
        
        // File Search
        List<FileSearchResult> SearchFiles(string searchPattern, string searchPath, bool searchSubfolders = true);
        List<FileSearchResult> SearchFilesByContent(string searchText, string searchPath, bool searchSubfolders = true);
        
        // File Operations
        bool CopyFile(string sourcePath, string destinationPath, bool overwrite = false);
        bool MoveFile(string sourcePath, string destinationPath);
        bool DeleteFile(string filePath, bool permanent = false);
        bool RenameFile(string filePath, string newName);
        
        // Folder Operations
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path, bool recursive = true, bool permanent = false);
        bool RenameDirectory(string directoryPath, string newName);
        
        // File Information
        FileInfo GetFileInfo(string filePath);
        DirectoryInfo GetDirectoryInfo(string directoryPath);
        
        // Duplicate Finder
        List<DuplicateFileGroup> FindDuplicateFiles(string path, bool searchSubfolders = true);
        
        // Large Files
        List<LargeFileInfo> GetLargestFiles(string path, int topN = 100, bool searchSubfolders = true);
        
        // File Comparison
        bool CompareFiles(string filePath1, string filePath2);
        
        // Batch Operations
        BatchOperationResult BatchRename(List<FileRenameOperation> operations);
        BatchOperationResult BatchDelete(List<string> filePaths, bool permanent = false);
        BatchOperationResult BatchCopy(List<FileCopyOperation> operations, bool overwrite = false);
        
        // Archive Operations
        bool CreateArchive(string archivePath, List<string> filesToArchive, ArchiveFormat format);
        bool ExtractArchive(string archivePath, string extractPath);
        
        // File Split/Join
        bool SplitFile(string filePath, string outputPath, long chunkSize);
        bool JoinFiles(List<string> chunkPaths, string outputPath);
        
        // File Properties
        Dictionary<string, string> GetFileProperties(string filePath);
        bool SetFileProperties(string filePath, Dictionary<string, string> properties);
        
        // File Permissions
        FilePermissionInfo GetFilePermissions(string filePath);
        bool SetFilePermissions(string filePath, FilePermissionInfo permissions);
    }

    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsFile => !IsDirectory;
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastAccessed { get; set; }
        public string Attributes { get; set; }
        public string Extension { get; set; }
        public string TypeDescription { get; set; }
        
        private static string FormatFileSize(long bytes)
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

    public class DriveInfo
    {
        public string DriveLetter { get; set; }
        public string DriveType { get; set; }
        public string FileSystem { get; set; }
        public string Label { get; set; }
        public long TotalSize { get; set; }
        public long TotalFreeSpace { get; set; }
        public long TotalUsedSpace => TotalSize - TotalFreeSpace;
        public double UsagePercentage => (double)TotalUsedSpace / TotalSize * 100;
        public bool IsReady { get; set; }
        public bool IsNetworkDrive { get; set; }
        public bool IsRemovable { get; set; }
        public bool IsCDRom { get; set; }
    }

    public class FileSearchResult
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Directory { get; set; }
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public DateTime LastModified { get; set; }
        public string Extension { get; set; }
        public string TypeDescription { get; set; }
        
        private static string FormatFileSize(long bytes)
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

    public class DuplicateFileGroup
    {
        public string Hash { get; set; }
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public List<string> FilePaths { get; set; } = new();
        public int FileCount => FilePaths.Count;
        
        private static string FormatFileSize(long bytes)
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

    public class FileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastAccessed { get; set; }
        public string Attributes { get; set; }
        public string Extension { get; set; }
        public string TypeDescription { get; set; }
        public string Owner { get; set; }
        public string MD5Hash { get; set; }
        public string SHA1Hash { get; set; }
        
        private static string FormatFileSize(long bytes)
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

    public class DirectoryInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastAccessed { get; set; }
        public string Attributes { get; set; }
        public long FileCount { get; set; }
        public long FolderCount { get; set; }
        public long TotalSize { get; set; }
        public string TotalSizeFormatted => FormatFileSize(TotalSize);
        
        private static string FormatFileSize(long bytes)
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

    public class FileRenameOperation
    {
        public string SourcePath { get; set; }
        public string NewName { get; set; }
    }

    public class FileCopyOperation
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
    }

    public class BatchOperationResult
    {
        public bool IsSuccessful { get; set; }
        public int TotalOperations { get; set; }
        public int SuccessfulOperations { get; set; }
        public int FailedOperations { get; set; }
        public List<OperationError> Errors { get; set; } = new();
        public TimeSpan Duration { get; set; }
    }

    public class OperationError
    {
        public string Operation { get; set; }
        public string SourcePath { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ArchiveFormat
    {
        Zip,
        SevenZip,
        Tar,
        TarGz,
        TarBz2
    }

    public class FilePermissionInfo
    {
        public bool IsReadOnly { get; set; }
        public bool IsHidden { get; set; }
        public bool IsSystem { get; set; }
        public bool IsArchive { get; set; }
        public string Owner { get; set; }
        public string Group { get; set; }
        public Dictionary<string, bool> Permissions { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using SystemHub.Models;

namespace SystemHub.Services.Interfaces
{
    public interface IUninstallerService
    {
        // Get installed programs
        List<InstalledProgram> GetInstalledPrograms();
        
        // Get programs by category
        List<InstalledProgram> GetProgramsByCategory(string category);
        
        // Get recently installed programs
        List<InstalledProgram> GetRecentlyInstalled(int days = 30);
        
        // Get large programs
        List<InstalledProgram> GetLargePrograms(long minSize = 100 * 1024 * 1024);
        
        // Get infrequently used programs
        List<InstalledProgram> GetInfrequentlyUsedPrograms(int daysThreshold = 90);
        
        // Uninstall a program
        UninstallResult UninstallProgram(string programId, bool force = false);
        
        // Bulk uninstall
        BulkUninstallResult BulkUninstall(List<string> programIds, bool force = false);
        
        // Forced uninstall
        UninstallResult ForcedUninstall(string programId);
        
        // Clean up leftovers
        CleanupResult CleanupLeftovers(string programId);
        
        // Get startup programs
        List<StartupProgram> GetStartupPrograms();
        
        // Enable/disable startup program
        bool SetStartupProgramEnabled(string programId, bool enabled);
        
        // Get program details
        ProgramDetails GetProgramDetails(string programId);
        
        // Search for programs
        List<InstalledProgram> SearchPrograms(string searchTerm);
        
        // Get uninstall history
        List<UninstallHistoryItem> GetUninstallHistory();
        
        // Get registry entries for a program
        List<RegistryEntry> GetProgramRegistryEntries(string programId);
        
        // Get file leftovers for a program
        List<string> GetProgramFileLeftovers(string programId);
    }

    public class InstalledProgram
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string InstallLocation { get; set; }
        public string InstallSource { get; set; }
        public DateTime InstallDate { get; set; }
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public string UninstallString { get; set; }
        public string QuietUninstallString { get; set; }
        public bool IsSystemComponent { get; set; }
        public bool IsWindowsUpdate { get; set; }
        public string Category { get; set; }
        public string IconPath { get; set; }
        public int UsageCount { get; set; }
        public DateTime LastUsed { get; set; }
        public bool CanUninstall { get; set; }
        
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

    public class ProgramDetails : InstalledProgram
    {
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Contact { get; set; }
        public string HelpLink { get; set; }
        public string HelpTelephone { get; set; }
        public string Readme { get; set; }
        public string URLInfoAbout { get; set; }
        public string URLUpdateInfo { get; set; }
        public List<string> Dependencies { get; set; } = new();
        public List<string> Features { get; set; } = new();
    }

    public class StartupProgram
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Command { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
        public bool IsWindowsService { get; set; }
        public StartupType Type { get; set; }
        public DateTime? LastRunTime { get; set; }
        public int RunCount { get; set; }
    }

    public enum StartupType
    {
        Registry,
        StartupFolder,
        ScheduledTask,
        Service,
        Unknown
    }

    public class UninstallResult
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public bool WasForced { get; set; }
        public DateTime Timestamp { get; set; }
        public TimeSpan Duration { get; set; }
        public long SpaceFreed { get; set; }
        public string SpaceFreedFormatted => FormatFileSize(SpaceFreed);
        public bool RequiresReboot { get; set; }
        public List<string> FilesRemoved { get; set; } = new();
        public List<string> RegistryKeysRemoved { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
        
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

    public class BulkUninstallResult
    {
        public List<UninstallResult> Results { get; set; } = new();
        public int TotalPrograms { get; set; }
        public int Successful { get; set; }
        public int Failed { get; set; }
        public DateTime Timestamp { get; set; }
        public TimeSpan Duration { get; set; }
        public long TotalSpaceFreed { get; set; }
        public string TotalSpaceFreedFormatted => FormatFileSize(TotalSpaceFreed);
        
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

    public class CleanupResult
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> FilesRemoved { get; set; } = new();
        public List<string> RegistryKeysRemoved { get; set; } = new();
        public List<string> FoldersRemoved { get; set; } = new();
        public long SpaceFreed { get; set; }
        public string SpaceFreedFormatted => FormatFileSize(SpaceFreed);
        public DateTime Timestamp { get; set; }
        
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

    public class UninstallHistoryItem
    {
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Version { get; set; }
        public DateTime UninstallDate { get; set; }
        public bool WasSuccessful { get; set; }
        public bool WasForced { get; set; }
        public long SpaceFreed { get; set; }
        public string SpaceFreedFormatted => FormatFileSize(SpaceFreed);
        
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

    public class RegistryEntry
    {
        public string Key { get; set; }
        public string ValueName { get; set; }
        public string ValueData { get; set; }
        public RegistryValueKind ValueKind { get; set; }
    }

    public enum RegistryValueKind
    {
        Unknown,
        String,
        ExpandString,
        Binary,
        DWORD,
        MultiString,
        QWORD
    }
}

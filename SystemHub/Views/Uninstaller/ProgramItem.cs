using System;

namespace SystemHub.Views.Uninstaller
{
    public class ProgramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string InstallDate { get; set; }
        public long Size { get; set; }
        public string SizeFormatted => FormatFileSize(Size);
        public string Category { get; set; }
        public string LastUsed { get; set; }
        public string IconPath { get; set; }
        public bool IsSelected { get; set; }

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
}

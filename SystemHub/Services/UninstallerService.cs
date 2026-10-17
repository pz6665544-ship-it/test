using System;
using System.Collections.Generic;
using SystemHub.Models;
using SystemHub.Services.Interfaces;

namespace SystemHub.Services
{
    public class UninstallerService : IUninstallerService
    {
        private readonly Random _random = new();
        private readonly List<InstalledProgram> _programs = GenerateSamplePrograms();
        private readonly List<StartupProgram> _startupPrograms = GenerateSampleStartupPrograms();
        private readonly List<UninstallHistoryItem> _uninstallHistory = GenerateSampleUninstallHistory();

        public List<InstalledProgram> GetInstalledPrograms()
        {
            return new List<InstalledProgram>(_programs);
        }

        public List<InstalledProgram> GetProgramsByCategory(string category)
        {
            return _programs.FindAll(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        public List<InstalledProgram> GetRecentlyInstalled(int days = 30)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return _programs.FindAll(p => p.InstallDate >= cutoffDate);
        }

        public List<InstalledProgram> GetLargePrograms(long minSize = 100 * 1024 * 1024)
        {
            return _programs.FindAll(p => p.Size >= minSize);
        }

        public List<InstalledProgram> GetInfrequentlyUsedPrograms(int daysThreshold = 90)
        {
            var cutoffDate = DateTime.Now.AddDays(-daysThreshold);
            return _programs.FindAll(p => p.LastUsed < cutoffDate);
        }

        public UninstallResult UninstallProgram(string programId, bool force = false)
        {
            var program = _programs.Find(p => p.Id == programId);
            if (program == null)
            {
                return new UninstallResult
                {
                    ProgramId = programId,
                    ProgramName = "Unknown",
                    IsSuccessful = false,
                    ErrorMessage = "Program not found",
                    WasForced = force,
                    Timestamp = DateTime.Now,
                    Duration = TimeSpan.FromSeconds(_random.Next(5, 30))
                };
            }

            // Simulate uninstall
            var success = _random.Next(10) != 0 || force; // Higher success rate with force
            var spaceFreed = program.Size;
            
            var result = new UninstallResult
            {
                ProgramId = programId,
                ProgramName = program.Name,
                IsSuccessful = success,
                ErrorMessage = success ? null : "Failed to uninstall program",
                WasForced = force,
                Timestamp = DateTime.Now,
                Duration = TimeSpan.FromSeconds(_random.Next(5, 60)),
                SpaceFreed = spaceFreed,
                RequiresReboot = _random.Next(5) == 0,
                FilesRemoved = GenerateRandomFileList(program.InstallLocation, 20),
                RegistryKeysRemoved = GenerateRandomRegistryKeys(program.Name, 10),
                Warnings = success ? new List<string>() : new List<string> { "Some files could not be removed" }
            };

            if (success)
            {
                _programs.Remove(program);
                _uninstallHistory.Add(new UninstallHistoryItem
                {
                    ProgramId = programId,
                    ProgramName = program.Name,
                    Version = program.Version,
                    UninstallDate = DateTime.Now,
                    WasSuccessful = true,
                    WasForced = force,
                    SpaceFreed = spaceFreed
                });
            }

            return result;
        }

        public BulkUninstallResult BulkUninstall(List<string> programIds, bool force = false)
        {
            var results = new List<UninstallResult>();
            var totalSpaceFreed = 0L;
            var startTime = DateTime.Now;

            foreach (var programId in programIds)
            {
                var result = UninstallProgram(programId, force);
                results.Add(result);
                if (result.IsSuccessful)
                {
                    totalSpaceFreed += result.SpaceFreed;
                }
            }

            return new BulkUninstallResult
            {
                Results = results,
                TotalPrograms = programIds.Count,
                Successful = results.Count(r => r.IsSuccessful),
                Failed = results.Count(r => !r.IsSuccessful),
                Timestamp = startTime,
                Duration = DateTime.Now - startTime,
                TotalSpaceFreed = totalSpaceFreed
            };
        }

        public UninstallResult ForcedUninstall(string programId)
        {
            return UninstallProgram(programId, force: true);
        }

        public CleanupResult CleanupLeftovers(string programId)
        {
            var program = _programs.Find(p => p.Id == programId);
            if (program == null)
            {
                return new CleanupResult
                {
                    ProgramId = programId,
                    ProgramName = "Unknown",
                    IsSuccessful = false
                };
            }

            var filesRemoved = GenerateRandomFileList(program.InstallLocation, 50);
            var registryKeysRemoved = GenerateRandomRegistryKeys(program.Name, 20);
            var foldersRemoved = GenerateRandomFolders(program.InstallLocation, 10);
            var spaceFreed = (long)(_random.NextDouble() * program.Size * 0.3);

            return new CleanupResult
            {
                ProgramId = programId,
                ProgramName = program.Name,
                IsSuccessful = true,
                FilesRemoved = filesRemoved,
                RegistryKeysRemoved = registryKeysRemoved,
                FoldersRemoved = foldersRemoved,
                SpaceFreed = spaceFreed,
                Timestamp = DateTime.Now
            };
        }

        public List<StartupProgram> GetStartupPrograms()
        {
            return new List<StartupProgram>(_startupPrograms);
        }

        public bool SetStartupProgramEnabled(string programId, bool enabled)
        {
            var program = _startupPrograms.Find(p => p.Id == programId);
            if (program != null)
            {
                program.Enabled = enabled;
                return true;
            }
            return false;
        }

        public ProgramDetails GetProgramDetails(string programId)
        {
            var program = _programs.Find(p => p.Id == programId);
            if (program == null)
                return null;

            return new ProgramDetails
            {
                Id = program.Id,
                Name = program.Name,
                DisplayName = program.DisplayName,
                Publisher = program.Publisher,
                Version = program.Version,
                InstallLocation = program.InstallLocation,
                InstallSource = program.InstallSource,
                InstallDate = program.InstallDate,
                Size = program.Size,
                UninstallString = program.UninstallString,
                QuietUninstallString = program.QuietUninstallString,
                IsSystemComponent = program.IsSystemComponent,
                IsWindowsUpdate = program.IsWindowsUpdate,
                Category = program.Category,
                IconPath = program.IconPath,
                UsageCount = program.UsageCount,
                LastUsed = program.LastUsed,
                CanUninstall = program.CanUninstall,
                Description = GetProgramDescription(programId),
                Comments = "No additional comments",
                Contact = GetPublisherContact(program.Publisher),
                HelpLink = GetHelpLink(program.Name),
                HelpTelephone = "N/A",
                Readme = "No readme available",
                URLInfoAbout = GetAboutUrl(program.Publisher),
                URLUpdateInfo = GetUpdateUrl(program.Name),
                Dependencies = GetDependencies(programId),
                Features = GetFeatures(programId)
            };
        }

        public List<InstalledProgram> SearchPrograms(string searchTerm)
        {
            return _programs.FindAll(p => 
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.DisplayName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Publisher.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public List<UninstallHistoryItem> GetUninstallHistory()
        {
            return new List<UninstallHistoryItem>(_uninstallHistory);
        }

        public List<RegistryEntry> GetProgramRegistryEntries(string programId)
        {
            var program = _programs.Find(p => p.Id == programId);
            if (program == null)
                return new List<RegistryEntry>();

            return GenerateRandomRegistryKeys(program.Name, 50);
        }

        public List<string> GetProgramFileLeftovers(string programId)
        {
            var program = _programs.Find(p => p.Id == programId);
            if (program == null)
                return new List<string>();

            return GenerateRandomFileList(program.InstallLocation, 100);
        }

        #region Helper Methods

        private static List<InstalledProgram> GenerateSamplePrograms()
        {
            var programs = new List<InstalledProgram>();
            var categories = new[] { "Productivity", "Multimedia", "Utilities", "Games", "Development", "Security", "Communication", "Browser", "System" };
            var publishers = new[] { "Microsoft", "Adobe", "Google", "Mozilla", "NVIDIA", "Intel", "Apple", "Oracle", "JetBrains", "Autodesk" };
            
            // System programs
            programs.Add(new InstalledProgram
            {
                Id = "Win11",
                Name = "Windows 11 Pro",
                DisplayName = "Windows 11 Pro",
                Publisher = "Microsoft",
                Version = "22H2",
                InstallLocation = "C:\\Windows",
                InstallSource = "Microsoft",
                InstallDate = new DateTime(2023, 1, 1),
                Size = 25 * 1024 * 1024 * 1024,
                UninstallString = null,
                QuietUninstallString = null,
                IsSystemComponent = true,
                IsWindowsUpdate = false,
                Category = "System",
                IconPath = "imageres.dll,-102",
                UsageCount = 0,
                LastUsed = DateTime.MinValue,
                CanUninstall = false
            });

            // Office suite
            programs.Add(new InstalledProgram
            {
                Id = "Office365",
                Name = "Microsoft 365",
                DisplayName = "Microsoft 365 Apps for enterprise",
                Publisher = "Microsoft",
                Version = "16.0.16731.20206",
                InstallLocation = "C:\\Program Files\\Microsoft Office\\root\\Office16",
                InstallSource = "C:\\Program Files\\Microsoft Office",
                InstallDate = new DateTime(2023, 5, 15),
                Size = 3 * 1024 * 1024 * 1024,
                UninstallString = "MsiExec.exe /X{90160000-0011-0000-1000-0000000FF1CE}",
                QuietUninstallString = "MsiExec.exe /X{90160000-0011-0000-1000-0000000FF1CE} /qn",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Productivity",
                IconPath = "C:\\Program Files\\Microsoft Office\\root\\vfs\\ProgramFilesCommonX64\\Microsoft Shared\\OFFICE16\\msoxlicon.exe,0",
                UsageCount = 150,
                LastUsed = DateTime.Now.AddDays(-2),
                CanUninstall = true
            });

            // Web browsers
            programs.Add(new InstalledProgram
            {
                Id = "Chrome",
                Name = "Google Chrome",
                DisplayName = "Google Chrome",
                Publisher = "Google",
                Version = "119.0.6045.123",
                InstallLocation = "C:\\Program Files\\Google\\Chrome\\Application",
                InstallSource = "C:\\Program Files\\Google\\Chrome\\Application\\119.0.6045.123\\Installer\\chrome.7z",
                InstallDate = new DateTime(2023, 8, 1),
                Size = 300 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\Google\\Chrome\\Application\\119.0.6045.123\\Installer\\setup.exe\" --uninstall",
                QuietUninstallString = "\"C:\\Program Files\\Google\\Chrome\\Application\\119.0.6045.123\\Installer\\setup.exe\" --uninstall --force-uninstall",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Browser",
                IconPath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe,0",
                UsageCount = 500,
                LastUsed = DateTime.Now.AddHours(-1),
                CanUninstall = true
            });

            programs.Add(new InstalledProgram
            {
                Id = "Firefox",
                Name = "Mozilla Firefox",
                DisplayName = "Mozilla Firefox",
                Publisher = "Mozilla",
                Version = "115.0.3",
                InstallLocation = "C:\\Program Files\\Mozilla Firefox",
                InstallSource = "C:\\Program Files\\Mozilla Firefox\\uninstall\\helper.exe",
                InstallDate = new DateTime(2023, 7, 10),
                Size = 250 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\Mozilla Firefox\\uninstall\\helper.exe\"",
                QuietUninstallString = "\"C:\\Program Files\\Mozilla Firefox\\uninstall\\helper.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Browser",
                IconPath = "C:\\Program Files\\Mozilla Firefox\\firefox.exe,0",
                UsageCount = 200,
                LastUsed = DateTime.Now.AddDays(-3),
                CanUninstall = true
            });

            // Development tools
            programs.Add(new InstalledProgram
            {
                Id = "VS2022",
                Name = "Visual Studio 2022",
                DisplayName = "Microsoft Visual Studio 2022",
                Publisher = "Microsoft",
                Version = "17.8.0",
                InstallLocation = "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community",
                InstallSource = "C:\\Program Files\\Microsoft Visual Studio\\Installer\\vswhere.exe",
                InstallDate = new DateTime(2023, 6, 1),
                Size = 25 * 1024 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\Microsoft Visual Studio\\Installer\\vswhere.exe\" -legacy -products * -uninstall",
                QuietUninstallString = null,
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Development",
                IconPath = "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\devenv.exe,0",
                UsageCount = 300,
                LastUsed = DateTime.Now.AddDays(-1),
                CanUninstall = true
            });

            programs.Add(new InstalledProgram
            {
                Id = "VSCode",
                Name = "Visual Studio Code",
                DisplayName = "Visual Studio Code",
                Publisher = "Microsoft",
                Version = "1.84.2",
                InstallLocation = "C:\\Users\\SystemUser\\AppData\\Local\\Programs\\Microsoft VS Code",
                InstallSource = "C:\\Users\\SystemUser\\AppData\\Local\\Programs\\Microsoft VS Code\\unins000.exe",
                InstallDate = new DateTime(2023, 9, 1),
                Size = 400 * 1024 * 1024,
                UninstallString = "\"C:\\Users\\SystemUser\\AppData\\Local\\Programs\\Microsoft VS Code\\unins000.exe\"",
                QuietUninstallString = "\"C:\\Users\\SystemUser\\AppData\\Local\\Programs\\Microsoft VS Code\\unins000.exe\" /SILENT",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Development",
                IconPath = "C:\\Users\\SystemUser\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe,0",
                UsageCount = 400,
                LastUsed = DateTime.Now,
                CanUninstall = true
            });

            // Graphics
            programs.Add(new InstalledProgram
            {
                Id = "Photoshop",
                Name = "Adobe Photoshop 2024",
                DisplayName = "Adobe Photoshop 2024",
                Publisher = "Adobe",
                Version = "25.0.0",
                InstallLocation = "C:\\Program Files\\Adobe\\Adobe Photoshop 2024",
                InstallSource = "C:\\Program Files\\Adobe\\Adobe Photoshop 2024\\Uninstall\\Photoshop2024Uninstaller.exe",
                InstallDate = new DateTime(2023, 10, 1),
                Size = 2 * 1024 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\Adobe\\Adobe Photoshop 2024\\Uninstall\\Photoshop2024Uninstaller.exe\"",
                QuietUninstallString = "\"C:\\Program Files\\Adobe\\Adobe Photoshop 2024\\Uninstall\\Photoshop2024Uninstaller.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Multimedia",
                IconPath = "C:\\Program Files\\Adobe\\Adobe Photoshop 2024\\Photoshop.exe,0",
                UsageCount = 50,
                LastUsed = DateTime.Now.AddDays(-7),
                CanUninstall = true
            });

            // GPU drivers
            programs.Add(new InstalledProgram
            {
                Id = "NVIDIADriver",
                Name = "NVIDIA Graphics Driver",
                DisplayName = "NVIDIA Graphics Driver 536.99",
                Publisher = "NVIDIA",
                Version = "536.99",
                InstallLocation = "C:\\Program Files\\NVIDIA Corporation",
                InstallSource = "C:\\Windows\\System32\\nvuninst.exe",
                InstallDate = new DateTime(2023, 8, 15),
                Size = 1 * 1024 * 1024 * 1024,
                UninstallString = "\"C:\\Windows\\System32\\nvuninst.exe\"",
                QuietUninstallString = "\"C:\\Windows\\System32\\nvuninst.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "System",
                IconPath = "C:\\Program Files\\NVIDIA Corporation\\Display.NvContainer\\NVDisplay.Container.exe,0",
                UsageCount = 0,
                LastUsed = DateTime.MinValue,
                CanUninstall = true
            });

            // Games
            programs.Add(new InstalledProgram
            {
                Id = "Steam",
                Name = "Steam",
                DisplayName = "Steam",
                Publisher = "Valve",
                Version = "1.0.0.77",
                InstallLocation = "C:\\Program Files (x86)\\Steam",
                InstallSource = "C:\\Program Files (x86)\\Steam\\uninstall.exe",
                InstallDate = new DateTime(2023, 2, 1),
                Size = 1 * 1024 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files (x86)\\Steam\\uninstall.exe\"",
                QuietUninstallString = "\"C:\\Program Files (x86)\\Steam\\uninstall.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Games",
                IconPath = "C:\\Program Files (x86)\\Steam\\steam.exe,0",
                UsageCount = 200,
                LastUsed = DateTime.Now.AddDays(-5),
                CanUninstall = true
            });

            programs.Add(new InstalledProgram
            {
                Id = "EpicGames",
                Name = "Epic Games Launcher",
                DisplayName = "Epic Games Launcher",
                Publisher = "Epic Games",
                Version = "14.0.0",
                InstallLocation = "C:\\Program Files (x86)\\Epic Games\\Launcher",
                InstallSource = "C:\\Program Files (x86)\\Epic Games\\Launcher\\Portal\\Binaries\\Win32\\EpicGamesLauncher-Uninstaller.exe",
                InstallDate = new DateTime(2023, 3, 15),
                Size = 800 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files (x86)\\Epic Games\\Launcher\\Portal\\Binaries\\Win32\\EpicGamesLauncher-Uninstaller.exe\"",
                QuietUninstallString = "\"C:\\Program Files (x86)\\Epic Games\\Launcher\\Portal\\Binaries\\Win32\\EpicGamesLauncher-Uninstaller.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Games",
                IconPath = "C:\\Program Files (x86)\\Epic Games\\Launcher\\Portal\\Binaries\\Win32\\EpicGamesLauncher.exe,0",
                UsageCount = 150,
                LastUsed = DateTime.Now.AddDays(-10),
                CanUninstall = true
            });

            // Utilities
            programs.Add(new InstalledProgram
            {
                Id = "7Zip",
                Name = "7-Zip",
                DisplayName = "7-Zip 23.01",
                Publisher = "Igor Pavlov",
                Version = "23.01",
                InstallLocation = "C:\\Program Files\\7-Zip",
                InstallSource = "C:\\Program Files\\7-Zip\\Uninstall.exe",
                InstallDate = new DateTime(2023, 4, 1),
                Size = 30 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\7-Zip\\Uninstall.exe\"",
                QuietUninstallString = "\"C:\\Program Files\\7-Zip\\Uninstall.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Utilities",
                IconPath = "C:\\Program Files\\7-Zip\\7zFM.exe,0",
                UsageCount = 100,
                LastUsed = DateTime.Now.AddDays(-2),
                CanUninstall = true
            });

            programs.Add(new InstalledProgram
            {
                Id = "WinRAR",
                Name = "WinRAR",
                DisplayName = "WinRAR 6.23",
                Publisher = "RARLAB",
                Version = "6.23",
                InstallLocation = "C:\\Program Files\\WinRAR",
                InstallSource = "C:\\Program Files\\WinRAR\\uninstall.exe",
                InstallDate = new DateTime(2023, 5, 10),
                Size = 25 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files\\WinRAR\\uninstall.exe\"",
                QuietUninstallString = "\"C:\\Program Files\\WinRAR\\uninstall.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Utilities",
                IconPath = "C:\\Program Files\\WinRAR\\WinRAR.exe,0",
                UsageCount = 75,
                LastUsed = DateTime.Now.AddDays(-5),
                CanUninstall = true
            });

            // Security
            programs.Add(new InstalledProgram
            {
                Id = "Kaspersky",
                Name = "Kaspersky Total Security",
                DisplayName = "Kaspersky Total Security",
                Publisher = "Kaspersky Lab",
                Version = "21.15.10.544",
                InstallLocation = "C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15",
                InstallSource = "C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15\\kavremover.exe",
                InstallDate = new DateTime(2023, 7, 1),
                Size = 500 * 1024 * 1024,
                UninstallString = "\"C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15\\kavremover.exe\"",
                QuietUninstallString = "\"C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15\\kavremover.exe\" /S",
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Security",
                IconPath = "C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15\\avp.exe,0",
                UsageCount = 0,
                LastUsed = DateTime.Now.AddHours(-24),
                CanUninstall = true
            });

            // Communication
            programs.Add(new InstalledProgram
            {
                Id = "Discord",
                Name = "Discord",
                DisplayName = "Discord",
                Publisher = "Discord Inc.",
                Version = "1.0.9023",
                InstallLocation = "C:\\Users\\SystemUser\\AppData\\Local\\Discord",
                InstallSource = "C:\\Users\\SystemUser\\AppData\\Local\\Discord\\app-1.0.9023\\Discord.exe",
                InstallDate = new DateTime(2023, 8, 1),
                Size = 200 * 1024 * 1024,
                UninstallString = "\"C:\\Users\\SystemUser\\AppData\\Local\\Discord\\app-1.0.9023\\Discord.exe\" --uninstall",
                QuietUninstallString = null,
                IsSystemComponent = false,
                IsWindowsUpdate = false,
                Category = "Communication",
                IconPath = "C:\\Users\\SystemUser\\AppData\\Local\\Discord\\app-1.0.9023\\Discord.exe,0",
                UsageCount = 300,
                LastUsed = DateTime.Now.AddMinutes(-30),
                CanUninstall = true
            });

            // Add more programs to reach a reasonable count
            for (int i = 0; i < 30; i++)
            {
                var category = categories[_random.Next(categories.Length)];
                var publisher = publishers[_random.Next(publishers.Length)];
                
                programs.Add(new InstalledProgram
                {
                    Id = $"App{i}",
                    Name = $"Application {i + 1}",
                    DisplayName = $"Application {i + 1} Pro",
                    Publisher = publisher,
                    Version = $"{_random.Next(1, 10)}.{_random.Next(0, 99)}",
                    InstallLocation = $@"C:\\Program Files\\{publisher}\\Application {i + 1}",
                    InstallSource = $@"C:\\Program Files\\{publisher}\\Application {i + 1}\\uninstall.exe",
                    InstallDate = DateTime.Now.AddDays(-_random.Next(365)),
                    Size = (long)((_random.NextDouble() * 500) * 1024 * 1024),
                    UninstallString = $@"\"C:\\Program Files\\{publisher}\\Application {i + 1}\\uninstall.exe\"",
                    QuietUninstallString = $@"\"C:\\Program Files\\{publisher}\\Application {i + 1}\\uninstall.exe\" /S",
                    IsSystemComponent = false,
                    IsWindowsUpdate = false,
                    Category = category,
                    IconPath = $@"C:\\Program Files\\{publisher}\\Application {i + 1}\\app.exe,0",
                    UsageCount = _random.Next(0, 500),
                    LastUsed = DateTime.Now.AddDays(-_random.Next(365)),
                    CanUninstall = true
                });
            }

            return programs;
        }

        private static List<StartupProgram> GenerateSampleStartupPrograms()
        {
            var programs = new List<StartupProgram>();
            
            programs.Add(new StartupProgram
            {
                Id = "OneDrive",
                Name = "Microsoft OneDrive",
                Publisher = "Microsoft",
                Command = "\"C:\\Users\\SystemUser\\AppData\\Local\\Microsoft\\OneDrive\\OneDrive.exe\" /background",
                Location = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddHours(-1),
                RunCount = 100
            });

            programs.Add(new StartupProgram
            {
                Id = "Spotify",
                Name = "Spotify",
                Publisher = "Spotify AB",
                Command = "\"C:\\Users\\SystemUser\\AppData\\Roaming\\Spotify\\Spotify.exe\" --autostart",
                Location = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddHours(-2),
                RunCount = 50
            });

            programs.Add(new StartupProgram
            {
                Id = "Steam",
                Name = "Steam",
                Publisher = "Valve",
                Command = "\"C:\\Program Files (x86)\\Steam\\steam.exe\" -silent",
                Location = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddDays(-1),
                RunCount = 200
            });

            programs.Add(new StartupProgram
            {
                Id = "Discord",
                Name = "Discord",
                Publisher = "Discord Inc.",
                Command = "\"C:\\Users\\SystemUser\\AppData\\Local\\Discord\\app-1.0.9023\\Discord.exe\" --start-minimized",
                Location = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddHours(-3),
                RunCount = 150
            });

            programs.Add(new StartupProgram
            {
                Id = "Kaspersky",
                Name = "Kaspersky Total Security",
                Publisher = "Kaspersky Lab",
                Command = "\"C:\\Program Files (x86)\\Kaspersky Lab\\Kaspersky Total Security 21.15\\avp.exe\"",
                Location = "HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddMinutes(-15),
                RunCount = 500
            });

            programs.Add(new StartupProgram
            {
                Id = "NVIDIAGeForceExperience",
                Name = "NVIDIA GeForce Experience",
                Publisher = "NVIDIA",
                Command = "\"C:\\Program Files\\NVIDIA Corporation\\Display.NvContainer\\NVDisplay.Container.exe\" -nvcontainer -id 1",
                Location = "HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddMinutes(-5),
                RunCount = 300
            });

            programs.Add(new StartupProgram
            {
                Id = "WindowsDefender",
                Name = "Windows Defender Notification Icon",
                Publisher = "Microsoft",
                Command = "\"C:\\Program Files\\Windows Defender\\MSASCuiL.exe\"",
                Location = "HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = true,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddMinutes(-10),
                RunCount = 1000
            });

            programs.Add(new StartupProgram
            {
                Id = "Dropbox",
                Name = "Dropbox",
                Publisher = "Dropbox, Inc.",
                Command = "\"C:\\Program Files (x86)\\Dropbox\\Client\\Dropbox.exe\" /systemstartup",
                Location = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                Enabled = false,
                IsWindowsService = false,
                Type = StartupType.Registry,
                LastRunTime = DateTime.Now.AddDays(-30),
                RunCount = 20
            });

            return programs;
        }

        private static List<UninstallHistoryItem> GenerateSampleUninstallHistory()
        {
            var history = new List<UninstallHistoryItem>();
            
            history.Add(new UninstallHistoryItem
            {
                ProgramId = "OldApp1",
                ProgramName = "Old Application 1",
                Version = "1.0.0",
                UninstallDate = DateTime.Now.AddDays(-5),
                WasSuccessful = true,
                WasForced = false,
                SpaceFreed = 500 * 1024 * 1024
            });

            history.Add(new UninstallHistoryItem
            {
                ProgramId = "TempApp",
                ProgramName = "Temporary Application",
                Version = "2.1.0",
                UninstallDate = DateTime.Now.AddDays(-10),
                WasSuccessful = true,
                WasForced = true,
                SpaceFreed = 150 * 1024 * 1024
            });

            history.Add(new UninstallHistoryItem
            {
                ProgramId = "FailedUninstall",
                ProgramName = "Failed Uninstall App",
                Version = "3.0.0",
                UninstallDate = DateTime.Now.AddDays(-15),
                WasSuccessful = false,
                WasForced = false,
                SpaceFreed = 0
            });

            return history;
        }

        private static string GetProgramDescription(string programId)
        {
            var descriptions = new Dictionary<string, string>
            {
                { "Office365", "Microsoft 365 is a subscription service that ensures you always have the most up-to-date modern productivity tools." },
                { "Chrome", "Google Chrome is a fast, secure, and free web browser built for the modern web." },
                { "Firefox", "Mozilla Firefox is a free and open-source web browser developed by the Mozilla Foundation." },
                { "VS2022", "Visual Studio 2022 is the most comprehensive IDE for .NET and C++ developers on Windows." },
                { "VSCode", "Visual Studio Code is a lightweight but powerful source code editor which runs on your desktop and is available for Windows, macOS and Linux." },
                { "Photoshop", "Adobe Photoshop is the industry standard for digital image editing and graphic design." },
                { "NVIDIADriver", "NVIDIA Graphics Driver provides the necessary software to power NVIDIA GPUs." },
                { "Steam", "Steam is the ultimate destination for playing, discussing, and creating games." },
                { "EpicGames", "Epic Games Launcher is the one place to get all the latest and greatest PC games." },
                { "7Zip", "7-Zip is a free and open-source file archiver, a utility used to place groups of files within compressed containers." },
                { "WinRAR", "WinRAR is a powerful archive manager. It can backup your data and reduce the size of email attachments." },
                { "Kaspersky", "Kaspersky Total Security protects your privacy and data on all your devices, and keeps you safe from online threats." },
                { "Discord", "Discord is the easiest way to communicate over voice, video, and text. Chat, hang out, and stay close with your friends and communities." }
            };

            if (descriptions.TryGetValue(programId, out var description))
                return description;
            
            return "No description available for this program.";
        }

        private static string GetPublisherContact(string publisher)
        {
            var contacts = new Dictionary<string, string>
            {
                { "Microsoft", "support@microsoft.com" },
                { "Adobe", "support@adobe.com" },
                { "Google", "support@google.com" },
                { "Mozilla", "support@mozilla.org" },
                { "NVIDIA", "support@nvidia.com" },
                { "Intel", "support@intel.com" },
                { "Apple", "support@apple.com" },
                { "Oracle", "support@oracle.com" },
                { "JetBrains", "support@jetbrains.com" },
                { "Autodesk", "support@autodesk.com" }
            };

            if (contacts.TryGetValue(publisher, out var contact))
                return contact;
            
            return "N/A";
        }

        private static string GetHelpLink(string programName)
        {
            var links = new Dictionary<string, string>
            {
                { "Google Chrome", "https://support.google.com/chrome" },
                { "Mozilla Firefox", "https://support.mozilla.org" },
                { "Microsoft 365", "https://support.office.com" },
                { "Visual Studio 2022", "https://docs.microsoft.com/en-us/visualstudio/" },
                { "Visual Studio Code", "https://code.visualstudio.com/docs" }
            };

            if (links.TryGetValue(programName, out var link))
                return link;
            
            return "https://example.com/support";
        }

        private static string GetAboutUrl(string publisher)
        {
            var urls = new Dictionary<string, string>
            {
                { "Microsoft", "https://www.microsoft.com/about" },
                { "Adobe", "https://www.adobe.com/about-adobe.html" },
                { "Google", "https://about.google/" },
                { "Mozilla", "https://www.mozilla.org/en-US/about/" },
                { "NVIDIA", "https://www.nvidia.com/en-us/about-nvidia/" }
            };

            if (urls.TryGetValue(publisher, out var url))
                return url;
            
            return "https://example.com/about";
        }

        private static string GetUpdateUrl(string programName)
        {
            var urls = new Dictionary<string, string>
            {
                { "Google Chrome", "https://www.google.com/chrome/update/" },
                { "Mozilla Firefox", "https://www.mozilla.org/en-US/firefox/new/" },
                { "Microsoft 365", "https://www.microsoft.com/en-us/software-download/office" }
            };

            if (urls.TryGetValue(programName, out var url))
                return url;
            
            return "https://example.com/update";
        }

        private List<string> GetDependencies(string programId)
        {
            var dependencies = new Dictionary<string, List<string>>
            {
                { "VS2022", new List<string> { ".NET Runtime", "Visual C++ Redistributable", "Windows SDK" } },
                { "VSCode", new List<string> { "Node.js", "Git", "Python" } },
                { "Photoshop", new List<string> { "Adobe Creative Cloud", "Microsoft Visual C++ Redistributable" } }
            };

            if (dependencies.TryGetValue(programId, out var deps))
                return deps;
            
            return new List<string>();
        }

        private List<string> GetFeatures(string programId)
        {
            var features = new Dictionary<string, List<string>>
            {
                { "VS2022", new List<string> { "Code Editor", "Debugger", "Profiler", "IntelliSense", "Git Integration" } },
                { "VSCode", new List<string> { "Lightweight", "Extensions", "Git Integration", "IntelliSense", "Multi-language Support" } },
                { "Photoshop", new List<string> { "Layers", "Filters", "Brushes", "3D Tools", "Content-Aware Fill" } }
            };

            if (features.TryGetValue(programId, out var feats))
                return feats;
            
            return new List<string> { "Basic Features" };
        }

        private List<string> GenerateRandomFileList(string basePath, int count)
        {
            var files = new List<string>();
            var extensions = new[] { ".dll", ".exe", ".tmp", ".log", ".config", ".dat", ".cache" };
            
            for (int i = 0; i < count; i++)
            {
                var subPath = string.Join("\\", GenerateRandomPathSegments(_random.Next(1, 5)));
                var fileName = $"file_{_random.Next(1000)}{extensions[_random.Next(extensions.Length)]}";
                files.Add(System.IO.Path.Combine(basePath, subPath, fileName));
            }
            
            return files;
        }

        private List<string> GenerateRandomPathSegments(int count)
        {
            var segments = new List<string>();
            var names = new[] { "temp", "cache", "data", "backup", "logs", "config", "plugins", "extensions" };
            
            for (int i = 0; i < count; i++)
            {
                segments.Add(names[_random.Next(names.Length)]);
            }
            
            return segments;
        }

        private List<string> GenerateRandomRegistryKeys(string programName, int count)
        {
            var keys = new List<string>();
            var hives = new[] { "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", "HKEY_CLASSES_ROOT" };
            var paths = new[]
            {
                "Software",
                "Software\\Classes",
                "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                "Software\\Microsoft\\Windows\\CurrentVersion\\App Paths",
                "System\\CurrentControlSet\\Services"
            };
            
            for (int i = 0; i < count; i++)
            {
                var hive = hives[_random.Next(hives.Length)];
                var path = string.Join("\\", paths[_random.Next(paths.Length)], programName, _random.Next(1000).ToString());
                var valueName = _random.Next(2) == 0 ? null : $"Value{_random.Next(100)}";
                var value = _random.Next(2) == 0 ? "" : GenerateRandomValue();
                
                keys.Add($"{hive}\\\{path}\\{(valueName == null ? "(Default)" : valueName)} = {value}");
            }
            
            return keys;
        }

        private List<string> GenerateRandomFolders(string basePath, int count)
        {
            var folders = new List<string>();
            var names = new[] { "temp", "cache", "data", "backup", "logs", "config", "plugins", "extensions" };
            
            for (int i = 0; i < count; i++)
            {
                var subPath = string.Join("\\", GenerateRandomPathSegments(_random.Next(1, 3)));
                folders.Add(System.IO.Path.Combine(basePath, subPath));
            }
            
            return folders;
        }

        private string GenerateRandomValue()
        {
            var types = new[] { "string", "dword", "binary" };
            var type = types[_random.Next(types.Length)];
            
            switch (type)
            {
                case "string":
                    return $"\"{Guid.NewGuid().ToString().Substring(0, 8)}\"";
                case "dword":
                    return _random.Next(0, 1000000).ToString();
                case "binary":
                    return Convert.ToHexString(new byte[8]);
                default:
                    return "";
            }
        }

        #endregion
    }
}

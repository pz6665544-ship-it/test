using System;
using System.Collections.Generic;
using SystemHub.Services.Interfaces;

namespace SystemHub.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized) return;

            // Register all services
            RegisterService<INavigationService>(new NavigationService());
            RegisterService<ISystemInfoService>(new SystemInfoService());
            RegisterService<IDiskInfoService>(new DiskInfoService());
            RegisterService<IProcessService>(new ProcessService());
            RegisterService<INetworkService>(new NetworkService());
            RegisterService<IDownloadService>(new DownloadService());
            RegisterService<IUninstallerService>(new UninstallerService());
            RegisterService<IOptimizationService>(new OptimizationService());
            RegisterService<IDataRecoveryService>(new DataRecoveryService());
            RegisterService<ISettingsService>(new SettingsService());

            _isInitialized = true;
        }

        public static void Cleanup()
        {
            foreach (var service in _services.Values)
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _services.Clear();
            _isInitialized = false;
        }

        public static T GetService<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            return default;
        }

        public static void RegisterService<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public static void UnregisterService<T>()
        {
            _services.Remove(typeof(T));
        }
    }
}

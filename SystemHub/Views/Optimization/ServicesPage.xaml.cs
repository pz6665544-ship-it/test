using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SystemHub.Services;
using SystemHub.Services.Interfaces;

namespace SystemHub.Views.Optimization
{
    public partial class ServicesPage : Page
    {
        private readonly INavigationService _navigationService;
        private List<ServiceItem> _services = new();
        private List<ServiceItem> _filteredServices = new();

        public ServicesPage()
        {
            InitializeComponent();
            _navigationService = ServiceLocator.GetService<INavigationService>();
            LoadServices();
        }

        private void LoadServices()
        {
            try
            {
                _services = new List<ServiceItem>
                {
                    new ServiceItem { Id = "1", Name = "AeLookupSvc", DisplayName = "Application Experience", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "2", Name = "ALG", DisplayName = "Application Layer Gateway Service", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "3", Name = "AppIDSvc", DisplayName = "Application Identity", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "4", Name = "Appinfo", DisplayName = "Application Information", Status = "Running", StartupType = "Manual", Pid = "1234", IsSelected = false },
                    new ServiceItem { Id = "5", Name = "Apple Mobile Device Service", DisplayName = "Apple Mobile Device Service", Status = "Stopped", StartupType = "Automatic", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "6", Name = "AppMgmt", DisplayName = "Application Management", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "7", Name = "AppReadiness", DisplayName = "App Readiness", Status = "Running", StartupType = "Manual", Pid = "5678", IsSelected = false },
                    new ServiceItem { Id = "8", Name = "AppVClient", DisplayName = "App-V Client", Status = "Stopped", StartupType = "Disabled", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "9", Name = "AppXSvc", DisplayName = "AppX Deployment Service", Status = "Running", StartupType = "Manual", Pid = "9012", IsSelected = false },
                    new ServiceItem { Id = "10", Name = "AudioEndpointBuilder", DisplayName = "Windows Audio Endpoint Builder", Status = "Running", StartupType = "Automatic", Pid = "3456", IsSelected = false },
                    new ServiceItem { Id = "11", Name = "Audiosrv", DisplayName = "Windows Audio", Status = "Running", StartupType = "Automatic", Pid = "7890", IsSelected = false },
                    new ServiceItem { Id = "12", Name = "AxInstSV", DisplayName = "ActiveX Installer", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "13", Name = "BDESVC", DisplayName = "BitLocker Drive Encryption Service", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "14", Name = "BFE", DisplayName = "Base Filtering Engine", Status = "Running", StartupType = "Automatic", Pid = "1234", IsSelected = false },
                    new ServiceItem { Id = "15", Name = "BITS", DisplayName = "Background Intelligent Transfer Service", Status = "Running", StartupType = "Manual", Pid = "5678", IsSelected = false },
                    new ServiceItem { Id = "16", Name = "BrokerInfrastructure", DisplayName = "Background Intelligent Transfer Service", Status = "Running", StartupType = "Automatic", Pid = "9012", IsSelected = false },
                    new ServiceItem { Id = "17", Name = "Browser", DisplayName = "Computer Browser", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "18", Name = "bthserv", DisplayName = "Bluetooth Support Service", Status = "Stopped", StartupType = "Manual", Pid = "-", IsSelected = false },
                    new ServiceItem { Id = "19", Name = "CDPSvc", DisplayName = "Connected Devices Platform Service", Status = "Running", StartupType = "Automatic", Pid = "3456", IsSelected = false },
                    new ServiceItem { Id = "20", Name = "CDPUserSvc", DisplayName = "Connected Devices Platform User Service", Status = "Running", StartupType = "Manual", Pid = "7890", IsSelected = false }
                };
                
                _filteredServices = new List<ServiceItem>(_services);
                ServicesDataGrid.ItemsSource = _filteredServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedServices = _filteredServices.Where(s => s.IsSelected && s.Status != "Running").ToList();
                
                if (selectedServices.Count == 0)
                {
                    MessageBox.Show("Please select at least one stopped service to start.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var serviceNames = string.Join(", ", selectedServices.Select(s => s.DisplayName).Take(5));
                if (selectedServices.Count > 5)
                    serviceNames += " and " + (selectedServices.Count - 5) + " more";

                var result = MessageBox.Show(
                    $"Are you sure you want to start {selectedServices.Count} selected service(s)?\n\n{serviceNames}",
                    "Confirm Start Services",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var service in selectedServices)
                    {
                        service.Status = "Running";
                        service.Pid = new Random().Next(1000, 9999).ToString();
                    }
                    ServicesDataGrid.Items.Refresh();
                    MessageBox.Show($"Started {selectedServices.Count} service(s).", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedServices = _filteredServices.Where(s => s.IsSelected && s.Status == "Running").ToList();
                
                if (selectedServices.Count == 0)
                {
                    MessageBox.Show("Please select at least one running service to stop.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var serviceNames = string.Join(", ", selectedServices.Select(s => s.DisplayName).Take(5));
                if (selectedServices.Count > 5)
                    serviceNames += " and " + (selectedServices.Count - 5) + " more";

                var result = MessageBox.Show(
                    $"Are you sure you want to stop {selectedServices.Count} selected service(s)?\n\n{serviceNames}\n\nStopping some services may affect system functionality.",
                    "Confirm Stop Services",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var service in selectedServices)
                    {
                        service.Status = "Stopped";
                        service.Pid = "-";
                    }
                    ServicesDataGrid.Items.Refresh();
                    MessageBox.Show($"Stopped {selectedServices.Count} service(s).", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestartSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedServices = _filteredServices.Where(s => s.IsSelected && s.Status == "Running").ToList();
                
                if (selectedServices.Count == 0)
                {
                    MessageBox.Show("Please select at least one running service to restart.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var serviceNames = string.Join(", ", selectedServices.Select(s => s.DisplayName).Take(5));
                if (selectedServices.Count > 5)
                    serviceNames += " and " + (selectedServices.Count - 5) + " more";

                var result = MessageBox.Show(
                    $"Are you sure you want to restart {selectedServices.Count} selected service(s)?\n\n{serviceNames}\n\nThis will temporarily stop and then start the services.",
                    "Confirm Restart Services",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    foreach (var service in selectedServices)
                    {
                        service.Pid = new Random().Next(1000, 9999).ToString();
                    }
                    ServicesDataGrid.Items.Refresh();
                    MessageBox.Show($"Restarted {selectedServices.Count} service(s).", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var serviceId = button.Tag.ToString();
                    var service = _filteredServices.FirstOrDefault(s => s.Id == serviceId);
                    
                    if (service != null && service.Status != "Running")
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to start '{service.DisplayName}'?",
                            "Confirm Start",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            service.Status = "Running";
                            service.Pid = new Random().Next(1000, 9999).ToString();
                            ServicesDataGrid.Items.Refresh();
                            MessageBox.Show($"Service '{service.DisplayName}' started successfully.", 
                                "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var serviceId = button.Tag.ToString();
                    var service = _filteredServices.FirstOrDefault(s => s.Id == serviceId);
                    
                    if (service != null && service.Status == "Running")
                    {
                        var result = MessageBox.Show(
                            $"Are you sure you want to stop '{service.DisplayName}'?\n\nThis may affect system functionality.",
                            "Confirm Stop",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            service.Status = "Stopped";
                            service.Pid = "-";
                            ServicesDataGrid.Items.Refresh();
                            MessageBox.Show($"Service '{service.DisplayName}' stopped successfully.", 
                                "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag != null)
                {
                    var serviceId = button.Tag.ToString();
                    var service = _filteredServices.FirstOrDefault(s => s.Id == serviceId);
                    
                    if (service != null)
                    {
                        var info = $"Service Properties:\n\n";
                        info += $"Name: {service.Name}\n";
                        info += $"Display Name: {service.DisplayName}\n";
                        info += $"Status: {service.Status}\n";
                        info += $"Startup Type: {service.StartupType}\n";
                        info += $"PID: {service.Pid}\n\n";
                        info += "In a full implementation, this would show:\n";
                        info += "- Service description\n";
                        info += "- Path to executable\n";
                        info += "- Dependencies\n";
                        info += "- Recovery options\n";
                        info += "- Log on information";

                        MessageBox.Show(info, $"Properties: {service.DisplayName}", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var statusFilter = StatusFilterComboBox.Text;
                var startupFilter = StartupFilterComboBox.Text;
                var searchText = SearchTextBox.Text.ToLower();

                _filteredServices = _services.Where(s =>
                {
                    // Status filter
                    if (statusFilter != "All Statuses" && s.Status != statusFilter)
                        return false;
                    
                    // Startup type filter
                    if (startupFilter != "All Startup Types" && s.StartupType != startupFilter)
                        return false;
                    
                    // Search filter
                    if (!string.IsNullOrEmpty(searchText) && 
                        !s.Name.ToLower().Contains(searchText) && 
                        !s.DisplayName.ToLower().Contains(searchText))
                        return false;
                    
                    return true;
                }).ToList();

                ServicesDataGrid.ItemsSource = _filteredServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadServices();
                MessageBox.Show("Services list refreshed.", "SystemHub", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _navigationService.NavigateTo("Dashboard_Overview");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ServiceItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string StartupType { get; set; }
        public string Pid { get; set; }
        public bool IsSelected { get; set; }
        
        public System.Windows.Visibility StartButtonVisibility => Status == "Running" ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        public System.Windows.Visibility StopButtonVisibility => Status == "Running" ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
    }
}

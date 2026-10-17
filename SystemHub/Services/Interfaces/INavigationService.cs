using System;
using System.Windows.Controls;

namespace SystemHub.Services.Interfaces
{
    public interface INavigationService
    {
        event EventHandler<NavigationEventArgs> Navigated;
        
        void NavigateTo(string pageName);
        void NavigateTo(Type pageType);
        void NavigateTo(string pageName, object parameter);
        void GoBack();
        void GoForward();
        
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        
        string CurrentPageName { get; }
        object CurrentParameter { get; }
        
        void RegisterPage(string pageName, Type pageType);
        void RegisterPage(string pageName, Type pageType, Type viewModelType);
        
        Page GetCurrentPage();
    }

    public class NavigationEventArgs : EventArgs
    {
        public string PageName { get; set; }
        public object Parameter { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

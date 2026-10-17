using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SystemHub.Services.Interfaces;

namespace SystemHub.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<NavigationEntry> _backStack = new();
        private readonly Stack<NavigationEntry> _forwardStack = new();
        private NavigationEntry _currentEntry;
        private readonly Dictionary<string, PageInfo> _registeredPages = new();

        public event EventHandler<NavigationEventArgs> Navigated;

        public bool CanGoBack => _backStack.Count > 0;
        public bool CanGoForward => _forwardStack.Count > 0;
        public string CurrentPageName => _currentEntry?.PageName;
        public object CurrentParameter => _currentEntry?.Parameter;

        public void RegisterPage(string pageName, Type pageType)
        {
            _registeredPages[pageName] = new PageInfo(pageType, null);
        }

        public void RegisterPage(string pageName, Type pageType, Type viewModelType)
        {
            _registeredPages[pageName] = new PageInfo(pageType, viewModelType);
        }

        public void NavigateTo(string pageName)
        {
            NavigateTo(pageName, null);
        }

        public void NavigateTo(Type pageType)
        {
            var pageName = _registeredPages.FirstOrDefault(kvp => kvp.Value.PageType == pageType).Key;
            if (pageName != null)
            {
                NavigateTo(pageName, null);
            }
        }

        public void NavigateTo(string pageName, object parameter)
        {
            if (!_registeredPages.TryGetValue(pageName, out var pageInfo))
            {
                throw new InvalidOperationException($"Page '{pageName}' is not registered.");
            }

            // Push current to back stack
            if (_currentEntry != null)
            {
                _backStack.Push(_currentEntry);
            }

            // Clear forward stack
            _forwardStack.Clear();

            // Create new entry
            _currentEntry = new NavigationEntry(pageName, parameter);

            // Raise navigation event
            Navigated?.Invoke(this, new NavigationEventArgs
            {
                PageName = pageName,
                Parameter = parameter
            });
        }

        public void GoBack()
        {
            if (!_backStack.Any())
                return;

            // Push current to forward stack
            if (_currentEntry != null)
            {
                _forwardStack.Push(_currentEntry);
            }

            // Pop from back stack
            _currentEntry = _backStack.Pop();

            // Raise navigation event
            Navigated?.Invoke(this, new NavigationEventArgs
            {
                PageName = _currentEntry.PageName,
                Parameter = _currentEntry.Parameter
            });
        }

        public void GoForward()
        {
            if (!_forwardStack.Any())
                return;

            // Push current to back stack
            if (_currentEntry != null)
            {
                _backStack.Push(_currentEntry);
            }

            // Pop from forward stack
            _currentEntry = _forwardStack.Pop();

            // Raise navigation event
            Navigated?.Invoke(this, new NavigationEventArgs
            {
                PageName = _currentEntry.PageName,
                Parameter = _currentEntry.Parameter
            });
        }

        public Page GetCurrentPage()
        {
            if (_currentEntry == null || !_registeredPages.TryGetValue(_currentEntry.PageName, out var pageInfo))
                return null;

            try
            {
                var page = (Page)Activator.CreateInstance(pageInfo.PageType);
                
                // If there's a view model, try to set it as DataContext
                if (pageInfo.ViewModelType != null)
                {
                    var viewModel = Activator.CreateInstance(pageInfo.ViewModelType);
                    page.DataContext = viewModel;
                }
                
                return page;
            }
            catch
            {
                return null;
            }
        }

        private class NavigationEntry
        {
            public string PageName { get; }
            public object Parameter { get; }

            public NavigationEntry(string pageName, object parameter)
            {
                PageName = pageName;
                Parameter = parameter;
            }
        }

        private class PageInfo
        {
            public Type PageType { get; }
            public Type ViewModelType { get; }

            public PageInfo(Type pageType, Type viewModelType)
            {
                PageType = pageType;
                ViewModelType = viewModelType;
            }
        }
    }
}

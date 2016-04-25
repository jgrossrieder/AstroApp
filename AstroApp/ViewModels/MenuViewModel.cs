using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace AstroApp.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private bool _canNavigateToMain = false;
        private bool _canNavigateToSecond = true;

        public MenuViewModel(INavigationService navigationService, IResourceLoader resourceLoader)
        {
            // TODO: Add ability to indicate which page your on by listening for navigation events once the NuGet package has been updated. Change CanNavigate to use whether or not your on that page to return false.
            // As-is, if navigation occurs via the back button, we won't know and can't update the _canNavigate value
            _navigationService = navigationService;

            Commands = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel { DisplayName = resourceLoader.GetString("MainPageMenuItemDisplayName"), FontIcon = "\ue15f", Command = new DelegateCommand(NavigateToMainPage, CanNavigateToMainPage) },
                new MenuItemViewModel { DisplayName = resourceLoader.GetString("SecondPageMenuItemDisplayName"), FontIcon = "\ue15f", Command = new DelegateCommand(NavigateToSecondPage, CanNavigateToSecondPage) },
            };
        }

	    private bool CanNavigateToSecondPage()
	    {
			return _canNavigateToSecond;
		}

	    private void NavigateToSecondPage()
	    {
			if (CanNavigateToSecondPage())
			{
				if (_navigationService.Navigate(PageTokens.ZodiacPage, null))
				{
					_canNavigateToMain = true;
					_canNavigateToSecond = false;
					RaiseCanExecuteChanged();
				}
			}
		}

	    public ObservableCollection<MenuItemViewModel> Commands { get; set; }

        private void NavigateToMainPage()
        {
            if (CanNavigateToMainPage())
            {
                if (_navigationService.Navigate(PageTokens.ZodiacList, null))
                {
                    _canNavigateToMain = false;
                    _canNavigateToSecond = true;
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanNavigateToMainPage()
        {
            return _canNavigateToMain;
        }
		
        private void RaiseCanExecuteChanged()
        {
            foreach (MenuItemViewModel item in Commands)
            {
                (item.Command as DelegateCommand)?.RaiseCanExecuteChanged();
            }
        }
    }
}
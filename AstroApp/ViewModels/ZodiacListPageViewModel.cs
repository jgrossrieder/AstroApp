using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;
using AstroApp.DataRetriever;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using AstroApp.Model;
using Prism.Commands;

namespace AstroApp.ViewModels
{
	public class ZodiacListPageViewModel : AstroViewModel
	{
		private readonly IAstroRetriever _astroRetriever;
		private readonly INavigationService _navigationService;


		public DelegateCommand<Horoscope> DisplayZodiaCommand { get; set; }

		public String Title { get; set; }
		public HoroscopeSet HoroscopeSet
		{
			get { return GetValue<HoroscopeSet>(); }
			set { SetValue(value); }
		}

		public Boolean IsLoading
		{
			get { return GetValue<Boolean>(); }
			set { SetValue(value); }
		}


		public ZodiacListPageViewModel(IAstroRetriever astroRetriever, INavigationService navigationService)
		{
			DisplayZodiaCommand = new DelegateCommand<Horoscope>(NavigateToZodiac);
			_astroRetriever = astroRetriever;
			_navigationService = navigationService;
			Title = "Hello world";
		}

		private void NavigateToZodiac(Horoscope horoscope)
		{
			_navigationService.Navigate(PageTokens.ZodiacPage, horoscope);
		}

		public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);
			if (HoroscopeSet == null)
			{
				LoadHoroscope();
			}
		}

		private void LoadHoroscope()
		{
			IsLoading = true;
			Task.Factory.StartNew(async () =>
			{
				HoroscopeSet = await _astroRetriever.RetrieveHoroscope();
				IsLoading = false;
			});
		}
	}
}

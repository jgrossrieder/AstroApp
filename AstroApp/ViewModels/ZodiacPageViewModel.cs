using System;
using System.Collections.Generic;
using AstroApp.Model;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace AstroApp.ViewModels
{
	public class ZodiacPageViewModel : AstroViewModel
	{
		public Horoscope Horoscope { get { return this.GetValue<Horoscope>(); } set {SetValue(value);} }

		public ZodiacPageViewModel()
		{
		}

		public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			Horoscope horoscope = e.Parameter as Horoscope;
			if (horoscope != null)
			{
				Horoscope = horoscope;
			}
		}
	}
}

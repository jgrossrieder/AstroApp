// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Devices.Perception;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using AstroApp.Helpers;
using Prism.Windows.Mvvm;
using WinUX.Extensions;
using WPFSpark;

namespace AstroApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ZodiacListPage : SessionStateAwarePage
	{
		
		public ZodiacListPage()
		{
			this.InitializeComponent();
			SetUpPageAnimation();
		}

		private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
		}
		private  void SetUpPageAnimation()
		{
			TransitionCollection collection = new TransitionCollection();
			NavigationThemeTransition theme = new NavigationThemeTransition();

			var info = new ContinuumNavigationTransitionInfo();

			theme.DefaultNavigationTransitionInfo = info;
			collection.Add(theme);
			Transitions = collection;
		}
	}
}

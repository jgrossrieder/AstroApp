using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AstroApp.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ZodiacPage : Page
	{
		public ZodiacPage()
		{
			this.InitializeComponent();
			SetUpPageAnimation();
			
		}
		private void SetUpPageAnimation()
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

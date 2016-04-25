using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using AstroApp.DataRetriever;
using AstroApp.DataRetriever.MHDJ;

namespace AstroApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
	{
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
			Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
				Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
				Microsoft.ApplicationInsights.WindowsCollectors.Session);
			this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

		protected override UIElement CreateShell(Frame rootFrame)
		{
			AppShell shell = Container.Resolve<AppShell>();
			shell.SetContentFrame(rootFrame);
			return shell;
		}

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
			Container.RegisterInstance<IAstroRetriever>(new AstroRetriever());
			return base.OnInitializeAsync(args);
		}

		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			NavigationService.Navigate(PageTokens.ZodiacList, null);
			return Task.FromResult(true);
		}
	}
}

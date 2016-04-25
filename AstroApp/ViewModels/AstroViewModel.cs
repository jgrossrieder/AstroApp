using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Prism.Windows.Mvvm;

namespace AstroApp.ViewModels
{
	public abstract class AstroViewModel : ViewModelBase
	{
		private Dictionary<String, object> _backingValues = new Dictionary<string, object>();

		protected T GetValue<T>([CallerMemberName] String propertyName = null)
		{
			object objectOutput;
			if (_backingValues.TryGetValue(propertyName, out objectOutput))
			{
				return (T)objectOutput;
			}
			return default(T);
		}

		protected void SetValue<T>(T value, [CallerMemberName] String propertyName = null)
		{
			object objectOutput;
			if (!_backingValues.TryGetValue(propertyName, out objectOutput) || !((T)objectOutput).Equals(value))
			{
				_backingValues[propertyName] = value;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
				() =>
				{
					OnPropertyChanged(propertyName);
				});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

			}
		}
	}
}

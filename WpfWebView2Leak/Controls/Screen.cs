using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace WpfWebView2Leak.Controls
{
	public class Screen : UserControl, INotifyPropertyChanged
	{
		public virtual void Close()
		{
			Messenger.Default.Send(new NotificationMessage("Close Screen"));
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
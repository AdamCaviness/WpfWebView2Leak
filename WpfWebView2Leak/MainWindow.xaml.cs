using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using WpfWebView2Leak.Controls;
using WpfWebView2Leak.Screens;

namespace WpfWebView2Leak
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private Screen _currentScreen;

		public MainWindow()
		{
			InitializeComponent();

			Messenger.Default.Register<NotificationMessage>(this, OnMessageRecieved);
		}

		private void OnMessageRecieved(NotificationMessage obj)
		{
			if (obj.Notification == "Close Screen")
			{
				CurrentScreen = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}

		private void OnOpenWebScreenClicked(object sender, RoutedEventArgs e)
		{
			CurrentScreen = new WebScreen();
		}

		public Screen CurrentScreen
		{
			get
			{
				return _currentScreen;
			}

			set
			{
				_currentScreen = value;
				OnPropertyChanged();
			}
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
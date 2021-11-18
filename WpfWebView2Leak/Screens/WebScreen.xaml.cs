using System;
using WpfWebView2Leak.Controls;

namespace WpfWebView2Leak.Screens
{
	public partial class WebScreen : Screen
	{
		private string _webViewSource;

		public WebScreen()
		{
			WebViewSource = "https://google.com";

			InitializeComponent();

			WebView.ProcessFailed += OnWebViewProcessFailed;
		}

		private void OnWebViewProcessFailed(object sender, EventArgs e)
		{
			// This is to deal with a known issue where after a computer enters sleep mode,
			// the CoreWebView2 inner component will crash. Suggested solution is to listen
			// for this event and recreate the WebView2 control when this happens.
			// https://github.com/MicrosoftEdge/WebView2Feedback/issues/1344
			WebView.ProcessFailed -= OnWebViewProcessFailed;
			WebView.Cleanup();
			WebView.Dispose();

			var newWebView = new WebView2();
			newWebView.ProcessFailed += OnWebViewProcessFailed;
			WebView = newWebView;
		}
		private void OnCloseButtonClicked(object sender, System.Windows.RoutedEventArgs e)
		{
			WebView.Cleanup();
			WebView.Dispose();

			// Base close method sends a message to MainWindow
			// to null the active screen and ask the GC to collect.
			Close();
		}

		public string WebViewSource {
			get
			{
				return _webViewSource;
			}
			set
			{
				if (_webViewSource == value)
					return;

				_webViewSource = value;
				OnPropertyChanged();
			}
		}
	}
}
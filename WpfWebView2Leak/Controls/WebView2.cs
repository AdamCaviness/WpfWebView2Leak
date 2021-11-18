using System;
using System.Threading.Tasks;

namespace WpfWebView2Leak.Controls
{
	public class WebView2 : Microsoft.Web.WebView2.Wpf.WebView2
	{
		public WebView2() { }

		protected override async void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			await InitializeAsync();
		}

		public async Task InitializeAsync()
		{
			await EnsureCoreWebView2Async();

			if (IsInDesignMode)
				return;

			CoreWebView2.ProcessFailed += CoreWebView2_ProcessFailed;
		}
		public void Cleanup()
		{
			if (CoreWebView2 == null)
				return;

			CoreWebView2.Stop();
			CoreWebView2.ProcessFailed -= CoreWebView2_ProcessFailed;
		}

		private void CoreWebView2_ProcessFailed(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ProcessFailedEventArgs e)
		{
			ProcessFailed?.Invoke(sender, null);
		}

		public event EventHandler ProcessFailed;

	}
}
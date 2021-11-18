# WpfWebView2Leak
## Approach
This sample demonstrates what I believe to be a leak when a WebView2 control is shown in a control that isn't in a dedicated window. In this sample, there is only one window, the MainWindow. There is a UserControl named WebScreen that houses the WebView2 control along with a close button. Clicking the close button sends a message to the MainWindow to null the screen that is being shown and ask the GC to collect.

![Screenshot](https://i.imgur.com/rTqJ6gk.png)

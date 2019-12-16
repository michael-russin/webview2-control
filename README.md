<img src="https://github.com/michael-russin/webview2-interop/blob/master/new-microsoft-edge-icon.png" width="96">

# webview2-control
Window Forms and WFP control for the Microsoft WebView2 web browser control.

When Microsoft revealed their plans for a Chrome based Edge browser the first thing I thought was "I wonder if they are going to support an embedded web browser control"?  Well sure enough they released [WebView2](https://docs.microsoft.com/en-us/microsoft-edge/hosting/webview2) and I've been keeping an eye on the releases.  Recently I found a great [sample](https://github.com/MicrosoftEdge/WebView2Browser) written by [David Risney](https://github.com/david-risney) and I was inspired to take a shot at putting together a C# version. 

This project contains a Windows Forms and a WPF control implementation of the WebView2 C++ contorl .  It's built using the [interop project](https://github.com/michael-russin/webview2-interop) I wrote.  I didn't combine the inerop and window form control into one project since I felt the interop can later be used for a WPF version.   

If you just want to run the Windows Form version of the browser project using nuget packages then you can downloaded it from [webview2-dotnetbrowser](https://github.com/michael-russin/webview2-dotnetbrowser) This project includes the same demo but uses a project reference.  

## Installing / Getting started

* Microsoft Edge (Chromium) installed on a supported OS.
* Visual Stdio 2017 or 2019 with C# and desktop development set up

## Features

* Windows Forms WebView2 control
* Windows Forms Browser demo project
* WPF WebView2 control
* WPF Browser demo project

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Please let me know of any issues you run into.

## Links
- Windows Forms Nuget package: https://www.nuget.org/packages/MtrDev.WebView2.WinForms/
- WPF Nuget package: https://www.nuget.org/packages/MtrDev.WebView2.Wpf/
- Project homepage: https://github.com/michael-russin/webview2-interop
- Repository: https://github.com/michael-russin/webview2-interop
- Issue tracker: https://github.com/michael-russin/webview2-interop/issues
- Related projects:
  - WebView2 Interop: https://github.com/michael-russin/webview2-interop
  - David's awsome C++ sample: https://github.com/MicrosoftEdge/WebView2Browser
  - WebView2 Documentation: https://docs.microsoft.com/en-us/microsoft-edge/hosting/webview2/reference-webview2


## Licensing

The code in this project is licensed under MIT license.

<img src="https://github.com/michael-russin/webview2-interop/blob/master/new-microsoft-edge-icon.png" width="96">

# webview2-control
Window Forms and WFP control for the Microsoft WebView2 web browser control.

When Microsoft revealed their plans for a Chrome based Edge browser the first thing I thought was "I wonder if they are going to support an embedded web browser control"?  Well sure enough they released [WebView2](https://docs.microsoft.com/en-us/microsoft-edge/hosting/webview2) and I've been keeping an eye on the releases.  Recently I found a great [sample](https://github.com/MicrosoftEdge/WebView2Browser) written by [David Risney](https://github.com/david-risney) and I was inspired to take a shot at putting together a C# version. 

This project contains a Windows Forms and a WPF control implementation of the WebView2 C++ contorl .  It's built using the [interop project](https://github.com/michael-russin/webview2-interop) I wrote.    

This project now contains a Wpf and Windows Forms port of the C++ [WebView2 API Sample]( https://github.com/MicrosoftEdge/WebView2Samples/tree/master/WebView2APISample) application.  These samples demonstrate almost all of the different functionality that the WebView2 supports.  Testing the AddRemoteObject/RemoveRemoteObject functionality reqires the building and registering of the dll in the RemoteComObject project.  

I've removed the original browser samples from the main solution but they are still available in the source directory if you want to build them. 

If you just want to run the original Windows Form version of the browser project using nuget packages then you can downloaded it from [webview2-dotnetbrowser](https://github.com/michael-russin/webview2-dotnetbrowser)  

## Installing / Getting started

* Microsoft Edge (Chromium) installed on a supported OS.
* Visual Stdio 2017 or 2019 with C# and desktop development set up

1. open the main solution Src\WebView2.sln
2. Select either the **WebView2.WinForms.Sample** or the **WebView2.Wpf.Sample** as the starup project
3. Check the properties of the **RemoteComObject** project.  You may need to change the _Platform Toolset_ or _Window SDK Version_ to match your environment. 
4. Build and Run.

## Features

* Windows Forms WebView2 control
* Windows Forms API sample project
* WPF WebView2 control
* WPF Browser API sample project

## Links
- Windows Forms Nuget package: https://www.nuget.org/packages/MtrDev.WebView2.WinForms/
- WPF Nuget package: https://www.nuget.org/packages/MtrDev.WebView2.Wpf/
- Project homepage: https://github.com/michael-russin/webview2-interop
- Repository: https://github.com/michael-russin/webview2-interop
- Issue tracker: https://github.com/michael-russin/webview2-interop/issues
- Related projects:
  - WebView2 Interop: https://github.com/michael-russin/webview2-interop
  - David's awsome C++ sample: https://github.com/MicrosoftEdge/WebView2Browser
  - The C++ Web API Sample project: https://github.com/MicrosoftEdge/WebView2Samples
  - WebView2 Documentation: https://docs.microsoft.com/en-us/microsoft-edge/hosting/webview2/reference-webview2


## Licensing

The code in this project is licensed under MIT license.

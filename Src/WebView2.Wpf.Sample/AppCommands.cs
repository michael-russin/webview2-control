using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MtrDev.WebView2.Wpf.Sample
{
    public static class FileCommands
    {
        public static readonly RoutedUICommand SaveScreenshot = new RoutedUICommand
            (
                "Save Screenshot",
                "SaveScreenshot",
                typeof(FileCommands)
            );
        public static readonly RoutedUICommand GetDocumentTitle = new RoutedUICommand
             (
                 "Get Document Title",
                 "GetDocumentTitle",
                 typeof(FileCommands)
             );
        public static readonly RoutedUICommand VersionAfterCreation = new RoutedUICommand
            (
                "Get Browser Version After Creation",
                "VersionAfterCreation",
                typeof(FileCommands)
            );
        public static readonly RoutedUICommand VersionBeforeCreation = new RoutedUICommand
             (
                 "Get Browser Version Before Creation",
                 "VersionBeforeCreation",
                 typeof(FileCommands)
             );

        public static readonly RoutedUICommand Exit = new RoutedUICommand
             (
                 "Exit",
                 "Exit",
                 typeof(FileCommands)
             );
    }

    public static class ScriptCommands
    {
        public static readonly RoutedUICommand InjectScript = new RoutedUICommand
            (
                "Inject Script",
                "InjectScript",
                typeof(ScriptCommands)
            );
        public static readonly RoutedUICommand AddInitializeScript = new RoutedUICommand
             (
                 "Add Initialize Script",
                 "AddInitializeScript",
                 typeof(ScriptCommands)
             );
        public static readonly RoutedUICommand RemoveInitializeScript = new RoutedUICommand
            (
                "Remove Initialize Script",
                "RemoveInitializeScript",
                typeof(ScriptCommands)
            );
        public static readonly RoutedUICommand PostMessageString = new RoutedUICommand
             (
                 "Post Message String",
                 "PostMessageString",
                 typeof(ScriptCommands)
             );

        public static readonly RoutedUICommand PostMessageJSON = new RoutedUICommand
             (
                 "Post Message JSON",
                 "PostMessageJSON",
                 typeof(ScriptCommands)
             );

        public static readonly RoutedUICommand SubscribetoCDPevent = new RoutedUICommand
             (
                 "Subscribe to CDP event",
                 "SubscribetoCDPevent",
                 typeof(ScriptCommands)
             );
        public static readonly RoutedUICommand CallCDPmethod = new RoutedUICommand
            (
                "Call CDP method",
                "CallCDPmethod",
                typeof(ScriptCommands)
            );
        public static readonly RoutedUICommand AddCOMobject = new RoutedUICommand
             (
                 "Add COM object",
                 "AddCOMobject",
                 typeof(ScriptCommands)
             );

        public static readonly RoutedUICommand OpenDevToolsWindow = new RoutedUICommand
             (
                 "Open DevTools Window",
                 "OpenDevToolsWindow",
                 typeof(ScriptCommands)
             );
    }

    public static class WindowCommands
    {
        public static readonly RoutedUICommand CloseWebView= new RoutedUICommand
        (
            "Close WebView",
            "CloseWebView",
            typeof(WindowCommands)
        );

        public static readonly RoutedUICommand CreateWebView = new RoutedUICommand
        (
            "Create WebView",
            "CreateWebView",
            typeof(WindowCommands)
        );

        public static readonly RoutedUICommand CreateNewWindow = new RoutedUICommand
        (
            "Create New Window",
            "CreateNewWindow",
            typeof(WindowCommands)
        );

        public static readonly RoutedUICommand CreateNewThread = new RoutedUICommand
        (
            "Create New Thread",
            "CreateNewThread",
            typeof(WindowCommands)
        );
    }

    public static class ProcessCommands
    {
        public static readonly RoutedUICommand BrowserProcessInfo = new RoutedUICommand
        (
            "Browser Process Info",
            "BrowserProcessInfo",
            typeof(ProcessCommands)
        );

        public static readonly RoutedUICommand CrashBrowserProcess = new RoutedUICommand
        (
            "Crash Browser Process",
            "CrashBrowserProcess",
            typeof(ProcessCommands)
        );
    }

    public static class SettingsCommands
    {
        public static readonly RoutedUICommand BlockedDomains = new RoutedUICommand
        (
            "Blocked Domains",
            "BlockedDomains",
            typeof(SettingsCommands)
        );

        public static readonly RoutedUICommand SetUserAgent = new RoutedUICommand
        (
            "Set User Agent",
            "SetUserAgent",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleJavaScript = new RoutedUICommand
        (
            "Toggle JavaScript",
            "ToggleJavaScript",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleWebMessaging = new RoutedUICommand
        (
            "Toggle Web Messaging",
            "ToggleWebMessaging",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleFullscreenallowed = new RoutedUICommand
        (
            "Toggle Fullscreen allowed",
            "ToggleFullscreenallowed",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleStatusBarenabled = new RoutedUICommand
        (
            "Toggle Status Bar enabled",
            "ToggleStatusBarenabled",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleDevToolsenabled = new RoutedUICommand
        (
            "Toggle DevTools enabled",
            "ToggleDevToolsenabled",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleBlockimages = new RoutedUICommand
        (
            "Toggle Block images",
            "ToggleBlockimages",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand JavaScriptDialogs = new RoutedUICommand
        (
            "JavaScript Dialogs",
            "JavaScriptDialogs",
            typeof(SettingsCommands)
        );

        public static readonly RoutedUICommand UseDefaultScriptDialogs = new RoutedUICommand
        (
            "Use Default Script Dialogs",
            "UseDefaultScriptDialogs",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand UseCustomScriptDialogs = new RoutedUICommand
        (
            "Use Custom Script Dialogs",
            "UseCustomScriptDialogs",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand UseDeferredScriptDialogs = new RoutedUICommand
        (
            "Use Deferred Script Dialogs",
            "UseDeferredScriptDialogs",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand CompleteDeferredScriptDialog = new RoutedUICommand
        (
            "Complete Deferred Script Dialog",
            "CompleteDeferredScriptDialog",
            typeof(SettingsCommands)
        );
        public static readonly RoutedUICommand ToggleContextMenusEnabled = new RoutedUICommand
        (
            "Toggle context menus enabled",
            "Togglecontextmenusenabled",
            typeof(SettingsCommands)
        );
    }

    public static class ViewCommands
    {
        public static readonly RoutedUICommand ToggleVisibility = new RoutedUICommand
        (
            "Toggle Visibility",
            "ToggleVisibility",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand GetWebViewBounds = new RoutedUICommand
        (
            "Get WebView Bounds",
            "GetWebViewBounds",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand Area25Percent = new RoutedUICommand
        (
            "25%",
            "Area25Percent",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand Area50Percent = new RoutedUICommand
        (
            "50%",
            "Area50Percent",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand Area75Percent = new RoutedUICommand
        (
            "75%",
            "Area75Percent",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand Area100Percent = new RoutedUICommand
        (
            "100%",
            "Area100Percent",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand ZoomHalf = new RoutedUICommand
        (
            "0.5x",
            "ZoomHalf",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand ZoomWhole = new RoutedUICommand
        (
            "1.0x",
            "ZoomWhole",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand ZoomTwice = new RoutedUICommand
        (
            "2.0x",
            "ZoomTwice",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand SetFocus = new RoutedUICommand
        (
            "Set Focus",
            "SetFocus",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand TabIn = new RoutedUICommand
        (
            "Tab In",
            "TabIn",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand ReverseTabIn = new RoutedUICommand
        (
            "Reverse Tab In",
            "ReverseTabIn",
            typeof(ViewCommands)
        );
        public static readonly RoutedUICommand ToggleTabHandling = new RoutedUICommand
        (
            "Toggle Tab Handling",
            "ToggleTabHandling",
            typeof(ViewCommands)
        );
    }

    public static class ScenerioCommands
    {
        public static readonly RoutedUICommand WebMessaging = new RoutedUICommand
        (
            "Web Messaging",
            "WebMessaging",
            typeof(ScenerioCommands)
        );
        public static readonly RoutedUICommand RemoteObjects = new RoutedUICommand
        (
            "Remote Objects",
            "RemoteObjects",
            typeof(ScenerioCommands)
        );
        public static readonly RoutedUICommand EventMonitor = new RoutedUICommand
        (
            "Event Monitor",
            "EventMonitor",
            typeof(ScenerioCommands)
        );
    }

    public static class HelpCommands
    {
        public static readonly RoutedUICommand About= new RoutedUICommand
        (
            "About",
            "About",
            typeof(ScenerioCommands)
        );
    }
}

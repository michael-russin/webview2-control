#region License
// Copyright (c) 2019 Michael T. Russin
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

namespace MtrDev.WebView2.Winforms
{
    internal enum HandlerType
    {
        NavigationStarting = 0,
        NavigationComplete = 1,
        ZoomFactorChanged = 2,
        WebMessageReceived = 3,
        DocumentStateChanged = 4,
        LostFocus = 5,
        FrameNavigationStarting = 6,
        MoveFocusRequested = 7,
        GotFocus = 8,
        WebResourceRequested = 9,
        ScriptDialogOpening = 10,
        PermissionRequested = 11,
        ProcessFailed = 12,
        TitleChanged = 13,
        NewWindow = 14,
        AcceleratorKeyPressed = 15
    }
}

using System;

namespace Russinsoft.WinForms
{
    public class ExecuteScriptCompletedEventArgs : EventArgs
    {
        internal ExecuteScriptCompletedEventArgs(int errorCode, string resultObjectAsJson)
        {
            ErrorCode = errorCode;
            ResultAsJson = resultObjectAsJson;
        }

        public int ErrorCode { get; private set; }

        public string ResultAsJson { get; private set; }
    }
}

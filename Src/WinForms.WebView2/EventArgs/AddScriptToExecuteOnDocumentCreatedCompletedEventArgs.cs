using System;

namespace Russinsoft.WinForms
{
    public class AddScriptToExecuteOnDocumentCreatedCompletedEventArgs : EventArgs
    {
        internal AddScriptToExecuteOnDocumentCreatedCompletedEventArgs(int errorCode, string id)
        {
            ErrorCode = errorCode;
            Id = id;
        }

        public int ErrorCode { get; private set; }

        public string Id { get; private set; }
    }
}

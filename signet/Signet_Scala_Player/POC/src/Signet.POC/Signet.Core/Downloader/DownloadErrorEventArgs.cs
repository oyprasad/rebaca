namespace Signet.Core.Downloader
{
    using System;

    public class DownloadErrorEventArgs : DownloadEventArgs
    {
        public Exception Error;

        public DownloadErrorEventArgs(Uri uri, Exception error)
            : base(uri)
        {
            this.Error = error;
        }
    }
}
namespace Signet.Core.Downloader
{
    using System;

    public class DownloadEventArgs : EventArgs
    {
        public Uri DownloadedUrl;

        public DownloadEventArgs(Uri uri)
        {
            this.DownloadedUrl = uri;
        }
    }
}
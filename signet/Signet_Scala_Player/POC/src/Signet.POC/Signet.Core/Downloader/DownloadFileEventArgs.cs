namespace Signet.Core.Downloader
{
    using System;

    public class DownloadFileEventArgs : DownloadEventArgs
    {
        public string Filename;

        public DownloadFileEventArgs(Uri uri, string filename)
            : base(uri)
        {
            this.Filename = filename;
        }
    }
}
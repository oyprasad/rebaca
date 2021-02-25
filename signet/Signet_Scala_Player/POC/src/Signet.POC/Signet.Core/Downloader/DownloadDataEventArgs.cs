namespace Signet.Core.Downloader
{
    using System;

    public class DownloadDataEventArgs : DownloadEventArgs
    {
        public byte[] Content;

        public DownloadDataEventArgs(Uri uri, byte[] data)
            : base(uri)
        {
            this.Content = data;
        }
    }
}
namespace Signet.Core.Downloader
{
    using System;
    using System.IO;

    public class DownloadStreamEventArgs : DownloadEventArgs
    {
        public Stream Content;

        public DownloadStreamEventArgs(Uri uri, Stream data)
            : base(uri)
        {
            this.Content = data;
        }
    }
}
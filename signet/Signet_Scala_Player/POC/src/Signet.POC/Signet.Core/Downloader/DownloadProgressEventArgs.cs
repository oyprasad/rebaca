namespace Signet.Core.Downloader
{
    using System;

    public class DownloadProgressEventArgs : DownloadEventArgs
    {
        public int PercentComplete;

        public DownloadProgressEventArgs(Uri uri, int percentProgress)
            : base(uri)
        {
            this.PercentComplete = percentProgress;
        }
    }
}
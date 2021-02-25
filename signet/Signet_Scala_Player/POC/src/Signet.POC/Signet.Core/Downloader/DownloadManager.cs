namespace Signet.Core.Downloader
{
    using System;
    using System.IO;
    using System.Net;

    public class DownloadManager
    {
        private WebClientPool clientPool;

        public event EventHandler<DownloadEventArgs> DownloadCancelled;

        public event EventHandler<DownloadDataEventArgs> DownloadDataCompleted;

        public event EventHandler<DownloadErrorEventArgs> DownloadError;

        public event EventHandler<DownloadFileEventArgs> DownloadFileCompleted;

        public event EventHandler<DownloadProgressEventArgs> DownloadProgressChanged;

        public event EventHandler<DownloadStreamEventArgs> DownloadStreamCompleted;

        public DownloadManager()
        {
            clientPool = new WebClientPool();
        }

        public DownloadManager(string username, string password)
        {
            clientPool = new WebClientPool(username, password);
        }

        public void DownloadData(string url)
        {
            WebClient client = getClient();
            try
            {
                byte[] content = client.DownloadData(new Uri(url));
                this.OnDownloadDataCompleted(this, new DownloadDataEventArgs(new Uri(url), content));
            }
            catch (Exception ex)
            {
                OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
            }
        }

        public void DownloadDataAsync(string url)
        {
            WebClient client = getClient();

            client.DownloadProgressChanged += (sender, e) =>
            {
                OnDownloadProgressChanged(this, new DownloadProgressEventArgs(new Uri(url), e.ProgressPercentage));
            };

            client.DownloadDataCompleted += (sender, e) =>
            {
                try
                {
                    if (e.Cancelled)
                    {
                        OnDownloadCancelled(this, new DownloadEventArgs(new Uri(url)));
                    }
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    OnDownloadDataCompleted(this, new DownloadDataEventArgs(new Uri(url), e.Result));
                }
                catch (Exception ex)
                {
                    OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
                }
            };
            client.DownloadDataAsync(new Uri(url));
        }

        public void DownloadFile(string url, string filename)
        {
            WebClient client = getClient();
            try
            {
                client.DownloadFile(new Uri(url), filename);
                OnDownloadFileCompleted(this, new DownloadFileEventArgs(new Uri(url), filename));
            }
            catch (Exception ex)
            {
                OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
            }
        }

        public void DownloadFileAsync(string url, string filename)
        {
            WebClient client = getClient();

            client.DownloadProgressChanged += (sender, e) =>
            {
                OnDownloadProgressChanged(this, new DownloadProgressEventArgs(new Uri(url), e.ProgressPercentage));
            };

            client.DownloadFileCompleted += (sender, e) =>
            {
                try
                {
                    if (e.Cancelled)
                    {
                        this.OnDownloadCancelled(this, new DownloadEventArgs(new Uri(url)));
                    }
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    OnDownloadFileCompleted(this, new DownloadFileEventArgs(new Uri(url), filename));
                }
                catch (Exception ex)
                {
                    OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
                }
            };
            client.DownloadFileAsync(new Uri(url), filename);
        }

        public void DownloadStream(string url)
        {
            WebClient client = getClient();
            try
            {
                Stream content = client.OpenRead(new Uri(url));
                OnDownloadStreamCompleted(this, new DownloadStreamEventArgs(new Uri(url), content));
            }
            catch (Exception ex)
            {
                OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
            }
        }

        public void DownloadStreamAsync(string url)
        {
            WebClient client = getClient();

            client.OpenReadCompleted += (sender, e) =>
            {
                try
                {
                    if (e.Cancelled)
                    {
                        OnDownloadCancelled(this, new DownloadEventArgs(new Uri(url)));
                    }
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    OnDownloadStreamCompleted(this, new DownloadStreamEventArgs(new Uri(url), e.Result));
                }
                catch (Exception ex)
                {
                    OnDownloadError(this, new DownloadErrorEventArgs(new Uri(url), ex));
                }
            };
            client.OpenReadAsync(new Uri(url));
        }

        private WebClient getClient()
        {
            WebClient client = clientPool.GetClientInstance();
            clientPool.SetClientBusy(client);
            return client;
        }

        protected virtual void OnDownloadCancelled(object sender, DownloadEventArgs e)
        {
            if (this.DownloadCancelled != null)
            {
                DownloadCancelled(sender, e);
            }
        }

        protected virtual void OnDownloadDataCompleted(object sender, DownloadDataEventArgs e)
        {
            if (DownloadDataCompleted != null)
            {
                DownloadDataCompleted(sender, e);
            }
        }

        protected virtual void OnDownloadError(object sender, DownloadErrorEventArgs e)
        {
            if (DownloadError != null)
            {
                DownloadError(sender, e);
            }
        }

        protected virtual void OnDownloadFileCompleted(object sender, DownloadFileEventArgs e)
        {
            if (DownloadFileCompleted != null)
            {
                DownloadFileCompleted(sender, e);
            }
        }

        protected virtual void OnDownloadProgressChanged(object sender, DownloadProgressEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(sender, e);
            }
        }

        protected virtual void OnDownloadStreamCompleted(object sender, DownloadStreamEventArgs e)
        {
            if (DownloadStreamCompleted != null)
            {
                DownloadStreamCompleted(sender, e);
            }
        }
    }
}
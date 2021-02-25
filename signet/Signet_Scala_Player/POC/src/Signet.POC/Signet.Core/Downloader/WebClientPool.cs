namespace Signet.Core.Downloader
{
    using System.Collections.Generic;
    using System.Net;

    public class WebClientPool
    {
        private NetworkCredential authCredential;
        private List<WebClient> clients;
        private object syncRoot;

        public WebClientPool()
            : this(0, string.Empty, string.Empty)
        {
        }

        public WebClientPool(int size)
            : this(size, string.Empty, string.Empty)
        {
        }

        public WebClientPool(string username, string password)
            : this(0, username, password)
        {
        }

        public WebClientPool(int size, string username, string password)
        {
            this.clients = new List<WebClient>();
            this.syncRoot = new object();
            this.authCredential = null;
            
            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                this.authCredential = new NetworkCredential(username, password);
            }
            
            lock (this.syncRoot)
            {
                for (int i = 0; i < size; i++)
                {
                    this.clients[i] = this.InitializeWebClient();
                }
            }
        }

        public WebClient GetClientInstance()
        {
            lock (this.syncRoot)
            {
                if ((this.clients.Count == 0) || this.clients[0].IsBusy)
                {
                    this.clients.Insert(0, this.InitializeWebClient());
                }
                return this.clients[0];
            }
        }

        private WebClient InitializeWebClient()
        {
            WebClient client = new WebClient();

            client.DownloadDataCompleted += (sender, e) =>
            {
                this.ReactivateWebClient(sender as WebClient);
            };
            
            if (this.authCredential != null)
            {
                client.Credentials = this.authCredential;
            }
            return client;
        }

        private void ReactivateWebClient(WebClient client)
        {
            if (!client.IsBusy)
            {
                lock (this.syncRoot)
                {
                    this.clients.Remove(client);
                    this.clients.Insert(0, client);
                }
            }
        }

        public void SetClientBusy(WebClient client)
        {
            lock (this.syncRoot)
            {
                if (!client.IsBusy)
                {
                    this.clients.Insert(this.clients.Count - 1, client);
                }
            }
        }
    }
}
namespace Signet.CM.DomainModel
{
    using System;
    using System.Collections;
    using Signet.Core.DomainObjects;

    public class MediaEntity : IMediaEntity
    {
        public ApprovalStatus? ApprovalStatus { get; set; }

        public string Description { get; set; }

        public string DownloadPath { get; set; }

        public long? DurationInMS { get; set; }

        public DateTime? EndValidDate { get; set; }

        public long? Height { get; set; }

        public int Id { get; set; }

        public bool? IsNonscheduled { get; set; }

        public DateTime? LastModified { get; set; }

        public long? Length { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int? RevisionNumber { get; set; }

        public DateTime? StartValidDate { get; set; }

        public MediaType? Type { get; set; }

        public int? Volume { get; set; }

        public string WebDavDownloadPath
        {
            get
            {
                return this.getWebDavContentUrl();
            }
        }

        public long? Width { get; set; }

        private string getWebDavContentUrl()
        {
            string webDavUrl = string.Empty;
            if (string.IsNullOrEmpty(this.DownloadPath) || !this.DownloadPath.Contains("/"))
            {
                return webDavUrl;
            }
            string[] parts = this.DownloadPath.Split(new char[] { '/' });
            ArrayList partsList = new ArrayList(parts.Length + 1);
            partsList.AddRange(parts);
            if (partsList.Count > 2)
            {
                partsList.Insert(2, "webdav");
            }
            return string.Join("/", partsList.ToArray());
        }
    }
}
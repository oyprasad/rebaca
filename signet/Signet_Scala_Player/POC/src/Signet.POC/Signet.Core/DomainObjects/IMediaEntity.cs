namespace Signet.Core.DomainObjects
{
    using System;

    public interface IMediaEntity : IEntity
    {
        ApprovalStatus? ApprovalStatus { get; set; }

        string Description { get; set; }

        string DownloadPath { get; set; }

        long? DurationInMS { get; set; }

        DateTime? EndValidDate { get; set; }

        long? Height { get; set; }

        bool? IsNonscheduled { get; set; }

        DateTime? LastModified { get; set; }

        long? Length { get; set; }

        string Name { get; set; }

        string Path { get; set; }

        int? RevisionNumber { get; set; }

        DateTime? StartValidDate { get; set; }

        MediaType? Type { get; set; }

        int? Volume { get; set; }

        string WebDavDownloadPath { get; }

        long? Width { get; set; }
    }
}
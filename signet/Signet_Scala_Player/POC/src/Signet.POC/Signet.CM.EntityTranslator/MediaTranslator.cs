namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Media;
    using Signet.Core.DomainObjects;

    public class MediaTranslator : EntityMapperTranslator<IMediaEntity, mediaTO>
    {
        protected override mediaTO BusinessToService(IMediaEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IMediaEntity ServiceToBusiness(mediaTO value)
        {
            return new MediaEntity
            {
                Id = value.id,
                Name = value.name,
                Description = value.description,
                Type = new MediaType?(value.mediaType.ToEntityEnum()),
                RevisionNumber = new int?(value.revision),
                IsNonscheduled = new bool?(value.nonScheduled),
                StartValidDate = new DateTime?(value.startValidDate),
                EndValidDate = new DateTime?(value.endValidDate),
                Volume = new int?(value.volume),
                DownloadPath = value.downloadPath,
                Path = value.path,
                LastModified = new DateTime?(value.lastModified),
                Length = new long?(value.length),
                DurationInMS = new long?(value.duration),
                Width = new long?(value.width),
                Height = new long?(value.height),
                ApprovalStatus = new ApprovalStatus?(value.approvalStatus.ToEntityEnum())
            };
        }
    }
}
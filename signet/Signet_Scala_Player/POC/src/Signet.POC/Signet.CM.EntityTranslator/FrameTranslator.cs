namespace Signet.CM.EntityTranslator
{
    using System;
    using System.Drawing;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Channel;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;

    public class FrameTranslator : EntityMapperTranslator<IFrameEntity, frameTO>
    {
        protected override frameTO BusinessToService(IFrameEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IFrameEntity ServiceToBusiness(frameTO value)
        {
            return new FrameEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                Position = new Point(value.x, value.y), 
                Size = new Size(value.width, value.height), 
                SortOrder = value.sortOrder, 
                Color = value.color.IsValidColorCode() ? new Color?(ColorTranslator.FromHtml(value.color)) : null, 
                ScaleMode = value.autoscale.ToEntityEnum(), 
                IsAudio = new bool?(value.audioTrack), 
                IsCharted = new bool?(value.isCharted) 
            };
        }
    }
}
namespace Signet.CM.EntityTranslator
{
    using System;
    using System.Drawing;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Channel;
    using Signet.Core.DomainObjects;

    public class FramesetTranslator : EntityMapperTranslator<IFramesetEntity, framesetTO>
    {
        protected override framesetTO BusinessToService(IFramesetEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IFramesetEntity ServiceToBusiness(framesetTO value)
        {
            return new FramesetEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                ScreenSize = new Size(value.screenWidth, value.screenHeight), 
                IsDefaulSet = new bool?(value.defaultSet) 
            };
        }
    }
}
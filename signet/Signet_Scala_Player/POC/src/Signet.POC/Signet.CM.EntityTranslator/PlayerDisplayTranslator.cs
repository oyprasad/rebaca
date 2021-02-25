namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Player;
    using Signet.Core.DomainObjects;

    public class PlayerDisplayTranslator : EntityMapperTranslator<IPlayerDisplayEntity, playerDisplayTO>
    {
        protected override playerDisplayTO BusinessToService(IPlayerDisplayEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IPlayerDisplayEntity ServiceToBusiness(playerDisplayTO value)
        {
            return new PlayerDisplayEntity 
            { 
                Id = value.id, 
                Description = value.description, 
                ScreenCounter = new int?(value.screenCounter), 
                ChannelId = new int?(value.channelId) 
            };
        }
    }
}
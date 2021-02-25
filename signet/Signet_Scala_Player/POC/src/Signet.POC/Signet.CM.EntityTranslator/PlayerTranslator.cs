namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.CM.DomainModel;
    using Signet.CM.Service.Stubs.Player;
    using Signet.Core.DomainObjects;

    public class PlayerTranslator : EntityMapperTranslator<IPlayerEntity, playerTO>
    {
        protected override playerTO BusinessToService(IPlayerEntity value)
        {
            throw new NotImplementedException();
        }

        protected override IPlayerEntity ServiceToBusiness(playerTO value)
        {
            return new PlayerEntity 
            { 
                Id = value.id, 
                Name = value.name, 
                Description = value.description, 
                UniqueId = value.uuid, 
                IsPreviewPlayer = new bool?(value.previewPlayer), 
                IsEnabled = new bool?(value.enabled), 
                Mac = value.mac, 
                Type = new PlayerType?(value.playerType.ToEntityEnum()) 
            };
        }
    }
}
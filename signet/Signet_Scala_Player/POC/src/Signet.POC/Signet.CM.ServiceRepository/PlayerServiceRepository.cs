namespace Signet.CM.ServiceRepository
{
    using System;
    using System.Collections.Generic;
    using Signet.CM.Service.Stubs.Player;
    using Signet.Core.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Translators;

    public abstract class PlayerServiceRepository : BaseServiceRepository, IPlayerServiceRepository
    {
        protected PlayerServiceRepository()
        {
            base.serviceInstance = new playerService();
        }

        protected override void ConfigureEndpoint(IServiceConnectionConfiguration config)
        {
            base.serviceInstance.Url = config.PlayerlEndpointUrl;
        }

        public IList<IPlayerDisplayEntity> GetAllPlayerDisplays(int playerId)
        {
            IEnumerable<playerDisplayTO> playerDisplayArray = null;
            IList<IPlayerDisplayEntity> playerDisplays = new List<IPlayerDisplayEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    playerDisplayArray = this.GetAllPlayerDisplaysImpl(playerId);
                });
                playerDisplayArray.ForEach<playerDisplayTO>(delegate(playerDisplayTO p)
                {
                    playerDisplays.Add(this.GetTranslator<IPlayerDisplayEntity, playerDisplayTO>().Translate<IPlayerDisplayEntity>(p));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return playerDisplays;
        }

        protected abstract playerDisplayTO[] GetAllPlayerDisplaysImpl(int playerId);

        public IList<IPlayerEntity> GetAllPlayers(int maxResults = 0)
        {
            IEnumerable<playerTO> playerArray = null;
            IList<IPlayerEntity> players = new List<IPlayerEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    playerArray = this.GetAllPlayersImpl(maxResults);
                });
                playerArray.ForEach<playerTO>(delegate(playerTO p)
                {
                    players.Add(this.GetTranslator<IPlayerEntity, playerTO>().Translate<IPlayerEntity>(p));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return players;
        }

        protected abstract playerTO[] GetAllPlayersImpl(int maxResults);

        protected playerService getService()
        {
            if (base.serviceInstance == null)
            {
                throw new InvalidOperationException("CM Player Service instance not created");
            }
            return (base.serviceInstance as playerService);
        }

        protected override void InitTranslators()
        {
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Player"));
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("PlayerDisplay"));
        }
    }
}
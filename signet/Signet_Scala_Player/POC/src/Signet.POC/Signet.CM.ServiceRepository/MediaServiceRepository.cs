namespace Signet.CM.ServiceRepository
{
    using System;
    using System.Collections.Generic;
    using Signet.CM.Service.Stubs.Media;
    using Signet.Core.Configuration;
    using Signet.Core.Dependency;
    using Signet.Core.DomainObjects;
    using Signet.Core.Extensions;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Translators;

    public abstract class MediaServiceRepository : BaseServiceRepository, IMediaServiceRepository
    {
        protected MediaServiceRepository()
        {
            base.serviceInstance = new mediaService();
        }

        protected override void ConfigureEndpoint(IServiceConnectionConfiguration config)
        {
            base.serviceInstance.Url = config.MediaEndpointUrl;
        }

        public IList<IMediaEntity> GetAllMedia(int maxResults = 0)
        {
            mediaTO[] mediaArray = null;
            IList<IMediaEntity> medias = new List<IMediaEntity>();
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    mediaArray = this.GetAllMediaImpl(maxResults);
                });
                mediaArray.ForEach<mediaTO>(delegate(mediaTO c)
                {
                    medias.Add(this.GetTranslator<IMediaEntity, mediaTO>().Translate<IMediaEntity>(c));
                });
            }
            finally
            {
                base.EndProfile();
            }
            return medias;
        }

        protected abstract mediaTO[] GetAllMediaImpl(int maxResults);

        public IMediaEntity GetMediaById(int mediaId)
        {
            mediaTO mediaTO = null;
            IMediaEntity media = null;
            base.BeginProfile();
            try
            {
                base.Execute(delegate
                {
                    mediaTO = this.GetMediaByIdImpl(mediaId);
                });
                media = base.GetTranslator<IMediaEntity, mediaTO>().Translate<IMediaEntity>(mediaTO);
            }
            finally
            {
                base.EndProfile();
            }
            return media;
        }

        protected abstract mediaTO GetMediaByIdImpl(int mediaId);

        protected mediaService getService()
        {
            if (base.serviceInstance == null)
            {
                throw new InvalidOperationException("CM Media Service instance not created");
            }
            return (base.serviceInstance as mediaService);
        }

        protected override void InitTranslators()
        {
            base.RegisterTranslator(IoC.Resolve<IEntityTranslator>("Media"));
        }
    }
}
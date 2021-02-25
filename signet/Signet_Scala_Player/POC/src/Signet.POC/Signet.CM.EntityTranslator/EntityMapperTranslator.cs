namespace Signet.CM.EntityTranslator
{
    using System;

    public abstract class EntityMapperTranslator<TBusinessEntity, TServiceEntity> : BaseTranslator
    {
        protected EntityMapperTranslator()
        {
        }

        protected abstract TServiceEntity BusinessToService(TBusinessEntity value);

        public override bool CanTranslate(Type targetType, Type sourceType)
        {
            return (((targetType == typeof(TBusinessEntity)) && (sourceType == typeof(TServiceEntity))) || ((targetType == typeof(TServiceEntity)) && (sourceType == typeof(TBusinessEntity))));
        }

        protected abstract TBusinessEntity ServiceToBusiness(TServiceEntity value);

        public override object Translate(Type targetType, object source)
        {
            if (targetType == typeof(TBusinessEntity))
            {
                return this.ServiceToBusiness((TServiceEntity)source);
            }
            if (targetType != typeof(TServiceEntity))
            {
                throw new InvalidOperationException();
            }
            return this.BusinessToService((TBusinessEntity)source);
        }
    }
}
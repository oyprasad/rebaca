namespace Signet.CM.EntityTranslator
{
    using System;
    using Signet.Core.Translators;

    public abstract class BaseTranslator : IEntityTranslator
    {
        protected BaseTranslator()
        {
        }

        public bool CanTranslate<TTarget, TSource>()
            where TTarget : class
            where TSource : class
        {
            return this.CanTranslate(typeof(TTarget), typeof(TSource));
        }

        public abstract bool CanTranslate(Type targetType, Type sourceType);

        public TTarget Translate<TTarget>(object source) where TTarget : class
        {
            return ((source != null) ? ((TTarget)this.Translate(typeof(TTarget), source)) : default(TTarget));
        }

        public abstract object Translate(Type targetType, object source);
    }
}
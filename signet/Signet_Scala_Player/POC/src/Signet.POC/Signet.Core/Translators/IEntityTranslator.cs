namespace Signet.Core.Translators
{
    using System;

    public interface IEntityTranslator
    {
        bool CanTranslate<TTarget, TSource>()
            where TTarget : class
            where TSource : class;

        bool CanTranslate(Type targetType, Type sourceType);

        TTarget Translate<TTarget>(object source) where TTarget : class;

        object Translate(Type targetType, object source);
    }
}
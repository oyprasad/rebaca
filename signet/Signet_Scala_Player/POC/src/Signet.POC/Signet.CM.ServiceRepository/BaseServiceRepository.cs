namespace Signet.CM.ServiceRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Services.Protocols;
    using Signet.Core.Configuration;
    using Signet.Core.Extensions;
    using Signet.Core.Logging;
    using Signet.Core.ServiceRepository;
    using Signet.Core.Translators;
    using Signet.Core.Utils;

    public abstract class BaseServiceRepository : IServiceRepository
    {
        private readonly int _baseStackFrameOffsetForTrace =
#if DEBUG
            3;
#else
            2;
#endif

        private readonly int _baseStackFrameOffsetForBenchmark = 2;

        private List<IEntityTranslator> _translators = new List<IEntityTranslator>();
        protected SoapHttpClientProtocol serviceInstance;

        public BaseServiceRepository()
        {
            this.InitTranslators();
        }

        protected void BeginProfile()
        {
            this.Trace(TraceTypeConstant.Start);
        }

        public void Configure(IServiceConnectionConfiguration config)
        {
            ICredentials credential = new NetworkCredential(config.Username, config.Password);
            this.serviceInstance.Credentials = credential;
            this.serviceInstance.PreAuthenticate = true;
            this.ConfigureEndpoint(config);
        }

        protected abstract void ConfigureEndpoint(IServiceConnectionConfiguration config);

        protected void EndProfile()
        {
            this.Trace(TraceTypeConstant.End);
        }

        protected void Execute(Action action)
        {
            try
            {
                try
                {
                    TimeSpan time = action.Benchmark();
                    Logger.Info(string.Format("{0} took {1} to complete", ReflectionHelper.GetComponentCallDetails(_baseStackFrameOffsetForBenchmark).ToString(), time.ToString(@"hh\:mm\:ss\.ff")));
                }
                catch (Exception ex)
                {
                    Logger.Error(string.Format("Error occured calling {0}", ReflectionHelper.GetComponentCallDetails(_baseStackFrameOffsetForBenchmark).ToString()), ex);
                    throw;
                }
            }
            finally
            {
            }
        }

        public IEntityTranslator GetTranslator<TTarget, TSource>()
        {
            return (from t in this._translators
                    where t.CanTranslate(typeof(TTarget), typeof(TSource))
                    select t).Single<IEntityTranslator>();
        }

        protected abstract void InitTranslators();

        protected void RegisterTranslator(IEntityTranslator translator)
        {
            if (!this._translators.Contains(translator))
            {
                this._translators.Add(translator);
            }
        }

        private void Trace(TraceTypeConstant traceConstant)
        {
            string messageToLog = "{0} method: {1}";
            string methodInfo = ReflectionHelper.GetComponentCallDetails(_baseStackFrameOffsetForTrace).ToString();
            switch (traceConstant)
            {
                case TraceTypeConstant.Start:
                    messageToLog = string.Format(messageToLog, "Entering", methodInfo);
                    break;

                case TraceTypeConstant.End:
                    messageToLog = string.Format(messageToLog, "Leaving", methodInfo);
                    break;

                default:
                    messageToLog = "Invalid TraceType specified";
                    break;
            }
            Logger.Info(messageToLog);
        }
    }
}
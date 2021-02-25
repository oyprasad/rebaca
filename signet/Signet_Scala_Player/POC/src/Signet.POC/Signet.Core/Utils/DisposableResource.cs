namespace Signet.Core.Utils
{
    using System;

    public abstract class DisposableResource : IDisposable
    {
        protected DisposableResource()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~DisposableResource()
        {
            this.Dispose(false);
        }
    }
}
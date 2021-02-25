using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Signet.Core.Extensions;
using Signet.Core.Task;
using Signet.Core.Utils;
using Signet.Core.Logging;

namespace Signet.CM.Tasks
{
    public sealed class CreateVirtualDirectory : IStartupTask
    {
        private readonly string VDirName = ConfigurationManager.AppSettings["VDirName"];
        private readonly string folderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Content");

        public void Execute()
        {
            try
            {
                if (IISManager.CreateVirtualDirectory("localhost", VDirName, folderPath))
                    ConsoleLogger.Log(LogLevel.Info, "Created virtual directory for app: {0} on server: {1} at path: {2}".FormatWith(VDirName, "localhost", folderPath));
            }
            catch (Exception ex)
            {
                throw new Exception("Could not create virtual directory for app: {0} on server: {1} at path: {2}".FormatWith(VDirName, "localhost", folderPath), ex);
            }
        }
    }
}

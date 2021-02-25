using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;

namespace Signet.Core.Utils
{
    public static class IISManager
    {
        public static bool CreateVirtualDirectory(string serverName, string appName, string path)
        {
            var schema = new DirectoryEntry("IIS://" + serverName + "/Schema/AppIsolated");
            bool canCreate = !(schema.Properties["Syntax"].Value.ToString().ToUpper() == "BOOLEAN");
            schema.Dispose();

            if (canCreate)
            {
                bool pathCreated = false;
                try
                {
                    var admin = new DirectoryEntry("IIS://" + serverName + "/W3SVC/1/Root");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        pathCreated = true;
                    }

                    IEnumerable<DirectoryEntry> matchingEntries = admin.Children.Cast<DirectoryEntry>().Where(v => v.Name == appName);
                    foreach (DirectoryEntry vd in matchingEntries)
                    {
                        admin.Invoke("Delete", new[] { vd.SchemaClassName, appName });
                        admin.CommitChanges();
                        break;
                    }

                    DirectoryEntry vdir = admin.Children.Add(appName, "IIsWebVirtualDir");

                    vdir.Properties["Path"][0] = path;
                    vdir.Properties["AppFriendlyName"][0] = appName;
                    vdir.Properties["EnableDirBrowsing"][0] = false;
                    vdir.Properties["AccessRead"][0] = true;
                    vdir.Properties["AccessExecute"][0] = true;
                    vdir.Properties["AccessWrite"][0] = false;
                    vdir.Properties["AccessScript"][0] = true;
                    vdir.Properties["AuthNTLM"][0] = true;
                    vdir.CommitChanges();

                    vdir.Invoke("AppCreate", true);

                    return true;
                }
                catch (Exception)
                {
                    if (pathCreated)
                        Directory.Delete(path);
                    throw;
                }
            }
            return false;
        }
    }
}
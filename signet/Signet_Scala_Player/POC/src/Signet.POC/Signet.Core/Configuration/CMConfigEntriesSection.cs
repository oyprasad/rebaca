namespace Signet.Core.Configuration
{
    using System.Configuration;

    public class CMConfigEntriesSection : ConfigurationSection
    {
        public static CMConfigEntriesSection CurrentConfigurations()
        {
            return (CMConfigEntriesSection)ConfigurationManager.GetSection("Signet.CM.Service");
        }

        [ConfigurationProperty("CMConfigEntries"), ConfigurationCollection(typeof(CMConfigEntryCollection), AddItemName = "CMConfigEntry")]
        public CMConfigEntryCollection ConfigEntries
        {
            get
            {
                return (CMConfigEntryCollection)base["CMConfigEntries"];
            }
        }
    }
}
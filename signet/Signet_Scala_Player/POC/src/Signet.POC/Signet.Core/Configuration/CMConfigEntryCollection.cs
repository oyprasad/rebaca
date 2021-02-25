namespace Signet.Core.Configuration
{
    using System.Configuration;

    public class CMConfigEntryCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CMConfigEntry();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CMConfigEntry)element).Environment;
        }

        public CMConfigEntry this[int index]
        {
            get
            {
                return (CMConfigEntry)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }
    }
}
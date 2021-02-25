namespace Signet.Core.Utils
{
    public class ComponentCallDetails
    {
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", this.Namespace, this.Classname, this.MethodSignature);
        }

        public string Classname { get; set; }

        public string MethodSignature { get; set; }

        public string Namespace { get; set; }
    }
}
namespace CloudSync
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class FileServerAttribute : Attribute
    {
        public string Identifier { get; }

        public FileServerAttribute(string identifier)
        {
            Identifier = identifier;
        }
    }
}
namespace CloudSync
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DatabaseAttribute : Attribute
    {
        public string Identifier { get; }

        public DatabaseAttribute(string identifier)
        {
            Identifier = identifier;
        }
    }
}
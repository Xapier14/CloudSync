namespace CloudSync
{
    public interface ISharedDataStore
    {
        public void SetItem(string key, object value, object? owner = null);
        public object GetItem(string key);
        public T GetItem<T>(string key);
        public object this[string key] { get; set; }
        public object this[string key, object owner] { get; set; }
    }
}

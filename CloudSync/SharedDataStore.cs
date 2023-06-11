using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
    public class SharedDataStore
    {
        private readonly Dictionary<string, object?> _ownership = new();
        private readonly Dictionary<string, object> _data = new();

        public void SetItem(string key, object value, object? owner = null)
        {
            _ownership.TryGetValue(key, out var itemOwner);
            if (_ownership == null || itemOwner != owner)
                throw new InvalidOperationException("Item cannot be set as the key is owned by another object.");
            _data[key] = value;
            _ownership[key] = owner;
        }

        public object GetItem(string key)
            => _data[key];

        public T GetItem<T>(string key)
            => (T)_data[key];

        public object this[string key]
        {
            get => GetItem(key);
            set => SetItem(key, value);
        }

        public object this[string key, object owner]
        {
            get => GetItem(key);
            set => SetItem(key, value, owner);
        }
    }
}

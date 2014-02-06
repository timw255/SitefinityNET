using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.ServiceWrappers.ContentServices
{
    public abstract class CustomizableFields
    {
        [JsonExtensionData]
        private Dictionary<string, object> _customFields = new Dictionary<string, object>();

        private Dictionary<string, object> CustomFields
        {
            get
            {
                if (_customFields == null)
                    _customFields = new Dictionary<string, object>();
                return _customFields;
            }
        }

        public TValue GetValue<TValue>(string name)
        {
            TValue property = GetProperty<TValue>(name);
            if (property != null)
                return property;
            return default(TValue);
        }

        public void SetValue<TValue>(string name, TValue value)
        {
            TValue property = GetProperty<TValue>(name);
            if (property != null)
                property = value;
        }

        public bool HasProperty(string name)
        {
            return _customFields == null ? false : CustomFields.ContainsKey(name);
        }

        public void AddProperty<TValue>(string name, TValue property)
        {
            if (HasProperty(name))
                throw new InvalidOperationException(string.Format("Type already contains a property with the name '{0}'", name));

            CustomFields.Add(name, property);
        }

        public void RemoveProperty(string name)
        {
            CustomFields.Remove(name);
        }

        public TValue GetProperty<TValue>(string name)
        {
            if (!HasProperty(name))
                throw new InvalidOperationException(string.Format("Type does not contain property with the name '{0}'", name));

            TValue property = (TValue)CustomFields[name];
            if (property == null)
                throw new InvalidOperationException(string.Format("Invalid type specified", name));

            return property;
        }
    }
}
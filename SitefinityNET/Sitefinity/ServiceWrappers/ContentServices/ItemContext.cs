using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.ServiceWrappers.ContentServices
{
    class ItemContext<T>
        where T : new()
    {
        public T Item { get; set; }

        public ItemContext()
        {
            this.Item = new T();
        }
    }
}
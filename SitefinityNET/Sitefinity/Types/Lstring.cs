using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class Lstring
    {
        public string PersistedValue { get; set; }

        public string Value { get; set; }

        public Lstring()
        {
            this.PersistedValue = "";
            this.Value = "";
        }
    }
}
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SitefinityNET
{
    public static class Utility
    {
        public enum ContentFilter
        {
            TitleEquals = 1,
            TitleLike = 2,
            IdEquals = 3
        }

        public static Dictionary<ContentFilter, string> FilterStrings = new Dictionary<ContentFilter, string>()
        {
            { ContentFilter.TitleEquals, "Title.Equals(\"{0}\")" },
            { ContentFilter.TitleLike, "Title.ToUpper().Contains(\"{0}\".ToUpper())" },
            { ContentFilter.IdEquals, "Id.Equals(\"{0}\")" }
        };

        public static string BuildFilterString(Dictionary<ContentFilter, string> filters)
        {
            string filterString = "";
            if (filters != null)
            {
                foreach (KeyValuePair<ContentFilter, string> f in filters)
                {
                    filterString += String.Format(Utility.FilterStrings[f.Key], f.Value);
                }
            }
            return filterString;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
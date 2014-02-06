using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public class RatingResult
    {
        public decimal Average { get; set; }

        public string SubtitleMessage { get; set; }

        public uint VotesCount { get; set; }
    }
}
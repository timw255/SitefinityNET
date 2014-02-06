using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityNET.Sitefinity.Types
{
    public enum ContentLifecycleStatus
    {
        Master,
        Temp,
        Live
    }

    public enum ContentUIStatus
    {
        Draft = 0,
        PrivateCopy = 1,
        Published = 2,
        Scheduled = 3,
        NotSupported = 666
    }

    public enum PostRights
    {
        None,
        Everyone,
        RegisteredUsers
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lateetud.NServiceBus.DAL
{
    interface BaseManager
    {
        void Insert(string messageId, string message);
        object Select(string messageId);
        void Update(string messageId, string status);
    }
}

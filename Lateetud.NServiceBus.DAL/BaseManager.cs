using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lateetud.NServiceBus.DAL
{
    interface BaseManager
    {
        void Insert(string generalAgentId, string messageId, string message);
        object Select(string generalAgentId);
        void Update(string generalAgentId, string messageId, string status);
    }
}

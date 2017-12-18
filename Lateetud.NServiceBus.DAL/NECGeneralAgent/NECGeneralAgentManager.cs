using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lateetud.NServiceBus.DAL.NECGeneralAgent
{
    public class NECGeneralAgentManager : BaseManager
    {
        public void Insert(string generalAgentId, string messageId, string message)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                context.GeneralAgent_InsertUpdate("I", generalAgentId, messageId, message, "Pending");
            }
        }

        public object Select(string generalAgentId)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                return context.GeneralAgentByGeneralAgentID_Select(generalAgentId).FirstOrDefault();
            }
        }

        public void Update(string generalAgentId, string messageId, string status)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                context.GeneralAgent_InsertUpdate("U", generalAgentId, messageId, null, status);
            }
        }
    }
}

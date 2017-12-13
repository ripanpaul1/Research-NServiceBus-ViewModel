using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lateetud.NServiceBus.DAL.NECGeneralAgent
{
    public class NECGeneralAgentManager : BaseManager
    {
        public void Insert(string messageId, string message)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                context.GeneralAgent_InsertUpdate("I", messageId, message, "Pending");
            }
        }

        public object Select(string messageId)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                return context.GeneralAgentByGeneralAgentID_Select(messageId).FirstOrDefault();
            }
        }

        public void Update(string messageId, string status)
        {
            using (var context = new NECGeneralAgentEntities())
            {
                context.GeneralAgent_InsertUpdate("U", messageId, null, status);
            }
        }
    }
}

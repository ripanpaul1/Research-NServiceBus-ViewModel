
using System.Linq;
using Lateetud.NServiceBus.DAL.ef;

namespace Lateetud.NServiceBus.DAL.Smart
{
    public class SmartRpaManager
    {
        public void Insert(SmartRpa smartRpa)
        {
            using (var context = new SMARTEntities())
            {
                context.SmartRpa_InsertUpdate("I", smartRpa.ServiceName, smartRpa.MessageId, smartRpa.RequestId, smartRpa.Message, smartRpa.Status);
            }
        }

        public object Select(SmartRpa smartRpa)
        {
            using (var context = new SMARTEntities())
            {
                return context.SmartRpaByRequestId_Select(smartRpa.RequestId).FirstOrDefault();
            }
        }

        public void Update(SmartRpa smartRpa)
        {
            using (var context = new SMARTEntities())
            {
                context.SmartRpa_InsertUpdate("U", smartRpa.ServiceName, smartRpa.MessageId, smartRpa.RequestId, smartRpa.Message, smartRpa.Status);
            }
        }
    }
}

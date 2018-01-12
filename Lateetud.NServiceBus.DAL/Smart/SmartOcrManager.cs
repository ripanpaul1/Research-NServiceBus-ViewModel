
using System.Linq;
using Lateetud.NServiceBus.DAL.ef;

namespace Lateetud.NServiceBus.DAL.Smart
{
    public class SmartOcrManager
    {
        public void Insert(SmartOcr smartOcr)
        {
            using (var context = new SMARTEntities())
            {
                context.SmartOcr_InsertUpdate("I", smartOcr.ServiceName, smartOcr.MessageId, smartOcr.RequestId, smartOcr.Message, smartOcr.Status);
            }
        }

        public object Select(SmartOcr smartOcr)
        {
            using (var context = new SMARTEntities())
            {
                return context.SmartOcrByRequestId_Select(smartOcr.RequestId).FirstOrDefault();
            }
        }

        public void Update(SmartOcr smartOcr)
        {
            using (var context = new SMARTEntities())
            {
                context.SmartOcr_InsertUpdate("U", smartOcr.ServiceName, smartOcr.MessageId, smartOcr.RequestId, "", smartOcr.Status);
            }
        }
    }
}

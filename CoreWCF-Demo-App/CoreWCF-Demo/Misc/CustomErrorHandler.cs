using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Dispatcher;
using log4net;
using System.Reflection;
using System.Runtime.Serialization;

namespace CoreWCF_Demo.Misc
{
    public class CustomErrorHandler : IErrorHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool HandleError(Exception error)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(error);
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var faultException = error as FaultException;
            if (faultException == null)
            {
                //If includeExceptionDetailInFaults = true, the fault exception with details will created by WCF.
                return;
            }
            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, messageFault, faultException.Action);
        }
    }
}

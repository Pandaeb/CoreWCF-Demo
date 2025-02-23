using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using log4net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CoreWCF_Demo.Misc
{
    public class CustomInspectorBehavior : IDispatchMessageInspector, IEndpointBehavior
    {
        private static ILog _logger = LogManager.GetLogger("ReqResLogger");

        public CustomInspectorBehavior()
        {
        }

        private void SetAddressing(ref Message replyMessage)
        {
            var addressingNS = "http://www.w3.org/2005/08/addressing";
            replyMessage.Headers.MessageId = new UniqueId();
            replyMessage.Headers.RelatesTo = OperationContext.Current.IncomingMessageHeaders.MessageId;
            replyMessage.Headers.To = OperationContext.Current.IncomingMessageHeaders.ReplyTo.Uri;
            if (replyMessage.IsFault == false)
            {
                replyMessage.Headers.ReplyTo = new EndpointAddress("http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous");
            }

            var newToHeader = MessageHeader.CreateHeader("To", addressingNS, replyMessage.Headers.To, false);
            var indexTo = replyMessage.Headers.FindHeader("To", addressingNS);
            replyMessage.Headers.RemoveAt(indexTo);
            replyMessage.Headers.Insert(indexTo, newToHeader);

            var incomingActionHeader = OperationContext.Current.IncomingMessageHeaders.FirstOrDefault(x => x.Name == "Action");
            var indexAction = replyMessage.Headers.FindHeader("Action", addressingNS);
            replyMessage.Headers.RemoveAt(indexAction);
            replyMessage.Headers.Insert(indexAction, (MessageHeader)incomingActionHeader);
        }

        private void LogFile(string contents, string name)
        {
            XDocument xml = XDocument.Parse(contents);
            XName passwordXName = XName.Get("Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            var descendants = xml.Descendants(passwordXName);
            descendants.Remove();

            contents = GetIndentedXml(xml);

            if (_logger.IsInfoEnabled)
                _logger.Info(name + " - " + contents);
        }

        public string GetIndentedXml(XDocument xmlDoc)
        {
            var sb = new StringBuilder();
            var xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.IndentChars = "  ";
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.NewLineHandling = NewLineHandling.Replace;

            using (XmlWriter writer = XmlWriter.Create(sb, xmlSettings))
            {
                xmlDoc.Save(writer);
            }

            string xml = sb.ToString();
            return xml;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
            request = buffer.CreateMessage();

            Message messageCopy = buffer.CreateMessage();
            string contents;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(memoryStream);
                messageCopy.WriteMessage(writer);
                writer.Flush();

                memoryStream.Position = 0;
                contents = new StreamReader(memoryStream).ReadToEnd();
            }

            LogFile(contents, "Request");
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            SetAddressing(ref reply);

            MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
            reply = buffer.CreateMessage();

            Message messageCopy = buffer.CreateMessage();
            string contents = messageCopy.ToString();
            LogFile(contents, "Response");
        }


        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var channelDispatcher = endpointDispatcher.ChannelDispatcher;
            if (channelDispatcher == null) return;
            foreach (var ed in channelDispatcher.Endpoints)
            {
                var inspector = new CustomInspectorBehavior();
                ed.DispatchRuntime.MessageInspectors.Add(inspector);
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}

using CoreWCF.Channels;
using CoreWCF;

namespace CoreWCF_Demo.Misc
{
    public static class BindingFactory
    {
        public static Binding PrepareCustomBinding(IConfiguration configuration)
        {
            SecurityBindingElement securityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            securityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11;
            securityBindingElement.SecurityHeaderLayout = SecurityHeaderLayout.Strict;
            securityBindingElement.IncludeTimestamp = false;

            var encodingBindingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, System.Text.Encoding.UTF8);
            encodingBindingElement.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
            {
                MaxDepth = int.MaxValue,
                MaxStringContentLength = int.MaxValue,
                MaxArrayLength = int.MaxValue,
                MaxBytesPerRead = int.MaxValue,
                MaxNameTableCharCount = int.MaxValue
            };

            var httpTransportBindingElement = new HttpTransportBindingElement();
            httpTransportBindingElement.ManualAddressing = true;
            httpTransportBindingElement.MaxBufferPoolSize = int.MaxValue;

            var customBinding = new CustomBinding(securityBindingElement, encodingBindingElement, httpTransportBindingElement);

            customBinding.CloseTimeout = TimeSpan.FromSeconds(configuration.GetValue<int>("CloseTimeout"));
            customBinding.OpenTimeout = TimeSpan.FromSeconds(configuration.GetValue<int>("OpenTimeout"));
            customBinding.ReceiveTimeout = TimeSpan.FromSeconds(configuration.GetValue<int>("ReceiveTimeout"));
            customBinding.SendTimeout = TimeSpan.FromSeconds(configuration.GetValue<int>("SendTimeout"));

            return customBinding;
        }
    }
}

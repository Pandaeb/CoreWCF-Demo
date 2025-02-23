using CoreWCF;
using CoreWCF_Demo_Infrastructure.Models.Abstractions;
using System.Runtime.Serialization;

namespace CoreWCF_Demo_Infrastructure.Models.WcfService
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
    [XmlSerializerFormat]
    [DataContract]
    public partial class TestServiceMessageRSBody : ResponseBase<TestServiceMessageRSBody>
    {
        private const int TestServiceMessageContentCode = 1;

        public TestServiceMessageRSBody()
        {

        }

        public TestServiceMessageRSBody(string echoToken, decimal currentVersion)
        {
            TimeStamp = DateTime.UtcNow;
            EchoToken = echoToken;
            Version = currentVersion;
            MessageContentCode = TestServiceMessageContentCode.ToString();
            Success = new Common.OTA_Success();
        }
    }
}

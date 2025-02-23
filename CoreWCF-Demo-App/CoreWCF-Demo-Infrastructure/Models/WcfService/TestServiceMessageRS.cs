using CoreWCF;
using System.Xml.Serialization;

namespace CoreWCF_Demo_Infrastructure.Models.WcfService
{
    [MessageContract(WrapperName = "TestServiceMessageRSBody", IsWrapped = false)]
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
    [XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
    [XmlSerializerFormat]
    public class TestServiceMessageRS
    {
        [MessageBodyMember(Name = "TestServiceMessageRSBody", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public TestServiceMessageRSBody ContractBody { get; set; }
    }
}

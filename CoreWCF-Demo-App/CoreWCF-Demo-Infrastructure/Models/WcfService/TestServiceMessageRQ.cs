using CoreWCF;
using System.Xml.Serialization;

namespace CoreWCF_Demo_Infrastructure.Models.WcfService
{

    [MessageContract(WrapperName = "TestServiceMessageRQBody", IsWrapped = false, WrapperNamespace = "WrapperNamespace")]
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
    [XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
    [XmlSerializerFormat]
    public class TestServiceMessageRQ
    {
        [MessageBodyMember(Name = "TestServiceMessageRQBody", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public TestServiceMessageRQBody MessageBody { get; set; }
    }
}

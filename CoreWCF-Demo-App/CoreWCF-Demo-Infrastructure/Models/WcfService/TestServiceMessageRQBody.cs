using CoreWCF;
using CoreWCF_Demo_Infrastructure.Models.Abstractions;

namespace CoreWCF_Demo_Infrastructure.Models.WcfService
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [MessageContract]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
    public partial class TestServiceMessageRQBody : RequestBase<TestServiceMessageRQBody>
    {
        private string messageTextField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MessageTextField { get => messageTextField; set => messageTextField = value; }
    }
}

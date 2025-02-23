using CoreWCF_Demo_Infrastructure.Models.Common;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CoreWCF_Demo_Infrastructure.Models.Abstractions
{
    [DataContract]
    [System.SerializableAttribute]
    public abstract class ResponseBase<T> where T : ResponseBase<T>
    {
        private decimal versionField;

        private System.DateTime timeStampField;

        private string echoTokenField;

        private int? messageContentCodeField;

        private OTA_Success successField;

        /// <remarks/>
        [DataMember(EmitDefaultValue = false)]
        [System.Xml.Serialization.XmlAttributeAttribute]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [DataMember(EmitDefaultValue = false)]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime TimeStamp
        {
            get
            {
                return this.timeStampField;
            }
            set
            {
                this.timeStampField = value;
            }
        }

        /// <remarks/>
        [DataMember(EmitDefaultValue = false)]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EchoToken
        {
            get
            {
                return this.echoTokenField;
            }
            set
            {
                this.echoTokenField = value;
            }
        }

        /// <remarks/>
        [DataMember(EmitDefaultValue = false)]
        [System.Xml.Serialization.XmlAttributeAttribute]
        public string MessageContentCode
        {
            get
            {
                if (this.messageContentCodeField.HasValue)
                    return this.messageContentCodeField.Value.ToString();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.messageContentCodeField = int.Parse(value);
                else
                    this.messageContentCodeField = null;
            }
        }

        /// <remarks/>
        [DataMember(EmitDefaultValue = false)]
        [XmlElement]
        public OTA_Success Success
        {
            get
            {
                return this.successField;
            }
            set
            {
                this.successField = value;
            }
        }
    }
}
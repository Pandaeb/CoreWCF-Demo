using System.Runtime.Serialization;

namespace CoreWCF_Demo_Infrastructure.Models.Abstractions
{
    [DataContract]
    [System.SerializableAttribute]
    public abstract class RequestBase<T> where T : RequestBase<T>
    {
        private string echoTokenField;

        private System.DateTime timeStampField;

        private decimal versionField;

        private byte messageContentCodeField;
        
        /// <remarks/>
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte MessageContentCode
        {
            get
            {
                return this.messageContentCodeField;
            }
            set
            {
                this.messageContentCodeField = value;
            }
        }
    }
}

using CoreWCF;
using CoreWCF_Demo_Infrastructure.Models.WcfService;

namespace CoreWCF_Demo_Infrastructure.Services
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IWcfService
    {
        [OperationContract(Action = "FirstServiceMessage", ReplyAction = "FirstServiceMessage")]
        Task<TestServiceMessageRS> TestServiceMessage(TestServiceMessageRQ request);
    }
}

using CoreWCF;
using CoreWCF_Demo_Infrastructure.Models.WcfService;

namespace CoreWCF_Demo_Infrastructure.Services
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IWcfService
    {
        [OperationContract(Action = "TestServiceMessage", ReplyAction = "TestServiceMessage")]
        Task<TestServiceMessageRS> TestServiceMessage(TestServiceMessageRQ request);
    }
}

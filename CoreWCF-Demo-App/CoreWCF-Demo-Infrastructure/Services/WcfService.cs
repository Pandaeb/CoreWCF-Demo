using CoreWCF;
using CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme;
using CoreWCF_Demo_Infrastructure.Models.WcfService;
using log4net;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using static CoreWCF_Demo_Infrastructure.Helpers.LoggerHelper;

namespace CoreWCF_Demo_Infrastructure.Services
{
    public class WcfService : IWcfService
    {
        private static ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const decimal CurrentVersion = 1.000m;

        [Authorize(AuthenticationSchemes = UserPasswordSchemeOptions.Name)]
        public async Task<TestServiceMessageRS> TestServiceMessage(TestServiceMessageRQ request)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info($"Method {GetCurrentMethod()} started");

            if (request.MessageBody == null)
            {
                if (_logger.IsErrorEnabled)
                    _logger.Error("Message body is null");

                throw new FaultException("Message body is null", FaultCode.CreateSenderFaultCode("InvalidRequest", "http://synxis.com/ws/2009/10/"));
            }

            var result = new TestServiceMessageRS();
            var contractBody = new TestServiceMessageRSBody(request.MessageBody?.EchoToken, CurrentVersion);

            result.ContractBody = contractBody;

            return result;
        }
    }
}

using CoreWCF;
using log4net;

using System.Reflection;

namespace CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme
{
    public class CustomUserNamePasswordValidator : CoreWCF.IdentityModel.Selectors.UserNamePasswordValidator
    {
        private static ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserPasswordAuthConfiguration _userPasswordAuthConfiguration;

        public CustomUserNamePasswordValidator(UserPasswordAuthConfiguration userPasswordAuthConfiguration)
        {
            _userPasswordAuthConfiguration = userPasswordAuthConfiguration;
        }

        public override ValueTask ValidateAsync(string userName, string password)
        {
            var user = _userPasswordAuthConfiguration.Users.FirstOrDefault(x => x.Username == userName);

            if (user == null)
            {
                if (_logger.IsErrorEnabled)
                    _logger.Error(MethodBase.GetCurrentMethod().Name + " - UserPassword validation failed: Unknown username");

                throw new FaultException("Unknown username", FaultCode.CreateSenderFaultCode("Authentication", "http://synxis.com/ws/2009/10/"));
            }
            if (user.Password != password)
            {
                if (_logger.IsErrorEnabled)
                    _logger.Error(MethodBase.GetCurrentMethod().Name + " - UserPassword validation failed: Wrong username or password");

                throw new FaultException("Wrong username or password", FaultCode.CreateSenderFaultCode("Authentication", "http://synxis.com/ws/2009/10/"));
            }

            return new ValueTask();
        }
    }
}

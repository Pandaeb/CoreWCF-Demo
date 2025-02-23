using System.Runtime.CompilerServices;

namespace CoreWCF_Demo_Infrastructure.Helpers
{
    public class LoggerHelper
    {
        public static string GetCurrentMethod([CallerMemberName] string method = "")
        {
            return method;
        }
    }
}

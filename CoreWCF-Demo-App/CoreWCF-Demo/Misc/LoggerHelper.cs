using log4net.Config;
using log4net;
using System.Reflection;
using CoreWCF.Channels;
using CoreWCF;

namespace CoreWCF_Demo.Misc
{
    public static class LoggerHelper
    {
        public static void ConfigureLogger()
        {
            var uriPath = new Uri(Assembly.GetEntryAssembly()?.Location ?? string.Empty);
            var rootDir = Path.GetDirectoryName(uriPath.LocalPath);
            var configurationDir = rootDir + @"\Configuration";

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var configPath = Path.Combine(configurationDir, "log4net.cfg.xml");
            XmlConfigurator.Configure(logRepository, new FileInfo(configPath));
        }
    }
}

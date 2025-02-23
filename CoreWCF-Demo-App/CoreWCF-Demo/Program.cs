using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCF_Demo.Configuration;
using CoreWCF_Demo.Misc;
using CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme;
using CoreWCF_Demo_Infrastructure.Services;
using log4net;
using System.Reflection;

ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

LoggerHelper.ConfigureLogger();
try
{
    if (_logger.IsInfoEnabled)
        _logger.Info("Starting service...");

    if (args == null || args.Length == 0)
    {
        if (_logger.IsErrorEnabled)
            _logger.Error("No Ip and/or Port found in parameters. Stopping service...");
        Environment.Exit(0);
    }

    var argsIp = args.SkipWhile(x => x != "--ipAddress").Skip(1).FirstOrDefault();
    var argsPort = args.SkipWhile(x => x != "--port").Skip(1).FirstOrDefault();
    var argsUrlHttp = $"http://{argsIp}:{argsPort}";

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>();

    builder.Services.ConfigureServices(builder.Configuration);

    var app = builder.Build();

    var customBinding = BindingFactory.PrepareCustomBinding(builder.Configuration);

    app.UseServiceModel(builder =>
    {
        if (_logger.IsInfoEnabled)
            _logger.Info("Configuring WcfService and ServiceEndpoints...");

        builder.AddService<WcfService>((serviceOptions) =>
        {
            var includeExceptionDetailInFaults = app.Configuration.GetValue<bool>("IncludeExceptionDetailInFaults");
            serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = includeExceptionDetailInFaults;

            serviceOptions.BaseAddresses.Add(new Uri("http://" + argsIp));
        })
        .AddServiceEndpoint<WcfService, IWcfService>(customBinding, app.Configuration.GetValue<string>("EndpointPath"));

        builder.ConfigureServiceHostBase<WcfService>(wcfServiceHostBase =>
        {
            if (_logger.IsInfoEnabled)
                _logger.Info("Configuring UserNamePasswordValidator...");
            var userPasswordAuthConfiguration = app.Services.GetService<UserPasswordAuthConfiguration>();
            wcfServiceHostBase.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = CoreWCF.Security.UserNamePasswordValidationMode.Custom;
            wcfServiceHostBase.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNamePasswordValidator(userPasswordAuthConfiguration);

            IEndpointBehavior behavior = new CustomInspectorBehavior();
            foreach (var endpoint in wcfServiceHostBase.Description.Endpoints)
            {
                endpoint.EndpointBehaviors.Add(behavior);
            }
        });
    });

    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpGetEnabled = true;
    serviceMetadataBehavior.HttpGetUrl = new Uri(argsUrlHttp + "/metadata");

    app.UseHttpsRedirection();

    if (_logger.IsInfoEnabled)
        _logger.Info("All configuration succeeded. Application run...");

    await app.RunAsync(argsUrlHttp);
}
catch (Exception ex)
{
    if (_logger.IsErrorEnabled)
        _logger.Error(ex.ToString());
}



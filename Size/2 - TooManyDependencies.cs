using Serilog;

namespace Size;

public class TooManyDependencies
{
    private readonly IRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPersonManager _personManager;
    private readonly IEmailSender _emailSender;
    private readonly IFileReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly IApiClient _apiClient;
    private readonly IAuditLogger _auditLogger;
    private readonly IMetricsProvider _metricsProvider;
    private readonly ICache _cache;
    private readonly ILogger _logger;

    public TooManyDependencies(
        IRepository repository,
        IDateTimeProvider dateTimeProvider,
        IPersonManager personManager,
        IEmailSender emailSender,
        IFileReader fileReader,
        IFileWriter fileWriter,
        IApiClient apiClient,
        IAuditLogger auditLogger,
        IMetricsProvider metricsProvider,
        ICache cache,
        ILogger logger)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
        _personManager = personManager;
        _emailSender = emailSender;
        _fileReader = fileReader;
        _fileWriter = fileWriter;
        _apiClient = apiClient;
        _auditLogger = auditLogger;
        _metricsProvider = metricsProvider;
        _cache = cache;
        _logger = logger;
    }

    public string GetData(string id)
    {
        return $"data: {_apiClient.GetData(id)}";
    }
}

public interface ICache
{
}

public interface IMetricsProvider
{
}

public interface IAuditLogger
{
}

public interface IApiClient
{
    string GetData(string id);
}

public class ApiClient : IApiClient
{
    public string GetData(string id)
    {
        return $"data_{id}";
    }
}

public interface IFileWriter
{
}

public interface IFileReader
{
    public void SetPath(string path);
    public string ReadLine();
    public bool IsAtEndOfFile();
}

public interface IEmailSender
{
}

public interface IPersonManager
{
}

public interface IDateTimeProvider
{
}

public interface IRepository
{
}
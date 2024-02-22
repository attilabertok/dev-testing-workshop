using Serilog;

namespace Observability;

public class IndirectOutputToLog(ILogger logger)
{
    public void Add(int a, int b)
    {
        logger.Information("Adding {A} and {B}", a, b);
        var result = a + b;
        logger.Information("Result is {Result}", result);
    }
}
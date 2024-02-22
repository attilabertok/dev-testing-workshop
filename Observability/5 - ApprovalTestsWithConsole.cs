namespace Observability;

public class ApprovalTestsWithConsole
{
    public void Add(int a, int b)
    {
        Console.WriteLine($"Adding {a} and {b}");
        var result = a + b;
        Console.WriteLine($"Result is {result}");
    }
}
namespace Controllability;

public class Isolability
{
    public int Add(int a, int b)
    {
        return IsolabilityDependency.GetResult(a + b);
    }

    private static class IsolabilityDependency
    {
        public static int GetResult(int result)
        {
            return result + 1;
        }
    }
}


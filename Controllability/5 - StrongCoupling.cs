namespace Controllability;

public class StrongCoupling(StrongCouplingDependency dependency)
{
    public int Add(int a, int b)
    {
        if (dependency.GetTimeOfDay() == StrongCouplingDependency.Morning)
        {
            return a + b;
        }

        // I am too tired to add 2 numbers
        return a + b + 1;
    }
}

public class StrongCouplingDependency
{
    public const string Morning = "morning";
    public const string Evening = "evening";

    public string GetTimeOfDay()
    {
        return DateTime.Now.Hour < 12 ? Morning : Evening;
    }
}
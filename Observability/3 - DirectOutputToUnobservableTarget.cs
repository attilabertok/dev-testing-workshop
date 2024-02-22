namespace Observability;

public class DirectOutputToUnobservableTarget
{
    private int result;

    public void Add(int a, int b)
    {
        result = a + b;
    }
}

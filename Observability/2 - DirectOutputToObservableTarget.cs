namespace Observability;

public class DirectOutputToObservableTarget
{
    public int Result { get; private set; }

    public void Add(int a, int b)
    {
        Result = a + b;
    }
}
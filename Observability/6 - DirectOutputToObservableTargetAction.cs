namespace Observability;

public class DirectOutputToObservableTargetAction
{
    public void Add(int a, int b, Action<int> action)
    {
        action(a + b);
    }
}
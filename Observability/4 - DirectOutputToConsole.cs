namespace Observability;

public class DirectOutputToConsole
{
    public void Add(int a, int b)
    {
        // this could be a MessageBox.Show() as well
        Console.WriteLine(a + b);
    }
}

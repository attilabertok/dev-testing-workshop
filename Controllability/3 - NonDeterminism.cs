namespace Controllability;

public class NonDeterminism
{
    Random random = new();

    public int GetRandomNumber()
    {
        return random.Next(1, 100);
    }
}
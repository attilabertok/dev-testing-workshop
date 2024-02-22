namespace Controllability;

public class TemporalCoupling
{
    private bool isInitialized = false;

    public void Initialize()
    {
        if (isInitialized)
        {
            throw new InvalidOperationException("Already initialized");
        }

        isInitialized = true;
    }

    public string ReticulateSplines()
    {
        if (!isInitialized)
        {
            throw new InvalidOperationException("Not initialized");
        }

        return "Splines reticulated";
    }
}
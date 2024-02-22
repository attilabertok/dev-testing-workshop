namespace Controllability;

public class UsingGlobalState
{
    public string Greet()
    {
        var timeOfDay = DateTime.Now.Hour < 12 ? "morning" : "evening";

        return $"Good {timeOfDay} from {Environment.MachineName}";
    }
}
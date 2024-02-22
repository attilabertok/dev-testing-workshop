namespace Controllability;

public class UsingImplicitData
{
    private const string path = "C:\\temp\\file.txt";

    public int CountCharacters()
    {
        var text = File.ReadAllText(path);
        return text.Length;
    }
}
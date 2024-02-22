namespace Size;

public class Singularity
{
    public string Greet(IHaveName somebody)
    {
        return $"Hello, {somebody.FullName}";
    }
}

public interface IHaveName
{
    string FullName { get; }
}

public class Person : IHaveName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}

public class Employee : IHaveName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int Age { get; set; }
    public string FullName => $"{FirstName}x {LastName}";

    public string Department { get; set; }
    public string Title { get; set; }
}

public class Student : IHaveName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public string Major { get; set; }
}
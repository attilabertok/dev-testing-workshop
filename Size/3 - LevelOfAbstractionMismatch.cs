namespace Size;

public class LevelOfAbstractionMismatch
{
    private readonly IEmployeeRepository _employees;
    private readonly IFileReader _bookedVacationsFile;

    public LevelOfAbstractionMismatch(
        IEmployeeRepository employees,
        IFileReader bookedVacationsFile)
    {
        _employees = employees;
        _bookedVacationsFile = bookedVacationsFile;
    }

    public int CalculateVacationDayCount(string employeeId)
    {
        var employee = _employees.Get(employeeId);
        var totalVacationCount = 20 + ((employee.Age - 18) / 3);
        _bookedVacationsFile.SetPath(@"C:\temp\bookedVacations.txt");
        Dictionary<string, int> bookedVacations = [];
        while (!_bookedVacationsFile.IsAtEndOfFile())
        {
            var line = _bookedVacationsFile.ReadLine();
            var parts = line.Split(' ');
            bookedVacations[parts[0]] = int.Parse(parts[1]);
        }
        var vacationForEmployee = totalVacationCount - bookedVacations[employeeId];

        return vacationForEmployee;
    }
}

public interface IEmployeeRepository
{
    Employee Get(string employeeId);
}
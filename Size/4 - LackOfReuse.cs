namespace Size;

public class LackOfReuse
{
    public int CalculateAge(int year, int month, int day)
    {
        var dateOfBirth = new MyDateTime(year, month, day);
        var now = new MyDateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            DateTime.Now.Hour,
            DateTime.Now.Minute,
            DateTime.Now.Second);

        if (now.Second > dateOfBirth.Second)
        {
            dateOfBirth.Minute++;
        }

        if (now.Minute > dateOfBirth.Minute)
        {
            dateOfBirth.Hour++;
        }

        if (now.Hour > dateOfBirth.Hour)
        {
            dateOfBirth.Day++;
        }

        if (now.Day > dateOfBirth.Day)
        {
            dateOfBirth.Month++;
        }

        if (now.Month > dateOfBirth.Month)
        {
            dateOfBirth.Year++;
        }

        return now.Year - dateOfBirth.Year;
    }

    public class MyDateTime
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public MyDateTime(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public MyDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = month;
            Second = second;
        }
    }
}
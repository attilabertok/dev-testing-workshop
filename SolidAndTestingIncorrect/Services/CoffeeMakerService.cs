using SolidAndTestingIncorrect.Domain;

namespace SolidAndTestingIncorrect.Services;

public class CoffeeMakerService : CoffeeMakerServiceBase
{
    public const int MilkCapacity = 600;
    public const int MaxBrewsBetweenCleans = 20;
    public const int MaxBrewsBetweenDescales = 100;

    public int CurrentMilkAmount { get; private set; }
    public bool IsKeepingWarm { get; private set; }
    public int ConfiguredTemperature { get; private set; }
    public CoffeeStrength Strength { get; private set; }
    public int BrewsSinceLastClean { get; private set; }
    public int BrewsSinceLastDescale { get; private set; }


    public ICoffee BrewFilterCoffee()
    {
        var beverage = new FilterCoffee();

        Brew(beverage);

        return beverage;
    }

    public ICoffee BrewLatte()
    {
        var beverage = new Latte();

        Brew(beverage);

        return beverage;
    }

    public ICoffee BrewCappuccino()
    {
        var beverage = new Cappuccino();

        Brew(beverage);

        return beverage;
    }

    public ICoffee BrewAmericano()
    {
        var beverage = new Americano();

        Brew(beverage);

        return beverage;
    }

    public void AddMilk(int amount)
    {
        switch (amount)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Milk amount must be greater than 0.");
            case > MilkCapacity:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"Milk amount must be less than {MilkCapacity}.");
        }

        CurrentMilkAmount += CurrentMilkAmount + amount > MilkCapacity
            ? throw new ArgumentException("Amount exceeds milk capacity.")
            : amount;
    }

    public void Clean()
    {
        BrewsSinceLastClean = 0;
    }

    public void Descale()
    {
        BrewsSinceLastDescale = 0;
    }

    public void KeepWarm()
    {
        IsKeepingWarm = true;
    }

    public void SetTemperature(Temperature temperature)
    {
        ConfiguredTemperature = temperature switch
        {
            Temperature.Low => 80,
            Temperature.Medium => 90,
            Temperature.High => 100,
            _ => throw new ArgumentOutOfRangeException(nameof(temperature), temperature, "Invalid temperature.")
        };
    }

    public void SetStrength(CoffeeStrength strength)
    {
        Strength = strength;
    }

    private bool CanBrew()
    {
        return !NeedsMaintenance();
    }

    private bool NeedsMaintenance()
    {
        return BrewsSinceLastClean >= MaxBrewsBetweenCleans || BrewsSinceLastDescale >= MaxBrewsBetweenDescales;
    }

    private static int CalculateExtraBeans(ICoffee coffee)
    {
        return coffee.Strength switch
        {
            CoffeeStrength.Weak => 0,
            CoffeeStrength.Medium => 20,
            CoffeeStrength.Strong => 40,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected override void Brew(ICoffee beverage)
    {
        if (!CanBrew())
        {
            throw new InvalidOperationException("Coffee maker needs maintenance.");
        }

        beverage.Strength = (CoffeeStrength)Math.Max((int)beverage.Strength, (int)Strength);

        base.Brew(beverage);

        IsKeepingWarm = false;
        CurrentMilkAmount -= beverage.RequiredMilk;
        BrewsSinceLastClean++;
        BrewsSinceLastDescale++;
    }

    protected override bool HasEnoughIngredientsFor(ICoffee beverage)
    {
        var extraBeans = CalculateExtraBeans(beverage);

        return CurrentWaterAmount >= beverage.RequiredWater
               && CurrentBeansAmount >= (beverage.RequiredBeans + extraBeans)
               && CurrentMilkAmount >= beverage.RequiredMilk;
    }
}

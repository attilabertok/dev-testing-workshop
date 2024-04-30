namespace SolidAndTestingIncorrect.ViewModels;

public class CoffeeMakerService : ICoffeeMakerService
{
    public const int WaterCapacity = 1200;
    public const int BeansCapacity = 1200;
    public const int MilkCapacity = 600;
    public const int MaxBrewsBetweenCleans = 20;
    public const int MaxBrewsBetweenDescales = 100;

    public int CurrentWaterAmount { get; private set; }
    public int CurrentBeansAmount { get; private set; }
    public int CurrentMilkAmount { get; private set; }
    public bool IsOn { get; private set; }
    public bool IsKeepingWarm { get; private set; }
    public int ConfiguredTemperature { get; private set; }
    public CoffeeStrength Strength { get; private set; }
    public int BrewsSinceLastClean { get; private set; }
    public int BrewsSinceLastDescale { get; private set; }

    public ICoffee BrewEspresso()
    {
        var beverage = new Espresso();

        Brew(beverage);

        return beverage;
    }

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

    public void AddWater(int amount)
    {
        switch (amount)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Water amount must be greater than 0.");
            case > WaterCapacity:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"Water amount must be less than {WaterCapacity}.");
        }

        CurrentWaterAmount += CurrentWaterAmount + amount > WaterCapacity
            ? throw new ArgumentException("Amount exceeds water capacity.")
            : amount;
    }

    public void AddBeans(int amount)
    {
        switch (amount)
        {
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Beans amount must be greater than 0.");
            case > BeansCapacity:
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"Beans amount must be less than {BeansCapacity}.");
        }

        CurrentBeansAmount += CurrentBeansAmount + amount > BeansCapacity
            ? throw new ArgumentException("Amount exceeds beans capacity.")
            : amount;
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

    public void TurnOn()
    {
        IsOn = true;
    }

    public void TurnOff()
    {
        IsOn = false;
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

    private bool HasEnoughIngredientsFor(ICoffee coffee)
    {
        var extraBeans = CalculateExtraBeans(coffee);

        return CurrentWaterAmount >= coffee.RequiredWater
               && CurrentBeansAmount >= (coffee.RequiredBeans + extraBeans)
               && CurrentMilkAmount >= coffee.RequiredMilk;
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

    private void Brew(ICoffee beverage)
    {
        if (!CanBrew())
        {
            throw new InvalidOperationException("Coffee maker needs maintenance.");
        }

        beverage.Strength = (CoffeeStrength)Math.Max((int)beverage.Strength, (int)Strength);

        if (!HasEnoughIngredientsFor(beverage))
        {
            throw new InvalidOperationException($"Not enough ingredients to brew {beverage.Name}.");
        }

        IsKeepingWarm = false;
        CurrentWaterAmount -= beverage.RequiredWater;
        CurrentBeansAmount -= beverage.RequiredBeans;
        CurrentMilkAmount -= beverage.RequiredMilk;
        BrewsSinceLastClean++;
        BrewsSinceLastDescale++;
    }
}

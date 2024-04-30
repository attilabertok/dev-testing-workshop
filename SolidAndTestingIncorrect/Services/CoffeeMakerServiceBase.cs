using SolidAndTestingIncorrect.Domain;

namespace SolidAndTestingIncorrect.Services;

public class CoffeeMakerServiceBase
{
    public const int WaterCapacity = 1200;
    public const int BeansCapacity = 1200;

    public bool IsOn { get; private set; }
    public int CurrentWaterAmount { get; private set; }
    public int CurrentBeansAmount { get; private set; }

    public ICoffee BrewEspresso()
    {
        var beverage = new Espresso();

        Brew(beverage);

        return beverage;
    }

    public void TurnOn()
    {
        IsOn = true;
    }

    public void TurnOff()
    {
        IsOn = false;
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

    protected virtual void Brew(ICoffee beverage)
    {
        if (!HasEnoughIngredientsFor(beverage))
        {
            throw new InvalidOperationException($"Not enough ingredients to brew {beverage.Name}.");
        }

        CurrentWaterAmount -= beverage.RequiredWater;
        CurrentBeansAmount -= beverage.RequiredBeans;
    }

    protected virtual bool HasEnoughIngredientsFor(ICoffee beverage)
    {
        return CurrentWaterAmount >= beverage.RequiredWater
            && CurrentBeansAmount >= beverage.RequiredBeans;
    }
}

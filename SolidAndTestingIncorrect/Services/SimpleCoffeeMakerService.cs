using SolidAndTestingIncorrect.Domain;

namespace SolidAndTestingIncorrect.Services;

public class SimpleCoffeeMakerService : CoffeeMakerServiceBase
{
    public ICoffee BrewAmericano()
    {
        var beverage = new Americano();

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
        throw new InvalidOperationException("This simple machine cannot brew a beverage that contains milk");
    }


    public override ICoffee BrewCappuccino()
    {
        throw new InvalidOperationException("This simple machine cannot brew a beverage that contains milk");
    }
}

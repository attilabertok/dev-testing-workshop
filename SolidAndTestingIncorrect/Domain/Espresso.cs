namespace SolidAndTestingIncorrect.Domain;

public class Espresso : ICoffee
{
    public string Name => "Espresso";
    public string Description => "A small, strong coffee.";
    public CoffeeStrength Strength { get; set; } = CoffeeStrength.Strong;
    public int RequiredBeans => 40;
    public int RequiredWater => 40;
    public int RequiredMilk => 0;
}
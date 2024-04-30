namespace SolidAndTestingIncorrect.ViewModels;

public class Latte : ICoffee
{
    public string Name => "Latte";
    public string Description => "A medium, milky coffee.";
    public CoffeeStrength Strength { get; set; } = CoffeeStrength.Medium;
    public int RequiredBeans => 40;
    public int RequiredWater => 40;
    public int RequiredMilk => 260;
}
namespace SolidAndTestingIncorrect.Domain;

public class Americano : ICoffee
{
    public string Name => "Americano";
    public string Description => "A large, weak coffee.";
    public CoffeeStrength Strength { get; set; } = CoffeeStrength.Weak;
    public int RequiredBeans => 80;
    public int RequiredWater => 300;
    public int RequiredMilk => 0;
}
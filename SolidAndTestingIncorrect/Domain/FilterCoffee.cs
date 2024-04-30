namespace SolidAndTestingIncorrect.Domain;

public class FilterCoffee : ICoffee
{
    public string Name => "Filter Coffee";
    public string Description => "A large, mild coffee.";
    public CoffeeStrength Strength { get; set; } = CoffeeStrength.Weak;
    public int RequiredBeans => 80;
    public int RequiredWater => 200;
    public int RequiredMilk => 0;
}
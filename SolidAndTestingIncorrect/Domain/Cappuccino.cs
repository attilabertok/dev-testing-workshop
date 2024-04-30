namespace SolidAndTestingIncorrect.Domain;

public class Cappuccino : ICoffee
{
    public string Name => "Cappuccino";
    public string Description => "A medium, frothy coffee.";
    public CoffeeStrength Strength { get; set; } = CoffeeStrength.Medium;
    public int RequiredBeans => 40;
    public int RequiredWater => 40;
    public int RequiredMilk => 100;
}
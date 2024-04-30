namespace SolidAndTestingIncorrect.ViewModels;

public interface ICoffee
{
    string Name { get; }
    string Description { get; }
    CoffeeStrength Strength { get; set; }
    int RequiredBeans { get; }
    int RequiredWater { get; }
    int RequiredMilk { get; }
}
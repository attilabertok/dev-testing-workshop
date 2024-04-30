namespace SolidAndTestingIncorrect.ViewModels;

public interface ICoffeeMakerService
{
    ICoffee BrewEspresso();
    ICoffee BrewFilterCoffee();
    ICoffee BrewLatte();
    ICoffee BrewCappuccino();
    ICoffee BrewAmericano();

    void AddWater(int amount);
    void AddBeans(int amount);
    void AddMilk(int amount);

    void Clean();
    void Descale();
        
    void TurnOn();
    void TurnOff();
    void KeepWarm();

    void SetTemperature(Temperature temperature);
    void SetStrength(CoffeeStrength strength);
}
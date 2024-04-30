using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace SolidAndTestingIncorrect.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly ICoffeeMakerService coffeeMakerService;
    private string title = "Prism Unity Application wat";
    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public MainWindowViewModel()
    {
    }

    public MainWindowViewModel(ICoffeeMakerService coffeeMakerService)
    {
        this.coffeeMakerService = coffeeMakerService;
    }

    public DelegateCommand DisplayErrorCommand { get; private set; } = new DelegateCommand(DisplayError);

    private static void DisplayError()
    {
        MessageBox.Show("Error, I has a sad, and it's big.");
    }
}

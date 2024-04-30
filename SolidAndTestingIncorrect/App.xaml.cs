using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using SolidAndTestingIncorrect.Services;
using SolidAndTestingIncorrect.ViewModels;
using SolidAndTestingIncorrect.Views;

namespace SolidAndTestingIncorrect;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<CoffeeMakerService>();
    }
}

using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using MyPrismApp.Views;
using MyPrismApp.ViewModels;
using Prism.Unity;

namespace MyPrismApp
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<MainViewModel>();
            containerRegistry.RegisterSingleton<TextFieldViewModel1>();
            containerRegistry.RegisterSingleton<TextFieldViewModel2>();
            containerRegistry.RegisterSingleton<TextFieldViewModel3>();
            containerRegistry.RegisterSingleton<DisabledTextFieldViewModel>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("RegionTextField1", typeof(TextFieldView1));
            regionManager.RegisterViewWithRegion("RegionTextField2", typeof(TextFieldView2));
            regionManager.RegisterViewWithRegion("RegionTextField3", typeof(TextFieldView3));
            regionManager.RegisterViewWithRegion("RegionDisabledTextField", typeof(DisabledTextFieldView));
            regionManager.RegisterViewWithRegion("RegionInputField", typeof(InputFieldView));
        }
    }
}

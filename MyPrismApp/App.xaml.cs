using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using MyPrismApp.Views;
using MyPrismApp.ViewModels;

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
            // ViewModelLocator автоматически разрешит зависимости для ViewModel, создаваемых для View в регионах,
            // если MainViewModel зарегистрирован как синглтон.
            // EventAggregator уже зарегистрирован Prism.
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
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

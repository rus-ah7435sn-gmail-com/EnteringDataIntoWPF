using Prism.Ioc;
using Prism.Unity; // Изменено на Prism.Unity
using System.Windows;
using MyPrismApp.Views;
using MyPrismApp.ViewModels; // Добавлено для явной регистрации ViewModel, если потребуется

namespace MyPrismApp
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Регистрируем MainView для навигации.
            // Prism автоматически свяжет MainView с MainViewModel по соглашению (если ViewModelLocator.AutoWireViewModel="True" в XAML)
            // или можно зарегистрировать их явно:
            // containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            // containerRegistry.RegisterForNavigation<MainView>(); // Закомментировано, т.к. MainView больше не используется напрямую для навигации в ContentRegion

            // Регистрация Views для текстовых полей.
            // Это позволяет Prism создавать экземпляры этих View и внедрять их в регионы.
            // ViewModels для этих View (например, TextFieldViewModel1) будут автоматически найдены и связаны
            // благодаря ViewModelLocator.AutoWireViewModel="True" в XAML каждого View.
            containerRegistry.RegisterForNavigation<TextFieldView1>();
            containerRegistry.RegisterForNavigation<TextFieldView2>();
            containerRegistry.RegisterForNavigation<TextFieldView3>();
            containerRegistry.RegisterForNavigation<DisabledTextFieldView>();

            // Prism автоматически регистрирует многие свои сервисы, такие как IRegionManager, IEventAggregator и т.д.
            // Явная регистрация ViewModel обычно не требуется, если они следуют соглашениям об именовании
            // и не требуют сложной логики создания экземпляров.

            // Здесь следует регистрировать любые пользовательские сервисы, необходимые для приложения.
            // Например:
            // containerRegistry.RegisterSingleton<IMyService, MyService>();
            // containerRegistry.Register<IOtherService, OtherService>();
        }

        protected override void ConfigureModuleCatalog(Prism.Modularity.IModuleCatalog moduleCatalog)
        {
            // Здесь можно будет регистрировать модули, если приложение будет модульным
            base.ConfigureModuleCatalog(moduleCatalog);
        }

        // Используем UnityContainerExtension
        protected override IContainerExtension CreateContainerExtension()
        {
            return new UnityContainerExtension();
        }

        // Этот метод вызывается после инициализации оболочки (MainWindow)
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Доступ к контейнеру здесь также может быть через this.Container для единообразия,
            // но Container напрямую тоже доступен как свойство базового класса.
            var regionManager = this.Container.Resolve<Prism.Regions.IRegionManager>();

            // Начальная загрузка Views в соответствующие регионы
            regionManager.RequestNavigate("RegionTextField1", nameof(TextFieldView1));
            regionManager.RequestNavigate("RegionTextField2", nameof(TextFieldView2));
            regionManager.RequestNavigate("RegionTextField3", nameof(TextFieldView3));
            regionManager.RequestNavigate("RegionDisabledTextField", nameof(DisabledTextFieldView));

            // Убедимся, что старая навигация к ContentRegion закомментирована или удалена
            // var navigationService = Container.Resolve<Prism.Regions.IRegionManager>(); // Это дублирует regionManager выше
            // navigationService.RequestNavigate("ContentRegion", nameof(MainView));
        }
    }
}

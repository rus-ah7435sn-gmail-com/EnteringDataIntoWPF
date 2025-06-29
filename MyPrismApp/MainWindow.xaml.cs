using Prism.Events;
using MyPrismApp.ViewModels;
using MyPrismApp.Views; // Для проверки типов TextFieldView1/2/3 и InputFieldView
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; // Для VisualTreeHelper

namespace MyPrismApp
{
    public partial class MainWindow : Window
    {
        private readonly IEventAggregator _eventAggregator; // Может быть не нужен здесь напрямую уже
        private readonly MainViewModel _mainViewModel;

        public MainWindow(IEventAggregator eventAggregator, MainViewModel mainViewModel)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator; // Сохраняем, если понадобится для других целей
            _mainViewModel = mainViewModel;

            this.Loaded += MainWindow_Loaded;
            this.PreviewMouseLeftButtonDown += MainWindow_PreviewMouseLeftButtonDown;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Убираем фокус с любого элемента при запуске
            FocusManager.SetFocusedElement(this, this);
            Keyboard.ClearFocus();
            // Устанавливаем начальный FocusedViewModel в null (это уже default для свойства)
            // _mainViewModel.SetFocusedViewModel(null); // Это уже должно быть так по умолчанию
        }

        private void MainWindow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем, был ли клик на одном из TextFieldView1/2/3 или InputFieldView
            DependencyObject? current = e.OriginalSource as DependencyObject;
            bool clickOnFocusableControl = false;

            while (current != null)
            {
                if (current is TextFieldView1 || current is TextFieldView2 || current is TextFieldView3 || current is InputFieldView)
                {
                    clickOnFocusableControl = true;
                    break;
                }
                // Если кликнули на TextBox внутри InputFieldView, это тоже считается "фокусным" контролом
                if (current is TextBox textBox && ParentIsInputFieldView(textBox))
                {
                    clickOnFocusableControl = true;
                    break;
                }
                current = VisualTreeHelper.GetParent(current);
            }

            if (!clickOnFocusableControl)
            {
                // Клик был вне наших целевых UserControl-ов.
                _mainViewModel.SetFocusedViewModel(null);
                // Также убираем реальный фокус клавиатуры, если он где-то был, чтобы соответствовать логическому состоянию
                Keyboard.ClearFocus();
                FocusManager.SetFocusedElement(this, this); // Фокус на окно
            }
            // Если clickOnFocusableControl == true, то соответствующий UserControl (TextFieldViewX или InputFieldView)
            // сам обработает свой PreviewMouseLeftButtonDown и установит нужный FocusedViewModel или ничего не сделает.
            // InputFieldView сам по себе не устанавливает FocusedViewModel, но клик по нему не должен сбрасывать фокус, если он уже установлен на него (это будет сделано в след. шаге)
        }

        private bool ParentIsInputFieldView(DependencyObject? child)
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                if (parent is InputFieldView)
                {
                    return true;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return false;
        }
    }
}

using Prism.Events;
using MyPrismApp.ViewModels;
using MyPrismApp.ViewModels.Events;
using System.Windows;
using System.Windows.Controls; // Для TextBox
using System.Windows.Input;   // Для Keyboard, FocusManager, MouseButtonEventArgs

namespace MyPrismApp
{
    public partial class MainWindow : Window
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly MainViewModel _mainViewModel;

        public MainWindow(IEventAggregator eventAggregator, MainViewModel mainViewModel)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _mainViewModel = mainViewModel;

            this.Loaded += MainWindow_Loaded;
            this.PreviewMouseLeftButtonDown += MainWindow_PreviewMouseLeftButtonDown;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Убираем фокус с любого элемента при запуске
            FocusManager.SetFocusedElement(this, this); // Фокус на само окно
            Keyboard.ClearFocus(); // Очищаем логический фокус

            // Убедимся, что MainViewModel знает, что активных полей нет
            // Это уже должно быть так по инициализации ActiveInputTarget = 0,
            // но на всякий случай можно опубликовать событие, если это необходимо.
            // _eventAggregator.GetEvent<TextFieldFocusChangedEvent>().Publish(false);
            // Однако, это может быть излишним и вызвать ненужные обновления.
            // Инициализации ActiveInputTarget = 0 в MainViewModel должно быть достаточно.
        }

        private void MainWindow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is TextBox))
            {
                // Если клик был не на TextBox из наших регионов или InputField,
                // то это считается кликом "вне" целевых полей.
                var currentFocusedElement = FocusManager.GetFocusedElement(this);
                bool wasOurTextBoxFocused = false;

                if (currentFocusedElement is TextBox textBox)
                {
                    // Проверить, относится ли этот TextBox к TextFieldView1/2/3
                    // Это можно сделать, проверяя DataContext TextBox или его родительских элементов.
                    // Для простоты, если любой TextBox теряет фокус из-за клика не по TextBox,
                    // мы считаем, что TextField-ы теряют "активность" для ввода из MainInput.
                    // Это поведение уже обрабатывается через IsFocused свойство в TextFieldViewModel1/2/3
                    // и публикацию TextFieldFocusChangedEvent(false).
                    // Данный код в MainWindow_PreviewMouseLeftButtonDown должен гарантировать,
                    // что LostFocus на TextBox действительно произойдет.
                }

                // Сбрасываем фокус с клавиатуры, чтобы LostFocus на TextBox сработал, если он был активен.
                FocusManager.SetFocusedElement(this, this);
                Keyboard.ClearFocus();

                // MainViewModel.ActiveInputTarget должен обновиться через TextFieldFocusChangedEvent(false),
                // которое публикуется из сеттера IsFocused соответствующего TextFieldViewModel,
                // когда его TextBox теряет фокус.
                // Если же мы хотим принудительно указать, что ввод теперь не в TextField1/2/3:
                if (_mainViewModel.ActiveInputTarget == 1)
                {
                    // Это нужно, если LostFocus не сработал или сработал с задержкой.
                    // TextFieldFocusChangedEvent(false) должен был бы уже установить ActiveInputTarget = 0.
                    // Если мы вызовем это здесь снова, это может быть избыточно, но гарантирует состояние.
                     _eventAggregator.GetEvent<TextFieldFocusChangedEvent>().Publish(false);
                }
            }
        }
    }
}

using Prism.Regions; // Required for IRegionManager
using System.Windows;
using System.Windows.Controls; // Required for TextBox, UserControl
using System.Windows.Input;  // Required for InputEventArgs and FocusManager
using System.Windows.Media;  // Required for VisualTreeHelper
using MyPrismApp.ViewModels; // Required for DisabledTextFieldViewModel
using MyPrismApp.Views;    // Required for view types if checking them

namespace MyPrismApp
{
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;
        private readonly string[] _activeInputRegionNames =
        {
            "RegionTextField1",
            "RegionTextField2",
            "RegionTextField3"
        };

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
            this.PreviewTextInput += Window_PreviewTextInput;
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            IInputElement? focusedElement = FocusManager.GetFocusedElement(this);

            if (focusedElement is DependencyObject focusedDependencyObject)
            {
                if (IsFocusInActiveInputRegions(focusedDependencyObject))
                {
                    // Фокус на TextBox внутри одного из активных регионов, ничего не делаем.
                    return;
                }
            }

            // Фокус не в активном регионе или focusedElement is null
            // Добавляем текст в DisabledTextFieldViewModel
            var disabledViewRegion = _regionManager.Regions.ContainsRegionWithName("RegionDisabledTextField")
                                     ? _regionManager.Regions["RegionDisabledTextField"]
                                     : null;
            if (disabledViewRegion != null)
            {
                // Получаем активное View в регионе. Это должен быть DisabledTextFieldView.
                var disabledView = disabledViewRegion.ActiveViews.FirstOrDefault() as FrameworkElement; // UserControl / DisabledTextFieldView
                if (disabledView != null && disabledView.DataContext is DisabledTextFieldViewModel disabledViewModel)
                {
                    disabledViewModel.AppendText(e.Text);
                    e.Handled = true;
                }
            }
        }

        private bool IsFocusInActiveInputRegions(DependencyObject focusedElement)
        {
            // Проходим вверх по визуальному дереву от элемента с фокусом
            DependencyObject? currentElement = focusedElement;

            // Мы ищем TextBox внутри одного из наших UserControls (TextFieldView1,2,3)
            // Сначала убедимся, что сам элемент с фокусом - это TextBox
            if (!(currentElement is TextBox))
            {
                // Если фокус не на TextBox, то он точно не в одном из наших активных TextBox'ов
                // Однако, он может быть на самом UserControl, если тот как-то может получать фокус.
                // Для простоты, будем считать, что нас интересует только фокус на TextBox внутри UserControl.
                // Если currentElement это UserControl, то надо проверить его родительский регион.
            }

            while (currentElement != null)
            {
                // Проверяем, является ли текущий элемент ContentControl, который хостит регион
                if (currentElement is ContentControl regionHost)
                {
                    string? regionName = RegionManager.GetRegionName(regionHost);
                    if (regionName != null && _activeInputRegionNames.Contains(regionName))
                    {
                        // Элемент с фокусом (или его родитель TextBox) находится внутри ContentControl,
                        // который является одним из наших активных регионов.
                        // Теперь нужно убедиться, что сам focusedElement (или его родитель, если focusedElement - часть TextBox)
                        // является TextBox. Это можно сделать более строгой проверкой,
                        // убедившись, что focusedElement является потомком UserControl (TextFieldView1/2/3),
                        // который в свою очередь содержит TextBox.

                        // Для упрощения: если мы нашли родительский регион, который активен,
                        // и изначальный focusedElement был TextBox (или его часть), то считаем, что это наш случай.
                        // focusedElement уже проверен (или должен быть проверен более тщательно) на то, что это TextBox.
                        // Если focusedElement - это, например, сам UserControl (TextFieldView1), то currentElement == regionHost не будет,
                        // т.к. UserControl сам загружен ВНУТРЬ regionHost.

                        // Более точная проверка:
                        // 1. Найти родительский UserControl для focusedElement.
                        // 2. Проверить тип этого UserControl (TextFieldView1, TextFieldView2, TextFieldView3).
                        // 3. Если тип совпадает, то фокус в активном регионе.

                        var parentUserControl = FindParent<UserControl>(focusedElement);
                        if (parentUserControl is TextFieldView1 ||
                            parentUserControl is TextFieldView2 ||
                            parentUserControl is TextFieldView3)
                        {
                             // И при этом этот UserControl должен быть внутри одного из активных регионов
                             // Это уже проверяется через regionHost и regionName выше, если currentElement дошел до ContentControl региона
                             // Но currentElement (regionHost) это родитель для parentUserControl.
                             // Эта логика становится сложной.

                            // Упрощенный подход: если focusedElement это TextBox, и его родительский регион - активный.
                            if (focusedElement is TextBox) return true;

                            // Если сам focusedElement - это UserControl одного из нужных типов.
                            // Это менее вероятно, т.к. фокус обычно на TextBox внутри UserControl.
                            // if (parentUserControl != null && regionHost == GetParentRegionHost(parentUserControl)) return true;

                        }
                         // Если мы дошли до ContentControl, который является одним из наших активных регионов,
                         // и focusedElement (который должен быть TextBox или его частью) находится внутри этого региона.
                         return true;
                    }
                }
                currentElement = VisualTreeHelper.GetParent(currentElement);
            }
            return false;
        }

        // Вспомогательный метод для поиска родителя определенного типа
        public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject? parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T? parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}

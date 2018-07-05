using Recipes.Interfaces;
using System.Windows;

namespace Recipes {
    public partial class RecipesMainWindow : Window, IRecipesView {
        public RecipesMainWindow() {
            InitializeComponent();
        }

        public void BindDataContext(IRecipesViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public MessageBoxResult ConfirmOperation(string text, string caption) {
            return MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void ListBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.Receipes.SelectedItem = null;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }
    }
}

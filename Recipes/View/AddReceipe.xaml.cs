using Recipes.Interfaces;
using System.Windows;

namespace Recipes.View {
    public partial class AddReceipe : Window, IAddReceipeView {
        public AddReceipe() {
            InitializeComponent();
        }

        public void BindDataContext(IAddReceipeViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
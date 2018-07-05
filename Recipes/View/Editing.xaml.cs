using Recipes.Interfaces;
using System.Windows;

namespace Recipes.View {
    public partial class Editing : Window, IEditingView {
        public Editing() {
            InitializeComponent();
        }

        public void BindDataContext(IEditingViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}
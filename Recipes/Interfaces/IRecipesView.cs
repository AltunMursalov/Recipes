using System.Windows;

namespace Recipes.Interfaces {
    public interface IRecipesView {
        void BindDataContext(IRecipesViewModel viewModel);
        void ShowAlert(string text, string caption);
        MessageBoxResult ConfirmOperation(string text, string caption);
    }
}

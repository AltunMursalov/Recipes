namespace Recipes.Interfaces {
    public interface IAddReceipeView {
        void BindDataContext(IAddReceipeViewModel viewModel);
        void ShowAlert(string text, string caption);
        bool? ShowDialog();
    }
}
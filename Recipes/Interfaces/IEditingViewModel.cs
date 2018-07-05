using System.Windows.Input;

namespace Recipes.Interfaces {
    public interface IEditingViewModel {
        IEditingView View { get; }
        ICommand Confirm { get; }
        ICommand Cancel { get; }
    }
}
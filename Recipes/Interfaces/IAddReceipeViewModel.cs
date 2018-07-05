using System.Windows.Input;

namespace Recipes.Interfaces {
    public interface IAddReceipeViewModel {
        IAddReceipeView View { get; }
        ICommand AddIngredient { get; }
    }
}
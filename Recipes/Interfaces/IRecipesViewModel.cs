using Recipes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Recipes.Interfaces {
    public interface IRecipesViewModel {
        IRecipesView View { get; }
        ObservableCollection<Ingredient> SelectedIngredients { get; set; }
        ObservableCollection<Receipe> Recipes { get; set; }
        Receipe SelectedReciep { get; set; }
        ICommand Remove { get; }
        ICommand Edit { get; }
        ICommand Add { get; }
    }
}
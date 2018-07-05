using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Interfaces {
    public interface IDataService {
        bool AddReciepe(Receipe receipe, Ingredient ingredient);
        IEnumerable<Receipe> GetRecipes();
        IEnumerable<Ingredient> GetSelectedReciepeIngredients(Receipe reciep);
        bool DeleteReciep(Receipe reciep);
        void AddIngredient(Ingredient ingredient);
        IEnumerable<Unit> GetUnits();
        IEnumerable<Ingredient> GetIngredients();
        bool UpdateReceipe(Receipe receipe);
        IEnumerable<Receipe> SortByPrepareTime();
        IEnumerable<Receipe> SortedByAlphabet();
        IEnumerable<Receipe> GetFilteredReceipes(IList<Ingredient> ingredients);
    }
}
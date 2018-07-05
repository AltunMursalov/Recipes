using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Interfaces {
    public interface IRecipesRepository : IRepository {
        IEnumerable<Receipe> GetRecipes();
        bool RemoveReciep(Receipe reciep);
        bool AddReceipe(Receipe receipe, Ingredient ingredient);
        bool Update(Receipe receipe);
        IEnumerable<Receipe> SortByTitle();
        IEnumerable<Receipe> SortByPrepareTime();
        IEnumerable<Receipe> FilterReceipes(IEnumerable<Ingredient> ingredients);
    }
}
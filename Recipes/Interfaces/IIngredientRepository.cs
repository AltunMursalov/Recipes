using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Interfaces {
    public interface IIngredientRepository : IRepository {
        IEnumerable<Ingredient> GetReciepIngredients(Receipe reciep);
        void AddIngredient(Ingredient ingredient);
        IEnumerable<Ingredient> GetIngredients();
    }
}
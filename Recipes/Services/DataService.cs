using Recipes.Interfaces;
using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Services {
    public class DataService : IDataService {
        private readonly IRecipesRepository recipesRepository;
        private readonly IUnitRepository unitRepository;
        private readonly IIngredientRepository ingredientRepository;
        public DataService(IRecipesRepository recipesRepository, IIngredientRepository ingredientRepository, IUnitRepository unitRepository) {
            this.unitRepository = unitRepository;
            this.recipesRepository = recipesRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Receipe> GetRecipes() {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.GetRecipes();
            this.recipesRepository.CloseConnection();
            return result;
        }

        public bool UpdateReceipe(Receipe receipe) {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.Update(receipe);
            this.recipesRepository.CloseConnection();
            return result;
        }

        public IEnumerable<Receipe> SortedByAlphabet() {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.SortByTitle();
            this.recipesRepository.CloseConnection();
            return result;
        }

        public IEnumerable<Receipe> SortByPrepareTime() {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.SortByPrepareTime();
            this.recipesRepository.CloseConnection();
            return result;
        }

        public IEnumerable<Unit> GetUnits() {
            this.unitRepository.OpenConnection();
            var result = unitRepository.GetUnits();
            return result;
        }

        public IEnumerable<Receipe> GetFilteredReceipes(IList<Ingredient> ingredients) {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.FilterReceipes(ingredients);
            this.recipesRepository.CloseConnection();
            return result;
        }

        public IEnumerable<Ingredient> GetIngredients() {
            this.ingredientRepository.OpenConnection();
            var result = this.ingredientRepository.GetIngredients();
            this.ingredientRepository.CloseConnection();
            return result;
        }

        public IEnumerable<Ingredient> GetSelectedReciepeIngredients(Receipe reciep) {
            this.ingredientRepository.OpenConnection();
            var result = ingredientRepository.GetReciepIngredients(reciep);
            this.ingredientRepository.CloseConnection();
            return result;
        }

        public bool DeleteReciep(Receipe reciep) {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.RemoveReciep(reciep);
            this.recipesRepository.CloseConnection();
            return result;
        }

        public void AddIngredient(Ingredient ingredient) {
            this.ingredientRepository.OpenConnection();
            this.ingredientRepository.AddIngredient(ingredient);
            this.ingredientRepository.CloseConnection();
        }

        public bool AddReciepe(Receipe receipe, Ingredient ingredient) {
            this.recipesRepository.OpenConnection();
            var result = this.recipesRepository.AddReceipe(receipe, ingredient);
            this.recipesRepository.CloseConnection();
            return result;
        }
    }
}
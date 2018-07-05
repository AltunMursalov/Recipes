using Dapper;
using Recipes.Interfaces;
using Recipes.Model;
using Dapper.FastCrud;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace Recipes.Repositories {
    public class IngredientRepository : IIngredientRepository {
        private DbConnection connection;
        private DbProviderFactory factory;

        public void CloseConnection() {
            if (this.connection != null)
                this.connection.Close();
        }

        public IEnumerable<Ingredient> GetReciepIngredients(Receipe reciep) {
            return connection.Query<Ingredient>("SELECT * FROM ReceipesIngredients JOIN Ingredients ON Ingredients.Id = ReceipesIngredients.IngredientId JOIN Units " +
                "ON Units.Id = Ingredients.UnitId WHERE ReceipesIngredients.ReceipeId = @Id", param: reciep);
        }

        public void OpenConnection() {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed) {
                this.factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["My Connection"].ProviderName);
                this.connection = factory.CreateConnection();
                this.connection.ConnectionString = ConfigurationManager.ConnectionStrings["My Connection"].ConnectionString;
                this.connection.Open();
            }
        }

        public IEnumerable<Ingredient> GetIngredients() {
            return connection.Query<Ingredient>("SELECT * FROM Ingredients");
        }

        public void AddIngredient(Ingredient ingredient) {
            if (ingredient.Id == 0) {
                string query = "INSERT INTO Ingredients (Ingredient, UnitId) VALUES (@IngrentName, @UnitId)";
                this.connection.Query<Ingredient>(query, param: ingredient);
            }
        }
    }
}
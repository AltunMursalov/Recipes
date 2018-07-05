using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Dapper.FastCrud;
using Dapper;
using Recipes.Interfaces;
using Recipes.Model;

namespace Recipes.Repositories {
    public class RecipesRepository : IRecipesRepository {
        private DbConnection connection;
        private DbProviderFactory factory;

        public void CloseConnection() {
            if (this.connection != null)
                this.connection.Close();
        }

        public IEnumerable<Receipe> GetRecipes() {
            return connection.Query<Receipe>("SELECT * FROM Receipes");
        }

        public void OpenConnection() {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed) {
                this.factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["My Connection"].ProviderName);
                this.connection = factory.CreateConnection();
                this.connection.ConnectionString = ConfigurationManager.ConnectionStrings["My Connection"].ConnectionString;
                this.connection.Open();
            }
        }

        public bool RemoveReciep(Receipe reciep) {
            string query = "DELETE FROM ReceipesIngredients WHERE ReceipeId = @Id";
            this.connection.Query<Receipe>(query, param: reciep);
            return this.connection.Delete<Receipe>(reciep);
        }

        public IEnumerable<Receipe> FilterReceipes(IEnumerable<Ingredient> ingredients) {
            var parametres = new DynamicParameters();
            foreach (Ingredient item in ingredients) {
                parametres.Add("ingredients", item.IngrentName, System.Data.DbType.String, null, null);
            }
            string query = "SELECT * FROM (SELECT Receipes.Id as Id, Title, PrepareTime, [Description], Note, ReceipeId, IngredientId, Quantity, Ingredients.Id as IngId, Ingredient, " +
                "UnitId FROM Receipes JOIN ReceipesIngredients ON Receipes.Id = ReceipesIngredients.ReceipeId JOIN Ingredients ON Ingredients.Id = ReceipesIngredients.IngredientId) AS rec " +
                "WHERE rec.Ingredient IN (@ingredients)";
            return this.connection.Query<Receipe>(query, parametres );
        }

        public bool AddReceipe(Receipe receipe, Ingredient ingredient) {
            try {
                var parametres = new DynamicParameters();
                parametres.Add("title", receipe.Title, System.Data.DbType.String);
                parametres.Add("description", receipe.Description, System.Data.DbType.String);
                parametres.Add("prepareTime", receipe.PrepareTime, System.Data.DbType.Time);
                parametres.Add("note", receipe.Note, System.Data.DbType.String);
                parametres.Add("ingredient", ingredient.IngrentName, System.Data.DbType.String);
                parametres.Add("unitId", ingredient.UnitId, System.Data.DbType.Int32);
                parametres.Add("receipeId", receipe.Id, System.Data.DbType.Int32);
                parametres.Add("ingredientId", ingredient.Id, System.Data.DbType.Int32);
                parametres.Add("quantity", ingredient.Quantity, System.Data.DbType.Int32);
                this.connection.Execute("AddReceipe", parametres, commandType: System.Data.CommandType.StoredProcedure);
                return true;
            }
            catch (DbException) {
                return false;
            }
        }

        public bool Update(Receipe receipe) {
            return this.connection.Update<Receipe>(receipe);
        }

        public IEnumerable<Receipe> SortByTitle() {
            string query = "SELECT * FROM Receipes ORDER BY Title";
            return this.connection.Query<Receipe>(query);
        }

        public IEnumerable<Receipe> SortByPrepareTime() {
            string query = "SELECT * FROM Receipes ORDER BY PrepareTime";
            return this.connection.Query<Receipe>(query);
        }
    }
}
using Dapper;
using Recipes.Interfaces;
using Recipes.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace Recipes.Repositories {
    public class UnitRepository : IUnitRepository {
        private DbConnection connection;
        private DbProviderFactory factory;

        public void CloseConnection() {
            if (this.connection != null)
                this.connection.Close();
        }

        public IEnumerable<Unit> GetUnits() {
            return connection.Query<Unit>("SELECT * FROM Units");
        }

        public void OpenConnection() {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed) {
                this.factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["My Connection"].ProviderName);
                this.connection = factory.CreateConnection();
                this.connection.ConnectionString = ConfigurationManager.ConnectionStrings["My Connection"].ConnectionString;
                this.connection.Open();
            }
        }
    }
}
namespace Recipes.Interfaces {
    public interface IRepository {
        void CloseConnection();
        void OpenConnection();
    }
}
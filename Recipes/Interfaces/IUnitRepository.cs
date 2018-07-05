using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Interfaces {
    public interface IUnitRepository : IRepository{
        IEnumerable<Unit> GetUnits();
    }
}
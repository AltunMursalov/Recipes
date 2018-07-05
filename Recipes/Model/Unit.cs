using Recipes.Common;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Model {
    public class Unit : NotifyableObject {
        private int id;
        [Key]
        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string unit;
        public string UnitName {
            get { return unit; }
            set { unit = value; base.OnChanged(); }
        }
    }
}
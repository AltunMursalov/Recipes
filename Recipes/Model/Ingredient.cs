using Recipes.Common;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Model {
    public class Ingredient : NotifyableObject {
        private int id;
        [Key]
        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string ingredient;

        public string IngrentName {
            get { return ingredient; }
            set { ingredient = value; base.OnChanged(); }
        }

        private int unitId;

        public int UnitId {
            get { return unitId; }
            set { unitId = value; base.OnChanged(); }
        }

        private string unit;
        public string Unit {
            get { return unit; }
            set { unit = value; base.OnChanged(); }
        }

        private int quantity;
        public int Quantity {
            get { return quantity; }
            set { quantity = value; base.OnChanged(); }
        }
    }
}
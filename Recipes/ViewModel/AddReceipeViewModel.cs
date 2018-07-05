using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Musarium.Common;
using Recipes.Common;
using Recipes.Interfaces;
using Recipes.Model;

namespace Recipes.ViewModel {
    public class AddReceipeViewModel : NotifyableObject, IAddReceipeViewModel {
        private readonly IDataService dataService;
        public IAddReceipeView View { get; private set; }
        private Ingredient ingredient;
        public Ingredient Ingredient {
            get => ingredient;
            set { ingredient = value; base.OnChanged(); }
        }
        public ObservableCollection<Unit> Units { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        private Receipe receipe;
        public Receipe Receipe {
            get { return receipe; }
            set { receipe = value; base.OnChanged(); }
        }

        private Unit unit;
        public Unit Unit {
            get { return unit; }
            set { unit = value; base.OnChanged(); this.Ingredient.Unit = value.UnitName; this.Ingredient.UnitId = value.Id; }
        }


        private ICommand addIngredient;
        public ICommand AddIngredient {
            get {
                if (this.addIngredient is null) {
                    this.addIngredient = new RelayCommand(
                        (param) => {
                            var result = dataService.AddReciepe(this.Receipe, this.Ingredient);
                            if (result)
                                this.View.ShowAlert("Reciepe added!", "INFO");
                            else
                                this.View.ShowAlert("You are must to fill all fields!", "Error");
                        },
                        (param) => { return true; }
                    );
                }
                return this.addIngredient;
            }
        }

        public AddReceipeViewModel(IAddReceipeView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
            this.Units = new ObservableCollection<Unit>();
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.Ingredient = new Ingredient();
            this.Receipe = new Receipe();
            this.Unit = new Unit();

            this.DataFill();
        }

        private void DataFill() {
            var units = dataService.GetUnits();
            foreach (Unit unit in units) {
                this.Units.Add(unit);
            }

            var ingredients = dataService.GetIngredients();
            foreach (Ingredient ingredient in ingredients) {
                this.Ingredients.Add(ingredient);
            }
        }
    }
}
using Recipes.Interfaces;
using Recipes.Common;
using System.Collections.ObjectModel;
using Recipes.Model;
using Autofac;
using System.Windows.Input;
using System.Linq;
using Musarium.Common;
using System.Collections.Generic;

namespace Recipes.ViewModel {
    public class RecipesViewModel : NotifyableObject, IRecipesViewModel {
        private readonly IDataService dataService;
        public ObservableCollection<Receipe> Recipes { get; set; }
        public ObservableCollection<Ingredient> SelectedIngredients { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<string> Orders { get; set; }
        public List<Ingredient> FilterIngredients { get; set; }

        private Receipe selectedReciep;
        public Receipe SelectedReciep {
            get { return selectedReciep; }
            set {
                selectedReciep = value;
                base.OnChanged();
                var ingredients = dataService.GetSelectedReciepeIngredients(value);
                this.SelectedIngredients.Clear();
                foreach (Ingredient ingredient in ingredients) {
                    this.SelectedIngredients.Add(ingredient);
                }
            }
        }

        private string sort;
        public string Sort {
            get { return sort; }
            set {
                sort = value;
                base.OnChanged();
                if (value == "Alphabet") {
                    var receipes = this.Recipes.OrderBy(r => r.Title).ToList();
                    this.Recipes.Clear();
                    foreach (Receipe receipe in receipes) {
                        this.Recipes.Add(receipe);
                    }
                } else if (value == "Prepare Time") {
                    var receipes = this.Recipes.OrderBy(r => r.PrepareTime).ToList();
                    this.Recipes.Clear();
                    foreach (Receipe receipe in receipes) {
                        this.Recipes.Add(receipe);
                    }
                }
            }
        }

        public IRecipesView View { get; private set; }

        private ICommand remove;
        public ICommand Remove {
            get {
                if (this.remove is null) {
                    this.remove = new RelayCommand(
                        (param) => {
                            var confirm = this.View.ConfirmOperation("Do you really want to remove reciep?", "Question");
                            if (confirm == System.Windows.MessageBoxResult.Yes) {
                                var result = dataService.DeleteReciep(this.SelectedReciep);
                                if (result != false) {
                                    this.View.ShowAlert("Successfuly deleted!", "INFO");
                                } else {
                                    this.View.ShowAlert("Something is wrong!", "???");
                                }
                            }
                        },
                        (param) => { return SelectedReciep?.Id != 0 && SelectedReciep != null; }
                    );
                }
                return this.remove;
            }
        }

        private ICommand filter;
        public ICommand Filter {
            get {
                if (this.filter is null) {
                    this.filter = new RelayCommand(
                        (param) => {
                            var receipes = dataService.GetFilteredReceipes(FilterIngredients);
                            this.Recipes.Clear();
                            foreach (var item in receipes) {
                                this.Recipes.Add(item);
                            }
                            this.Sort = "";
                        },
                        (param) => { return true; }
                    );
                }
                return this.filter;
            }
        }

        private ICommand edit;
        public ICommand Edit {
            get {
                if (this.edit is null) {
                    this.edit = new RelayCommand(
                        (param) => {
                            var editing = App.Container.ResolveOptional<IEditingViewModel>(new TypedParameter(typeof(Receipe), this.SelectedReciep));
                            editing.View.ShowDialog();
                        },
                        (param) => { return SelectedReciep?.Id != 0 && SelectedReciep != null; }
                    );
                }
                return this.edit;
            }
        }

        private ICommand add;
        public ICommand Add {
            get {
                if (this.add is null) {
                    this.add = new RelayCommand(
                        (param) => {
                            var addViewModel = App.Container.Resolve<IAddReceipeViewModel>();
                            addViewModel.View.ShowDialog();
                        },
                        (param) => { return true; }
                    );
                }
                return this.add;
            }
        }

        public RecipesViewModel(IRecipesView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
            this.Recipes = new ObservableCollection<Receipe>();
            this.SelectedIngredients = new ObservableCollection<Ingredient>();
            this.SelectedReciep = new Receipe();
            this.Orders = new ObservableCollection<string> {
                "Alphabet",
                "Prepare Time",
                ""
            };
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.FilterIngredients = new List<Ingredient>();

            this.FillData();
        }

        private void FillData() {
            var receipes = dataService.GetRecipes();
            if (receipes != null) {
                foreach (Receipe reciep in receipes) {
                    this.Recipes.Add(reciep);
                }
            } else {
                this.View.ShowAlert("Recieps is empty!", "INFO");
            }

            var ingredients = dataService.GetIngredients();
            foreach (Ingredient ingredient in ingredients) {
                this.Ingredients.Add(ingredient);
            }
        }
    }
}
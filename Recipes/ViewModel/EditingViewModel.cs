using Musarium.Common;
using Recipes.Common;
using Recipes.Interfaces;
using Recipes.Model;
using System;
using System.Windows.Input;

namespace Recipes.ViewModel {
    public class EditingViewModel : NotifyableObject, IEditingViewModel {
        private readonly IDataService dataService;
        public IEditingView View { get; private set; }

        private Receipe receipe;
        public Receipe Receipe {
            get { return receipe; }
            set { receipe = value; base.OnChanged(); }
        }

        public EditingViewModel(IEditingView view, Receipe receipe, IDataService dataService) {
            this.Receipe = receipe;
            this.dataService = dataService;
            this.View = view;
            this.View.BindDataContext(this);
        }

        private ICommand ok;
        public ICommand Confirm {
            get {
                if (this.ok is null) {
                    this.ok = new RelayCommand(
                        (param) => {
                            var result = this.dataService.UpdateReceipe(this.Receipe);
                            if (result) {
                                this.View.ShowAlert("Edited", "INFO");
                                this.View.Hide();
                            } else {
                                this.View.ShowAlert("Something is wrong, but what is wrong?", "???");
                            }
                        },
                        (param) => { return Chek(); }
                    );
                }
                return this.ok;
            }
        }

        private bool Chek() {
            if (this.Receipe.Title != "" && this.Receipe.Note != "" && Receipe.PrepareTime.TotalSeconds > 0)
                return true;
            else
                return false;
        }

        private ICommand cancel;
        public ICommand Cancel {
            get {
                if (this.cancel is null) {
                    this.cancel = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                        },
                        (param) => { return true; }
                    );
                }
                return this.cancel;
            }
        }
    }
}

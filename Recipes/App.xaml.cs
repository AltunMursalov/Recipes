using Autofac;
using Recipes.Interfaces;
using Recipes.Repositories;
using Recipes.Services;
using Recipes.View;
using Recipes.ViewModel;
using System.Windows;

namespace Recipes {
    public partial class App : Application {
        public static IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            #region Autofac
            var builder = new ContainerBuilder();
            #region Views
            builder.RegisterType<RecipesMainWindow>().As<IRecipesView>();
            builder.RegisterType<AddReceipe>().As<IAddReceipeView>();
            builder.RegisterType<Editing>().As<IEditingView>();
            #endregion
            #region ViewModels
            builder.RegisterType<RecipesViewModel>().As<IRecipesViewModel>();
            builder.RegisterType<EditingViewModel>().As<IEditingViewModel>();
            builder.RegisterType<AddReceipeViewModel>().As<IAddReceipeViewModel>().SingleInstance();
            #endregion
            #region Repositories
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
            builder.RegisterType<RecipesRepository>().As<IRecipesRepository>();
            builder.RegisterType<UnitRepository>().As<IUnitRepository>();
            #endregion
            #region Services
            builder.RegisterType<DataService>().As<IDataService>();
            #endregion
            Container = builder.Build();
            #endregion

            var viewModel = Container.Resolve<IRecipesViewModel>();
            var view = viewModel.View as Window;
            this.MainWindow = view;
            this.MainWindow.Show();
        }
    }
}
namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Collections.Generic;
    using BakeSchoolAdmin_DatabaseConnection.Services;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.Views;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// the main window for the recipes.
    /// </summary>
    internal class MainRecipesViewModel : ViewModelBase
    {
        /// <summary>
        /// View that is currently bound to the main ContentControl..
        /// </summary>
        private UserControl currentMainRecipeView;

        /// <summary>
        /// Observable Collection for the recipes..
        /// </summary>
        private ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainRecipesViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public MainRecipesViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.InitRecipe();

            // subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentMainDataEvent>().Subscribe(this.ChangeCurrentMainView, ThreadOption.UIThread);

            // open the view with all recipes and home View from recipes!
            RecipeHomeViewModel recipeMainViewModel = new RecipeHomeViewModel(EventAggregator, this.recipes);
            RecipeHomeView recipeHomeView = new RecipeHomeView();

            recipeHomeView.DataContext = recipeMainViewModel;
            this.CurrentMainRecipeView = recipeHomeView;

            // subscribe to event
            this.EventAggregator.GetEvent<ChangeRecipeCurrentMainViewEvent>().Subscribe(this.ChangeRecipeCurrentMainView, ThreadOption.UIThread);
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the main ContentControl..
        /// </summary>
        public UserControl CurrentMainRecipeView
        {
            get
            {
                return this.currentMainRecipeView;
            }

            set
            {
                if (this.currentMainRecipeView != value)
                {
                    this.currentMainRecipeView = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentMainRecipeView));
                }
            }
        }

        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="user">The user<see cref="UserControl"/>.</param>
        public void ChangeRecipeCurrentMainView(UserControl user)
        {
            this.CurrentMainRecipeView = user;
            this.OnPropertyChanged(nameof(this.CurrentMainRecipeView));
        }

        /// <summary>
        /// The ChangeCurrentMainView.
        /// </summary>
        /// <param name="main">The main<see cref="UserControl"/>.</param>
        public void ChangeCurrentMainView(UserControl main)
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// load the current description data into the description field.
        /// </summary>
        internal void InitRecipe()
        {
            RecipeService recipeService = new RecipeService();
            if (recipeService.init())
            {
                IList<Recipe> recipedata = recipeService.ReadData();
                this.recipes = recipeService.GetRecipesObs(recipedata);
            }
        }
    }
}

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit;
    using BakeSchoolAdmin_Gui.Views.Recipes;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeMainListViewModel" />.
    /// </summary>
    internal class RecipeMainListViewModel : ViewModelBase
    {
        /// <summary>
        /// Create the selected Recipe form the current button click..
        /// </summary>
        public Recipe SelectedRecipe;

        /// <summary>
        /// The id of the recipe.
        /// </summary>
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeMainListViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipes">The recipes<see cref="ObservableCollection{Recipe}"/>.</param>
        public RecipeMainListViewModel(IEventAggregator eventAggregator, ObservableCollection<Recipe> recipes) : base(eventAggregator)
        {
            // load the recipes 
            this.LoadRecipes(recipes);

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.RecipeViewCommand = new ActionCommand(this.RecipeViewCommandExecute, this.RecipeViewCommandCanExecute);

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.RecipesAddCommand = new ActionCommand(this.RecipeAddCommandExecute, this.RecipeAddCommandCanExecute);
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value != -1)
                {
                    this.id = value;
                    this.OnPropertyChanged(nameof(this.id));
                }
            }
        }

        /// <summary>
        /// Gets the Category  id (Only Test)..
        /// </summary>
        public ICommand RecipeViewCommand { get; private set; }

        /// <summary>
        /// Gets the RecipesAddCommand.
        /// </summary>
        public ICommand RecipesAddCommand { get; private set; }

        /// <summary>
        /// Gets or sets the list with all categories..
        /// </summary>
        public ObservableCollection<Recipe> Recipes { get; set; }

        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="user">The user<see cref="UserControl"/>.</param>
        public void ChangeRecipeCurrentMainView(UserControl user) => throw new Exception("Change recipe current view");

        /// <summary>
        /// The LoadRecipes.
        /// </summary>
        /// <param name="recipes">The recipes<see cref="ObservableCollection{Recipe}"/>.</param>
        private void LoadRecipes(ObservableCollection<Recipe> recipes)
        {
            this.Recipes = recipes;
        }

        /// <summary>
        /// Determines if the edit the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool RecipeAddCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void RecipeAddCommandExecute(object parameter)
        {
            this.Id = this.Recipes.Count + 1;
            MainAddRecipes mainAddRecipes = new MainAddRecipes();
            RecipesAddMainViewModel recipesAddMainViewModel = new RecipesAddMainViewModel(EventAggregator, id);
            mainAddRecipes.DataContext = recipesAddMainViewModel;

            this.EventAggregator.GetEvent<ChangeRecipeCurrentMainViewEvent>().Publish(mainAddRecipes);
        }

        /// <summary>
        /// Determines if the edit the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool RecipeViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void RecipeViewCommandExecute(object parameter)
        {
            RecipeMainDetailView details = new RecipeMainDetailView();
            RecipeMainDetailViewModel categoryEditViewModel = new RecipeMainDetailViewModel(EventAggregator);
            details.DataContext = categoryEditViewModel;
           
            // Change the current view Right, if the event is fired.
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(details);

            foreach (Recipe recipe in this.Recipes)
            {
                if (recipe.Number == (int)parameter)
                {
                    this.SelectedRecipe = recipe;

                    this.EventAggregator.GetEvent<SelectedRecipeDataEvent>().Publish(this.SelectedRecipe);
                }
            }
        }
    }
}

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events.RecipeAddEvents;
    using BakeSchoolAdmin_Gui.Views.Recipes;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using Microsoft.Practices.Prism.Events;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="RecipesAddMainViewModel" />.
    /// </summary>
    internal class RecipesAddMainViewModel : ViewModelBase
    {
        #region ------------------------------------------------------------------ private fields ----------------------------------------
        /// <summary>
        /// View that is currently bound to the left ContentControl..
        /// </summary>
        private UserControl currentViewMain;

        /// <summary>
        /// Store the recipe id.
        /// </summary>
        private int recipeId;
        #endregion

        #region ------------------------------------------------------------------ Constructor ----------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipesAddMainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>Event aggregator</param>
        /// <param name="id">recipe id</param>
        public RecipesAddMainViewModel(IEventAggregator eventAggregator, int id) : base(eventAggregator)
        {
            RecipeEditAddIngredient view = new RecipeEditAddIngredient();
            RecipeEditAddIngredientViewModel model = new RecipeEditAddIngredientViewModel(EventAggregator, this.CurrentRecipe);
            view.DataContext = model;
            this.currentViewMain = view;

            this.recipeId = id;

            this.OpenDescription = new ActionCommand(this.ShowDescriptionViewCommandExecute, this.ShowDescriptionViewCommandCanExecute);
            this.OpenCheck = new ActionCommand(this.ShowCheckViewCommandExecute, this.ShowCheckViewCommandCanExecute);
            this.OpenIngredient = new ActionCommand(this.ShowIngredientsViewCommandExecute, this.ShowIngriedentsCommandCanExecute);

            this.EventAggregator.GetEvent<ReloadCurrentCheckRecipeEventData>().Subscribe(this.LoadRecipe, ThreadOption.UIThread);
        }
        #endregion

        /// <summary>
        /// Gets or sets for the current description.
        /// </summary>
        public List<Description> CurrentDescription { get; set; }

        /// <summary>
        /// Gets or sets for the current ingredient.
        /// </summary>
        public List<Ingredient> CurrentIngredient { get; set; }

        /// <summary>
        /// Gets and sets for the current Recipe.
        /// </summary>
        public Recipe CurrentRecipe { get; private set; }

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewMain
        {
            get
            {
                return this.currentViewMain;
            }

            set
            {
                if (this.currentViewMain != value)
                {
                    this.currentViewMain = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentViewMain));
                }
            }
        }

        /// <summary>
        /// Gets the OpenDescription
        /// execute  the Command show the description form the recipes..
        /// </summary>
        public ICommand OpenDescription { get; private set; }

        /// <summary>
        /// Gets the OpenIngredient
        /// execute  the Command show the recipes form the recipes..
        /// </summary>
        public ICommand OpenIngredient { get; private set; }

        /// <summary>
        /// Gets the OpenCheck
        /// execute  the Command show the check form the recipes..
        /// </summary>
        public ICommand OpenCheck { get; private set; }

        #region ---------------------------------------------- Events Methods --------------------------
        /// <summary>
        /// load the recipe in the current recipe data and check whether it contains description and recipe
        /// </summary>
        /// <param name="currentrecipe"> current recipe</param>
        public void LoadRecipe(Recipe currentrecipe)
        {
            if (this.CurrentRecipe == null)
            {
                this.CurrentRecipe = currentrecipe;
            }

            if (currentrecipe.Ingredients != null)
            {
                this.CurrentIngredient = currentrecipe.Ingredients;            
                this.CurrentRecipe.Number = this.recipeId;
                this.CurrentRecipe.Name = currentrecipe.Name;
            }

            if (currentrecipe.Descriptions != null)
            {
                this.CurrentDescription = currentrecipe.Descriptions;
            }
        }
        #endregion

        #region ---------------------------------------------- Command Methods --------------------------
        /// <summary>
        /// Determines if the view is correct and we can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ShowDescriptionViewCommandCanExecute(object parameter)
        {
            if (this.CurrentRecipe != null)
            {
                if (!string.IsNullOrEmpty(this.CurrentRecipe.Name))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void ShowDescriptionViewCommandExecute(object parameter)
        {
            RecipeEditAddDescription recipeEditAddDescription = new RecipeEditAddDescription();
            RecipeEditAddDescriptionViewModel recipeEditAddDescriptionViewModel = new RecipeEditAddDescriptionViewModel(EventAggregator, this.CurrentRecipe);
            recipeEditAddDescription.DataContext = recipeEditAddDescriptionViewModel;
            this.currentViewMain = recipeEditAddDescription;
            this.OnPropertyChanged(nameof(this.CurrentViewMain));
        }

        /// <summary>
        /// Determines if the view is correct and we can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ShowCheckViewCommandCanExecute(object parameter)
        {
            if (this.CurrentIngredient != null && this.CurrentDescription != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void ShowCheckViewCommandExecute(object parameter)
        {
            Recipe recipe = new Recipe();
            
            recipe.Descriptions = this.CurrentDescription;
            recipe.Ingredients = this.CurrentIngredient;
            recipe.Number = this.recipeId;
            recipe.Name = this.CurrentRecipe.Name;

            this.CurrentRecipe = recipe;

            RecipeEditCheckRecipe recipeEditCheck = new RecipeEditCheckRecipe();
            RecipeEditCheckRecipeViewModel recipeEditCheckModel = new RecipeEditCheckRecipeViewModel(EventAggregator, this.CurrentRecipe);
            recipeEditCheck.DataContext = recipeEditCheckModel;
            this.currentViewMain = recipeEditCheck;
            this.OnPropertyChanged(nameof(this.CurrentViewMain));
        }

        /// <summary>
        /// Determines if the view is correct and we can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ShowIngriedentsCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void ShowIngredientsViewCommandExecute(object parameter)
        {
            RecipeEditAddIngredient recipeEditIngred = new RecipeEditAddIngredient();
            RecipeEditAddIngredientViewModel recipeEditIngredCheckModel = new RecipeEditAddIngredientViewModel(EventAggregator, this.CurrentRecipe);
            recipeEditIngred.DataContext = recipeEditIngredCheckModel;
            this.currentViewMain = recipeEditIngred;
            this.OnPropertyChanged(nameof(this.CurrentViewMain));
        }
        #endregion
    }
}

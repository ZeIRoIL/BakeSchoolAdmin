using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Gui.Views.Recipes;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    class RecipesAddMainViewModel : ViewModelBase
    {
        #region ======================================== Fields, Constants, Delegates, Events =============================
        /// <summary>
        /// View that is currently bound to the left ContentControl
        /// </summary>
        private UserControl currentViewMain;
        /// <summary>
        /// gets and sets for the currentDes
        /// </summary>
        private Description currentDes;
        /// <summary>
        /// gets and sets for the current Recipe
        /// </summary>
        private Recipe currentRec;
        /// <summary>
        /// gets and sets for the current ingredient
        /// </summary>
        private Ingredient currentingredient;
        #endregion

        #region ======================================== Properties, Indexer ============================================
        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl
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

        #endregion
        public RecipesAddMainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            RecipeEditAddIngredient view = new RecipeEditAddIngredient();
            RecipeEditAddIngredientViewModel model = new RecipeEditAddIngredientViewModel(EventAggregator);
            view.DataContext = model;
            this.currentViewMain = view;

           
            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.OpenDescription = new ActionCommand(this.ShowDescriptionViewCommandExecute, this.ShowDescriptionViewCommandCanExecute);
            // action command if the change button is clicked and the usercontrol (AddCheck)will open.
            this.OpenCheck = new ActionCommand(this.ShowCheckViewCommandExecute, this.ShowCheckViewCommandCanExecute);
            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.OpenIngredient = new ActionCommand(this.ShowIngredientsViewCommandExecute, this.ShowIngriedentsCommandCanExecute);

        }

        #region ======================================== Events =======================================================

        
        #endregion

        #region ======================================== Command ====================================================

        /// <summary>
        /// execute  the Command show the desciption form the recipes
        /// </summary>
        public ICommand OpenDescription { get; private set; }

        /// <summary>
        /// execute  the Command show the recipes form the recipes
        /// </summary>
        public ICommand OpenIngredient { get; private set; }

        /// <summary>
        /// execute  the Command show the check form the recipes
        /// </summary>
        public ICommand OpenCheck { get; private set; }

        #endregion

        #region ======================================== Commands ================================= 

        /// <summary>
        /// Determines if the view is correct and we can be executed
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool ShowDescriptionViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void ShowDescriptionViewCommandExecute(object parameter)
        {
           RecipeEditAddDescription recipeEditAddDescription = new RecipeEditAddDescription();
            RecipeEditAddDescriptionViewModel recipeEditAddDescriptionViewModel = new RecipeEditAddDescriptionViewModel(EventAggregator,currentDes);
            recipeEditAddDescription.DataContext = recipeEditAddDescriptionViewModel;
            this.currentViewMain = recipeEditAddDescription;
            OnPropertyChanged(nameof(this.CurrentViewMain));
        }

        /// <summary>
        /// Determines if the view is correct and we can be executed
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool ShowCheckViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void ShowCheckViewCommandExecute(object parameter)
        {
            RecipeEditCheckRecipe recipeEditCheck = new RecipeEditCheckRecipe();
            RecipeEditCheckRecipeViewModel recipeEditCheckModel = new RecipeEditCheckRecipeViewModel(EventAggregator);
            recipeEditCheck.DataContext = recipeEditCheckModel;
            this.currentViewMain = recipeEditCheck;
            OnPropertyChanged(nameof(this.CurrentViewMain));
        }

        /// <summary>
        /// Determines if the view is correct and we can be executed
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool ShowIngriedentsCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Description button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void ShowIngredientsViewCommandExecute(object parameter)
        {
           RecipeEditAddIngredient recipeEditIngred = new RecipeEditAddIngredient();
            RecipeEditAddIngredientViewModel recipeEditIngredCheckModel = new RecipeEditAddIngredientViewModel(EventAggregator);
            recipeEditIngred.DataContext = recipeEditIngredCheckModel;
            this.currentViewMain = recipeEditIngred;
            OnPropertyChanged(nameof(this.CurrentViewMain));
        }

        #endregion
        
        
    }
}
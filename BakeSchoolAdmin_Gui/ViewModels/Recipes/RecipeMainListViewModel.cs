using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Gui.Views.Recipes;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    class RecipeMainListViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 
        /// <summary>
        /// The id of the category
        /// </summary>
        private int id;
        /// <summary>
        /// Create the selected Recipe form the current button click
        /// </summary>
        public Recipe selectedRecipe;
        #endregion
        public RecipeMainListViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            LoadRecipes();

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.RecipeViewCommand = new ActionCommand(this.RecipeViewCommandExecute, this.RecipeViewCommandCanExecute);
        }
        #region ======================================== Properties/Indexer ================================= 

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
        /// Gets the Category  id (Only Test)
        /// </summary>
        public ICommand RecipeViewCommand { get; private set; }


     

        /// <summary>
        /// Gets or sets the list with all categories
        /// </summary>
        public ObservableCollection<Recipe> Recipes { get; set; }
        #endregion

        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void AddCategory()
        {
            
        }
        #endregion

        #region ======================================== Private Helper ================================= 
        private void LoadRecipes()
        {
            //// init collection and add data
            this.Recipes = new ObservableCollection<Recipe>();

            Recipe recipe = new Recipe();
            recipe.Name = "NameRezept";
            recipe.Number = 1;
            this.Recipes.Add(recipe);
        }
        #endregion
        #region ======================================== Commands ================================= 


        /// <summary>
        /// Determines if the edti the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool RecipeViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void RecipeViewCommandExecute(object parameter)
        {
            UserControl editControl = new UserControl();
            RecipeMainDetailView details = new RecipeMainDetailView();
            RecipeMainDetailViewModel categoryEditViewModel = new RecipeMainDetailViewModel(EventAggregator);
            details.DataContext = categoryEditViewModel;
            editControl = details;

            // Change the current view Right, if the event is fired.
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(editControl);

            foreach (Recipe recipe in this.Recipes)
            {
                if (recipe.Number == (int)parameter)
                {
                    int i = recipe.Number;

                    this.selectedRecipe = recipe;

                    this.EventAggregator.GetEvent<SelectedRecipeDataEvent>().Publish(this.selectedRecipe);
                }
            }
        }

        #endregion




    }
}

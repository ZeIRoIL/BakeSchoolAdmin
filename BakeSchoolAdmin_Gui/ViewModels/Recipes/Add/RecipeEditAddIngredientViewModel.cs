namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events.RecipeAddEvents;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="RecipeEditAddIngredientViewModel" />.
    /// </summary>
    internal class RecipeEditAddIngredientViewModel : MainViewModel
    {
        /// <summary>
        /// the name of the ingredient...
        /// </summary>
        private string name;

        /// <summary>
        /// the data of the ingredient...
        /// </summary>
        private string data;

        /// <summary>
        /// the amount of the ingredient...
        /// </summary>
        private double amount = -1;

        /// <summary>
        /// the unit of the ingredient...
        /// </summary>
        private string unit;

        /// <summary>
        /// the new ingredient.
        /// </summary>
        private Ingredient ingredientNew;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditAddIngredientViewModel"/> class.
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="recipe">the current recipes</param>
        /// </summary>
        public RecipeEditAddIngredientViewModel(IEventAggregator eventAggregator, Recipe recipe) : base(eventAggregator)
        {
            if (recipe != null)
            {
                this.Name = recipe.Name;
                this.OnPropertyChanged(nameof(this.Name));
                
                // convert List to Observable List
                this.LoadIngredients(recipe.Ingredients);
            }
            
            this.SaveIngredient = new ActionCommand(this.AddIngredientCommandExecute, this.AddIngredientCommandCanExecute);

            this.ReloadData();
        }

        /// <summary>
        /// Gets or sets the Unit.
        /// </summary>
        public string Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                if (value != string.Empty)
                {
                    this.unit = value;
                    this.OnPropertyChanged(nameof(this.unit));
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the ingredient...
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != string.Empty)
                {
                    this.name = value;
                    this.OnPropertyChanged(nameof(this.name));
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the ingredient...
        /// </summary>
        public string Data
        {
            get
            {
                return this.data;
            }

            set
            {
                if (value != string.Empty)
                {
                    this.data = value;
                    this.OnPropertyChanged(nameof(this.data));
                }
            }
        }

        /// <summary>
        /// Gets or sets the amount of the ingredient...
        /// </summary>
        public double Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                if (-1 != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged(nameof(this.amount));
                }
            }
        }

        /// <summary>
        /// Gets the Recipe
        /// The recipe which will transfer into the end view if the user clicked on the button "Save"...
        /// </summary>
        public Recipe Recipe { get; private set; }

        /// <summary>
        /// Gets or sets the Ingredients
        /// Get or set the list with all ingredient which user can select....
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets new List ingredient.
        /// </summary>
        public List<Ingredient> IngredientList;

        /// <summary>
        /// Gets the SaveIngredient
        /// execute the command and add the recent ingredient in the ObservableCollection...
        /// </summary>
        public ICommand SaveIngredient { get; private set; }

        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool AddIngredientCommandCanExecute(object parameter)
        {
#warning check whether correct data
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddIngredientCommandExecute(object parameter)
        {
            if (this.name != string.Empty || this.amount != 0 || this.unit != string.Empty)
            {
                this.IngredientList = new List<Ingredient>();

                if (this.Ingredients == null)
                {
                    this.Ingredients = new ObservableCollection<Ingredient>();

                    this.ingredientNew = new Ingredient();
                    this.ingredientNew.Data = this.data;
                    this.ingredientNew.Amount = this.amount;
                    this.ingredientNew.Unit = this.unit;

                    this.Ingredients.Add(this.ingredientNew);
                }
                else
                {
                    this.ingredientNew = new Ingredient();
                    this.ingredientNew.Data = this.data;
                    this.ingredientNew.Amount = this.amount;
                    this.ingredientNew.Unit = this.unit;
                    this.Ingredients.Add(this.ingredientNew);
                }

                this.Recipe = new Recipe();

                foreach (var item in this.Ingredients)
                {
                    this.IngredientList.Add(item);
                }
                    
                Recipe.Ingredients = this.IngredientList;
                Recipe.Name = this.Name;

                this.ReloadData();
                this.EventAggregator.GetEvent<ReloadCurrentCheckRecipeEventData>().Publish(Recipe);
            }
        }

        #region ------------------------------------------------ Methods Helpers-------------------
        /// <summary>
        /// The LoadIngredients.
        /// </summary>
        /// <param name="list">ingredient lists</param>
        private void LoadIngredients(List<Ingredient> list)
        {
            //// init collection and add data
            this.Ingredients = new ObservableCollection<Ingredient>(list);
            this.IngredientList = list;
        }
        /// <summary>
        /// Gets the Recipe
        /// The recipe which will transfer into the end view if the user clicked on the button "Save".
        /// </summary>
        public void ReloadData()
        {
            this.OnPropertyChanged(nameof(this.Ingredients));
        }
        #endregion
    }
}

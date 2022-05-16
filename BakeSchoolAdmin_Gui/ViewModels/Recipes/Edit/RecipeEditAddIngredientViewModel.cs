namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeEditAddIngredientViewModel" />.
    /// </summary>
    internal class RecipeEditAddIngredientViewModel : MainViewModel
    {
        /// <summary>
        /// the name of the ingredient..
        /// </summary>
        private string name;

        /// <summary>
        /// the amount of the ingredient..
        /// </summary>
        private double amount = -1;

        /// <summary>
        /// the unit of the ingredient..
        /// </summary>
        private string unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditAddIngredientViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public RecipeEditAddIngredientViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.LoadIngredients();

            this.SaveIngredient = new ActionCommand(this.AddIngredientCommandExecute, this.AddIngredientCommandCanExecute);
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
        /// Gets or sets the name of the ingredient..
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
        /// Gets or sets the amount of the ingredient..
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
        /// The recipe which will transfer into the end view if the user clicked on the button "Save"..
        /// </summary>
        public Recipe Recipe { get; private set; }

        /// <summary>
        /// Gets or sets the Ingredients
        /// Get or set the list with all ingredient which user can select...
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets the SaveIngredient
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand SaveIngredient { get; private set; }

        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool AddIngredientCommandCanExecute(object parameter)
        {
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
                if (this.Ingredients.Count == 0)
                {
                    this.Ingredients = new ObservableCollection<Ingredient>();

                    Ingredient ingredient = new Ingredient();
                    ingredient.Data = this.name;
                    ingredient.Amount = this.amount;
                    ingredient.Unit = this.unit;

                    this.Ingredients.Add(ingredient);
                }
                else
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.Data = this.name;
                    ingredient.Amount = this.amount;
                    ingredient.Unit = this.unit;
                    this.Ingredients.Add(ingredient);
                }
            }
        }

        /// <summary>
        /// The LoadIngredients.
        /// </summary>
        private void LoadIngredients()
        {
            //// init collection and add data
            this.Ingredients = new ObservableCollection<Ingredient>();

            Ingredient ingredient = new Ingredient();
            ingredient.Data = "Käse";
            Ingredient ingredient1 = new Ingredient();
            ingredient1.Data = "Erdbeere";

            this.Ingredients.Add(ingredient);
            this.Ingredients.Add(ingredient1);
        }
    }
}

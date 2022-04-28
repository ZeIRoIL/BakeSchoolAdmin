using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    class RecipeEditAddIngredientViewModel : MainViewModel
    {
        #region ======================================== Fields, Constants, Delegates, Events ============================================ 
        /// <summary>
        /// the name of the ingredient
        /// </summary>
        private string name;
        /// <summary>
        /// the amount of the ingredient
        /// </summary>
        private double amount = -1;
        /// <summary>
        /// the unit of the ingredient
        /// </summary>
        private string unit;
        /// <summary>
        /// check if recipes are edited or created.
        /// </summary>
        private bool isEdit = true;
        #endregion
        public RecipeEditAddIngredientViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            LoadIngredients();

            this.SaveIngredient = new ActionCommand(this.AddIngredientCommandExecute, this.AddIngredientCommandCanExecute);
        }
        #region ======================================== Properties, Indexer =====================================================
        
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                if(value != string.Empty)
                {
                    this.unit = value;
                    this.OnPropertyChanged(nameof(this.unit));
                }
            }
        }
        /// <summary>
        /// Gets or sets the name of the ingredient
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
        /// Gets or sets the amount of the ingredient
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
        /// The recipe which will tranfer into the endview if the user clicked on the button "Save"
        /// </summary>
        public Recipe Recipe { get; private set; }
        /// <summary>
        /// Get or set the list with all ingredient which user can select.
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        /// <summary>
        /// execute the command and add the recent ingredient in the ObservableCollection 
        /// </summary>
        public ICommand SaveIngredient { get; private set; }
        #endregion
        #region ======================================== Command ====================================================
        /// <summary>
        /// Determines if the data is correct then the ingredient is created
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool AddIngredientCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void AddIngredientCommandExecute(object parameter)
        {
            if (this.name != string.Empty || this.amount != 0 || this.unit != String.Empty )
            {
                if (this.Ingredients.Count() == 0)
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
        #endregion
        #region ======================================== Private Helper ================================= 
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
        #endregion
    }
}

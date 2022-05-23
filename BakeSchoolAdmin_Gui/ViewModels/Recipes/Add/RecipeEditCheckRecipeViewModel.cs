namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="RecipeEditCheckRecipeViewModel" />.
    /// </summary>
    internal class RecipeEditCheckRecipeViewModel : ViewModelBase
    {
        /// <summary>
        /// Defines the recipe.
        /// </summary>
        private readonly Recipe recipe;

        /// <summary>
        /// Defines the description.
        /// </summary>
        private Description description;

        /// <summary>
        /// the name of the ingredient....
        /// </summary>
        private string name;

        /// <summary>
        /// the amount of the ingredient....
        /// </summary>
        private double amount = -1;

        /// <summary>
        /// the unit of the ingredient....
        /// </summary>
        private string unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditCheckRecipeViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipe">The recipe<see cref="Recipe"/>.</param>
        public RecipeEditCheckRecipeViewModel(IEventAggregator eventAggregator, Recipe recipe) : base(eventAggregator)
        {
            this.recipe = recipe;
            this.Name = recipe.Name;
            this.setIngredients(this.recipe);
            this.setDescriptions(this.recipe);
            this.ReloadData();
        }

        /// <summary>
        /// Gets or sets the Ingredient
        /// Defines the Ingredients..
        /// </summary>
        public Ingredient Ingredient{ get; set; }

        /// <summary>
        /// Gets or sets the Ingredient
        /// Defines the Ingredients..
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Gets or sets the Ingredients
        /// Get or set the list with all ingredient which user can select.....
        /// </summary>
        public ObservableCollection<Ingredient> IngredientsObs { get; set; }

        /// <summary>
        /// Gets or sets the Ingredients
        /// Get or set the list with all ingredient which user can select.....
        /// </summary>
        public ObservableCollection<Description> DescriptionObs { get; set; }

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
        /// Gets or sets the name of the ingredient....
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
        /// Gets or sets the amount of the ingredient....
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
        ///// The recipe which will transfer into the end view if the user clicked on the button "Save".
        /// </summary>
        public void ReloadData()
        {
            this.OnPropertyChanged(nameof(this.Name));
        }

        /// <summary>
        /// The setIngredients.
        /// </summary>
        /// <param name="recipe">The recipe<see cref="Recipe"/>.</param>
        public void setIngredients(Recipe recipe)
        {
            this.IngredientsObs = new ObservableCollection<Ingredient>();
            this.Ingredient = new Ingredient();

            foreach (var item in recipe.Ingredients)
            {
                Ingredient.Data = item.Data;
                Ingredient.Amount = item.Amount;
                Ingredient.Unit = item.Unit;

                this.IngredientsObs.Add(this.Ingredient);
            }
        }

        /// <summary>
        /// The setDescriptions for the recent recipe
        /// <param name="recipe">current recipe</param>
        /// </summary>
        public void setDescriptions(Recipe recipe)
        {
            this.DescriptionObs = new ObservableCollection<Description>();
            this.Description = new Description();

            foreach (var item in recipe.Descriptions)
            {
                Description.Text = item.Text;
                Description.Step = item.Step;
                Description.Hints = item.Hints;
                Description.Image = item.Image;

                this.DescriptionObs.Add(this.Description);
            }
        }
    }
}

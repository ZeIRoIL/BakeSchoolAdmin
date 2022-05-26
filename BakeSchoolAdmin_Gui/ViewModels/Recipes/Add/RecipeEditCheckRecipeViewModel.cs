namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_DatabaseConnection.Services;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using Microsoft.Practices.Prism.Events;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="RecipeEditCheckRecipeViewModel" />.
    /// </summary>
    internal class RecipeEditCheckRecipeViewModel : ViewModelBase
    {
        /// <summary>
        /// Defines the recipe.
        /// </summary>
        private readonly Recipe editrecipe;

        /// <summary>
        /// the description text for each step.
        /// </summary>
        private string text;

        /// <summary>
        /// the  step of description.
        /// </summary>
        private int step;

        /// <summary>
        /// the hint text for each step.
        /// </summary>
        private string texthint;

        /// <summary>
        /// the image of description.
        /// </summary>
        private Image image;

        /// <summary>
        /// the path to image 
        /// </summary>
        private string imagepath;

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
            this.editrecipe = recipe;
            this.Name = recipe.Name;
            this.setIngredients(this.editrecipe);
            this.setDescriptions(this.editrecipe);
            this.ReloadData();

            // the command for the ShowDescription
            this.ShowDescription = new ActionCommand(this.ShowDescriptionCommandExecute, this.ShowDescriptionCommandCanExecute);
            this.SaveRecipe = new ActionCommand(this.SaveRecipeCommandExecute, this.SaveRecipeCommandCanExecute);
        }

        /// <summary>
        /// Gets the ShowDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand ShowDescription { get; private set; }

        /// <summary>
        /// Gets the ShowDescription
        /// execute the command and save the whole recipe into the database.
        /// </summary>
        public ICommand SaveRecipe { get; private set; }

        /// <summary>
        /// Gets or sets the Ingredient
        /// Defines the Ingredients..
        /// </summary>
        public Ingredient Ingredient { get; set; }

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
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != string.Empty)
                {
                    this.text = value;
                    this.OnPropertyChanged(nameof(this.text));
                }
            }
        }

        /// <summary>
        /// Gets or sets the text hint.
        /// </summary>
        public string Texthint
        {
            get
            {
                return this.texthint;
            }

            set
            {
                if (value != string.Empty)
                {
                    this.texthint = value;
                    this.OnPropertyChanged(nameof(this.texthint));
                }
            }
        }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        public int Step
        {
            get
            {
                return this.step;
            }

            set
            {
                if (value != 0)
                {
                    this.step = value;
                    this.OnPropertyChanged(nameof(this.step));
                }
            }
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
        /// Gets or sets the Text.
        /// </summary>
        public Image Image
        {
            get
            {
                return this.image;
            }

            set
            {
                    this.image = value;
                    this.OnPropertyChanged(nameof(this.image));
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return this.imagepath;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.imagepath = value;
                    this.OnPropertyChanged(nameof(this.imagepath));
                }
            }
        }

        /// <summary>
        /// Gets the Recipe
        ///  The recipe which will transfer into the end view if the user clicked on the button "Save".
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
        /// </summary>
        /// <param name="recipe">current recipe</param>
        public void setDescriptions(Recipe recipe)
        {
            this.DescriptionObs = new ObservableCollection<Description>();
            this.Description = new Description();

            foreach (var item in recipe.Descriptions)
            {
                this.DescriptionObs.Add(item);
            }
        }

        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ShowDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and show user the text of the step.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void ShowDescriptionCommandExecute(object parameter)
        {
            if ((int)parameter != 0)
            {
                int stepId = (int)parameter;
                if (this.DescriptionObs.Any(d => d.Step == stepId))
                {
                    this.step = stepId;
                    this.text = this.DescriptionObs[this.step - 1].Text;
                    this.Image = new Image();
                    Image.Source = new BitmapImage(new Uri(this.DescriptionObs[this.step - 1].Image, UriKind.RelativeOrAbsolute));
                    this.OnPropertyChanged(nameof(this.text));
                    this.OnPropertyChanged(nameof(this.Image));
                }
            }
        }

        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SaveRecipeCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and show user the text of the step.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void SaveRecipeCommandExecute(object parameter)
        {
            RecipeService recipeService = new RecipeService();
            if (recipeService.init())
            {
                recipeService.WriteDataRecipe(this.editrecipe);
            }
        }
    }
}

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeMainDetailViewModel" />.
    /// </summary>
    internal class RecipeMainDetailViewModel : ViewModelBase
    {
        #region ---------------------------------------- fields -------------------------------------
        /// <summary>
        /// create name field.
        /// </summary>
        private string name;

        /// <summary>
        /// store the image path for each steps description
        /// </summary>
        private Image imagepath;

        /// <summary>
        /// the text for one description..
        /// </summary>
        private string text;

        /// <summary>
        /// the step for one description..
        /// </summary>
        private int step;

        /// <summary>
        /// the step counter for the recipe..
        /// </summary>
        private int stepCount;
        #endregion

        #region ---------------------------------------- Constructor ------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeMainDetailViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public RecipeMainDetailViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // subscribe to event
            this.EventAggregator.GetEvent<SelectedRecipeDataEvent>().Subscribe(this.SelectedRecipe, ThreadOption.UIThread);
            
            // the command for the ShowDescription
            this.ShowDescription = new ActionCommand(this.ShowDescriptionCommandExecute, this.ShowDescriptionCommandCanExecute);
        }
        #endregion

        /// <summary>
        /// Gets or sets the descriptions from the recipe..
        /// </summary>
        public ObservableCollection<Description> Descriptions { get; set; }

        /// <summary>
        /// Gets or sets the Ingredients.
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets the ShowDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand ShowDescription { get; private set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != null)
                {
                    this.name = value;
                    this.OnPropertyChanged(nameof(this.Name));
                }
            }
        }

        /// <summary>
        /// Gets or sets the image path
        /// </summary>
        public Image Imagepath
        {
            get
            {
                return this.imagepath;
            }

            set
            {
                if (value != null)
                {
                    this.imagepath = value;
                    this.OnPropertyChanged(nameof(this.imagepath));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.text = value;
                    this.OnPropertyChanged(nameof(this.text));
                }
            }
        }

        /// <summary>
        /// Gets or sets the StepCount.
        /// </summary>
        public int StepCount
        {
            get
            {
                return this.stepCount;
            }

            set
            {
                if (-1 != value)
                {
                    this.stepCount = value;
                    this.OnPropertyChanged(nameof(this.stepCount));
                }
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
                if (this.Descriptions.Any(d => d.Step == stepId))
                {
                    this.step = stepId;
                    this.text = this.Descriptions[this.step - 1].Text;

                    this.Imagepath = new Image();
                    Imagepath.Source =  new BitmapImage(new Uri(this.Descriptions[this.step -1].Image));
                   
                    this.OnPropertyChanged(nameof(this.text));
                    this.OnPropertyChanged(nameof(this.Imagepath));
                }
            }
        }

        #region ------------------------------------- method helpers ----------------------------
        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="recipe">Reference to the sent student data.</param>
        private void SelectedRecipe(Recipe recipe)
        {
            this.Name = recipe.Name;

            // set the ingredients into the view
            ObservableCollection<Ingredient> ingredientsNew = new ObservableCollection<Ingredient>();
            foreach (var item in recipe.Ingredients)
            {
                ingredientsNew.Add(item);
            }

            this.Ingredients = ingredientsNew;
            this.OnPropertyChanged(nameof(this.Ingredients));

            // set the Descritpion into the view
            ObservableCollection<Description> descriptionNew = new ObservableCollection<Description>();
            foreach (Description item in recipe.Descriptions)
            {
                descriptionNew.Add(item);
            }

            this.Descriptions = descriptionNew;
            this.OnPropertyChanged(nameof(this.Descriptions));

            this.StepCount = descriptionNew.Count;
            this.OnPropertyChanged(nameof(this.StepCount));
        }
        #endregion
    }
}

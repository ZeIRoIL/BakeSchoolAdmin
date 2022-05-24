namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events.RecipeAddEvents;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeEditAddDescriptionViewModel" />.
    /// </summary>
    internal class RecipeEditAddDescriptionViewModel : ViewModelBase
    {
        #region ---------------------------------------- private fields ---------------------------
        /// <summary>
        /// the text for one description..
        /// </summary>
        private string text;

        /// <summary>
        /// the text for one description..
        /// </summary>
        private string texthint;

        /// <summary>
        /// the step for one description..
        /// </summary>
        private int step;

        /// <summary>
        /// the step for one hint..
        /// </summary>
        private int stephint;

        /// <summary>
        /// the step counter for the recipe..
        /// </summary>
        private int stepCount;
        #endregion

        #region ---------------------------------------------- constructor --------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditAddDescriptionViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipe">The recipeDes<see cref="Recipe"/>.</param>
        public RecipeEditAddDescriptionViewModel(IEventAggregator eventAggregator, Recipe recipe) : base(eventAggregator)
        {
            if (recipe != null)
            {
                // If the Description is always created
                this.Recipe = recipe;
                if (recipe.Descriptions != null)
                {
                    this.Descriptions = new ObservableCollection<Description>(recipe.Descriptions);
                }
                else
                {
                    this.Descriptions = new ObservableCollection<Description>();
                }
                this.OnPropertyChanged(nameof(this.Descriptions));
            }
            else
            {
                // If the Description will init!
                this.Descriptions = new ObservableCollection<Description>();
                //this.Hints = new ObservableCollection<Hint>();
            }

            #region -------------------------------------------------------------- Description Commands --------------------------------------------------
            this.ShowDescription = new ActionCommand(this.ShowDescriptionCommandExecute, this.ShowDescriptionCommandCanExecute);
            this.AddDescription = new ActionCommand(this.AddDescriptionCommandExecute, this.AddDescriptionCommandCanExecute);
            this.SaveDescription = new ActionCommand(this.SaveDescriptionCommandExecute, this.SaveDescriptionCommandCanExecute);
            this.DeleteDescription = new ActionCommand(this.DeleteDescriptionCommandExecute, this.DeleteDescriptionCommandCanExecute);
            #endregion

            #region -------------------------------------------------------------- Hint Commands --------------------------------------------------
            //this.ShowHint = new ActionCommand(this.ShowHintCommandExecute, this.ShowHintCommandCanExecute);
            //this.AddHint = new ActionCommand(this.AddHintCommandExecute, this.AddHintCommandCanExecute);
            //this.DeleteHint = new ActionCommand(this.DeleteHintCommandExecute, this.DeleteHintCommandCanExecute);
            #endregion

        }
        #endregion

        #region ------------------------------------------------------------ properties, Indexer -----------------------------------------------
        /// <summary>
        /// Gets the Descriptions
        /// save the description of the recipes..
        /// </summary>
        public ObservableCollection<Description> Descriptions { get; private set; }

        /// <summary>
        /// Gets the Descriptions
        /// save the description of the recipes..
        /// </summary>
        public List<Description> DescriptionsList { get; private set; }

        ///// <summary>
        ///// Gets the Hints
        ///// save the description of the recipes..
        ///// </summary>
        //public ObservableCollection<Hint> Hints { get; private set; }

        /// <summary>
        /// Gets the Recipe
        /// The recipe which will transfer into the end view if the user clicked on the button "Save"...
        /// </summary>
        public Recipe Recipe { get; private set; }

        /// <summary>
        /// Gets the ShowDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand ShowDescription { get; private set; }

        /// <summary>
        /// Gets the AddDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand AddDescription { get; private set; }

        /// <summary>
        /// Gets the SaveDescription
        /// execute the command and save the recent data into description..
        /// </summary>
        public ICommand SaveDescription { get; private set; }

        /// <summary>
        /// Gets the delete description
        /// execute the command and delete the recent data from the description.
        /// </summary>
        public ICommand DeleteDescription { get; private set; }

        /// <summary>
        /// Gets the show hint
        /// execute the command and add the recent description in the ObservableCollection..
        /// </summary>
        public ICommand ShowHint { get; private set; }

        /// <summary>
        /// Gets the AddDescription
        /// execute the command and add the recent description in the ObservableCollection..
        /// </summary>
        public ICommand AddHint { get; private set; }

        /// <summary>
        /// Gets the delete description
        /// execute the command and delete the recent data from the description.
        /// </summary>
        public ICommand DeleteHint { get; private set; }

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
        /// Gets or sets the Text.
        /// </summary>
        public string Texthint
        {
            get
            {
                return this.texthint;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.texthint = value;
                    this.OnPropertyChanged(nameof(this.texthint));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        public int Step
        {
            get
            {
                return this.step;
            }

            set
            {
                if (-1 != value)
                {
                    this.step = value;
                    this.OnPropertyChanged(nameof(this.step));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Step of hint.
        /// </summary>
        public int Stephint
        {
            get
            {
                return this.stephint;
            }

            set
            {
                if (-1 != value)
                {
                    this.stephint = value;
                    this.OnPropertyChanged(nameof(this.stephint));
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
        #endregion

        #region ---------------------------------------------------------- Commands ------------------------------------------------------------------

        #region ---------------------------------------------------------- Description ------------------------------------------------------------------

        #region -------------------------------------Show Description ------------------------------------
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
                    this.Step = stepId;
                    this.Text = this.Descriptions[this.Step - 1].Text;
                    this.OnPropertyChanged(nameof(this.Text));
                }
            }
        }
        #endregion

        #region -------------------------------------Add Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description step can be created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool AddDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and add a new Step for the description.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddDescriptionCommandExecute(object parameter)
        {
            // If the descriptionbox always contains a text.
            if (this.Text != null)
            {
                int saveStep = this.Step;
                this.Descriptions[saveStep - 1].Text = this.Text;
            }

            Description descriptions = new Description();
            int count = this.Descriptions.Count() + 1;
            if (count != 0)
            {
                descriptions.Step = count;
                this.Step = count;
                this.Descriptions.Add(descriptions);
                this.OnPropertyChanged(nameof(this.Descriptions));
            }
        }
        #endregion

        #region -------------------------------------Save Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SaveDescriptionCommandCanExecute(object parameter)
        {
            if (this.Descriptions == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void SaveDescriptionCommandExecute(object parameter)
        {
            // Save Description Step befor the whole recipe will create
            int saveStep = this.Step;
          
            this.Descriptions[saveStep - 1].Text = this.Text;

            this.OnPropertyChanged(nameof(this.Descriptions));

            this.DescriptionsList = new List<Description>();
            this.Recipe = new Recipe();

            foreach (var item in this.Descriptions)
            {
                this.DescriptionsList.Add(item);
            }

            this.Recipe.Descriptions = this.DescriptionsList;
            this.EventAggregator.GetEvent<ReloadCurrentCheckRecipeEventData>().Publish(this.Recipe);
        }
        #endregion

        #region -------------------------------------Delete Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool DeleteDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void DeleteDescriptionCommandExecute(object parameter)
        {
            if (Descriptions.Count != 0)
            {
                Descriptions.Remove(Descriptions.Last());
                this.Step--;
            }

            this.OnPropertyChanged(nameof(this.Descriptions));
            this.OnPropertyChanged(nameof(this.Step));
        }
        #endregion

        #endregion

        //#region --------------------------------------------------------- Hints ---------------------------------------------------------------------------

        //#region -------------------------------------Show Hint ------------------------------------
        ///// <summary>
        ///// Determines if the data is correct then the ingredient is created.
        ///// </summary>
        ///// <param name="parameter">Data used by the Command.</param>
        ///// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        //private bool ShowHintCommandCanExecute(object parameter)
        //{
        //    return true;
        //}

        ///// <summary>
        ///// Gets executed and show user the text of the step.
        ///// </summary>
        ///// <param name="parameter">Data used by the command.</param>
        //private void ShowHintCommandExecute(object parameter)
        //{
        //    if ((int)parameter != 0)
        //    {
        //        int stepId = (int)parameter;
        //        if (this.Hints.Any(d => d.Step == stepId))
        //        {
        //            this.stephint = stepId;
        //            this.Texthint = this.Hints[this.stephint - 1].Text;

        //            if (this.Descriptions[this.step - 1].Hints == null)
        //            {
        //                this.Texthint = string.Empty;
        //            }

        //            this.OnPropertyChanged(nameof(this.Texthint));
        //        }
        //    }
        //}
        //#endregion

        //#region -------------------------------------Add Hint ------------------------------------
        ///// <summary>
        ///// Determines if the data is correct then the description step can be created.
        ///// </summary>
        ///// <param name="parameter">Data used by the Command.</param>
        ///// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        //private bool AddHintCommandCanExecute(object parameter)
        //{
        //    return true;
        //}

        ///// <summary>
        ///// Gets executed and add a new Step for the description.
        ///// </summary>
        ///// <param name="parameter">Data used by the command.</param>
        //private void AddHintCommandExecute(object parameter)
        //{
        //    // If the descriptionbox always contains a text.
        //    if (this.Text != null)
        //    {
        //        if (this.Hints.Count == 0)
        //        {
        //            this.Hints[0].Text = this.Text;
        //        }
        //        else
        //        {
        //            int saveStep = this.Stephint;
        //            this.Hints[saveStep - 1].Text = this.Text;
        //        }
        //    }

        //    Hint hints = new Hint();
        //    int count = this.Hints.Count() + 1;
        //    if (count != 0)
        //    {
        //        hints.Step = count;
        //        this.Stephint = count;
        //        this.Hints.Add(hints);
        //        this.OnPropertyChanged(nameof(this.Hints));
        //    }
        //}
        //#endregion

        //#region -------------------------------------Delete Hint ------------------------------------
        ///// <summary>
        ///// Determines if the data is correct then the description can be saved.
        ///// </summary>
        ///// <param name="parameter">Data used by the Command.</param>
        ///// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        //private bool DeleteHintCommandCanExecute(object parameter)
        //{
        //    return true;
        //}

        ///// <summary>
        ///// Gets executed when the user clicks the Save button.
        ///// </summary>
        ///// <param name="parameter">Data used by the command.</param>
        //private void DeleteHintCommandExecute(object parameter)
        //{
        //    if (this.Hints.Count != 0)
        //    {
        //        this.Hints.Remove(this.Hints.Last());
        //        this.Stephint--;
        //    }

        //    this.OnPropertyChanged(nameof(this.Hints));
        //    this.OnPropertyChanged(nameof(this.Stephint));
        //}
        //#endregion

        //#endregion




        #endregion

        #region --------------------------------------------------------------------- mini helper -------------------------------------------
        /// <summary>
        /// load the current description data into the description field.
        /// </summary>
        public void LoadDescription()
        {
            throw new Exception("Das ist ein Test");
        }
        #endregion
    }
}

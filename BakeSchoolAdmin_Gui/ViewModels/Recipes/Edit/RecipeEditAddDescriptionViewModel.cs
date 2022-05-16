﻿namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;


    /// <summary>
    /// Defines the <see cref="RecipeEditAddDescriptionViewModel" />.
    /// </summary>
    internal class RecipeEditAddDescriptionViewModel : ViewModelBase
    {

        /// <summary>
        /// Gets or sets the recipesDes
        /// recipe which contain all the description of the recipe...
        /// </summary>
        private Description recipesDes { get; set; }

        /// <summary>
        /// Gets or sets the hints
        /// If the description has a hint than it would be there...
        /// </summary>
        private List<Hint> hints { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditAddDescriptionViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipeDes">The recipeDes<see cref="Description"/>.</param>
        public RecipeEditAddDescriptionViewModel(IEventAggregator eventAggregator, Description recipeDes) : base(eventAggregator)
        {
            this.recipesDes = recipeDes;
            this.Descriptions = new ObservableCollection<Description>();
            LoadDescription();

            this.ShowDescription = new ActionCommand(this.ShowDescriptionCommandExecute, this.ShowDescriptionCommandCanExecute);

            this.AddDescription = new ActionCommand(this.AddDescriptionCommandExecute, this.AddDescriptionCommandCanExecute);

            this.SaveDescription = new ActionCommand(this.SaveDescriptionCommandExecute, this.SaveDescriptionCommandCanExecute);
        }

        /// <summary>
        /// Gets the Descriptions
        /// save the description of the recipes..
        /// </summary>
        public ObservableCollection<Description> Descriptions { get; private set; }

        /// <summary>
        /// Gets the Hints
        /// save the description of the recipes..
        /// </summary>
        public ObservableCollection<Hint> Hints { get; private set; }

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
                    this.OnPropertyChanged(nameof(this.text));
                }
            }
        }

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
            Description descriptions = new Description();

            int count = this.Descriptions.Count() + 1;
            if (count != 0)
            {
                descriptions.Step = count;
                this.Descriptions.Add(descriptions);
                this.OnPropertyChanged(nameof(this.Descriptions));
            }
        }

        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SaveDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void SaveDescriptionCommandExecute(object parameter)
        {
            int saveStep = this.step;
            this.Descriptions[saveStep - 1].Text = this.Text;
        }

        /// <summary>
        /// load the current description data into the description field.
        /// </summary>
        internal void LoadDescription()
        {
        }
    }
}

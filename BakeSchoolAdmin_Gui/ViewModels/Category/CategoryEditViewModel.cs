namespace BakeSchoolAdmin_Gui.ViewModels
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_DatabaseConnection.Services;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.Events.CategoryEvents;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Category;
    using Microsoft.Practices.Prism.Events;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="CategoryEditViewModel" />.
    /// </summary>
    internal class CategoryEditViewModel : ViewModelBase
    {
        #region --------------------------------------------- Fields, Constants -----------------------------------------
        /// <summary>
        /// the amount for the slider of difficult..
        /// </summary>
        private int amount;
        #endregion

        #region --------------------------------------------- Con-/Destructor, Dispose, Clone ----------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryEditViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public CategoryEditViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // the command for the ShowHint
            this.EditCategoryCommand = new ActionCommand(this.EditCategoryExecute, this.EditCategoryCanExecute);

            // subscribe to event
            this.EventAggregator.GetEvent<SelectedCategoryDataEvent>().Subscribe(this.SelectedCategory, ThreadOption.UIThread);
        }
        #endregion

        #region --------------------------------------------- Propterties, Indexer -----------------------------------------
        /// <summary>
        /// Gets and sets the command edit category
        /// </summary>
        public ICommand EditCategoryCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Text of the selected category..
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Text of the selected category..
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Text of the selected category..
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the amount of the slider..
        /// </summary>
        public int Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged(nameof(this.amount));
                }
            }
        }
        #endregion

        #region --------------------------------------------- Commands -----------------------------------------
        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        public bool EditCategoryCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and show user the text of the step.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void EditCategoryExecute(object parameter)
        {
            CategoryDetails details = new CategoryDetails();
            details.Name = this.Name;
            details.Text = this.Text;
            details.Level = this.Amount;
            Category editCategory = new Category(this.Id, details);

            CategoryService categoryService = new CategoryService();
            
            if (categoryService.init())
            {
                categoryService.UpdateData(editCategory);
                this.EventAggregator.GetEvent<ReloadCategoryDataEvent>().Publish(true);

                // call the condition of save data
                this.EventAggregator.GetEvent<IsSavedCategoryEvent>().Publish("gespeichert");
            }
        }

        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="category">Reference to the sent student data.</param>
        public void SelectedCategory(Category category)
        {
            this.Text = category.Details.Text;
            this.Name = category.Details.Name;
            this.amount = category.Details.Level;

            this.OnPropertyChanged(nameof(this.Text));
            this.OnPropertyChanged(nameof(this.Name));
            this.OnPropertyChanged(nameof(this.amount));

            // call the condition of save data
            this.EventAggregator.GetEvent<IsSavedCategoryEvent>().Publish("ungespeichert");
        }
        #endregion
    }
}

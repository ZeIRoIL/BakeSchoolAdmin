namespace BakeSchoolAdmin_Gui.ViewModels
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.Events.CategoryEvents;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Category;
    using Microsoft.Practices.Prism.Events;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="CategoryAddViewModel" />.
    /// </summary>
    internal class CategoryAddViewModel : ViewModelBase
    {
        #region --------------------------------------------- Fields, Constants -----------------------------------------
        /// <summary>
        /// save the id for the category.
        /// </summary>
        private int id;

        /// <summary>
        /// the amount for the slider of difficult.
        /// </summary>
        private int amount;

        /// <summary>
        /// Whether user want to save the category in a file.
        /// </summary>
        private bool wantFileDb;
        #endregion

        #region --------------------------------------------- Con-/Destructor, Dispose, Clone ----------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAddViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public CategoryAddViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.CategoryAddNewCommand = new ActionCommand(this.CategroyAddCommandExecute, this.CategoryAddCommandCanExecute);

            // subscribe to event
            this.EventAggregator.GetEvent<GetLastCategorieIdDataEvent>().Subscribe(this.SetCollection, ThreadOption.UIThread);
        }
        #endregion

        #region --------------------------------------------- Propterties, Indexer -----------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the user want to save the database into a file.
        /// </summary>
        public bool WantFileDb
        {
            get 
            {
                return this.wantFileDb; 
            }

            set
            {
                    this.wantFileDb = value;
                    this.OnPropertyChanged(nameof(this.wantFileDb));
            }
        }

        /// <summary>
        /// Gets or sets the amount of the slider.
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

        /// <summary>
        /// Gets or sets the Name
        /// get and set the Name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Text
        /// get and set the Text of the category.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets the CategoryAddNewCommand
        /// execute  the Command which add a new category into the list.
        /// </summary>
        public ICommand CategoryAddNewCommand { get; private set; }
        #endregion

        #region --------------------------------------------- Mini Helpers -----------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="idCat">The idCat<see cref="int"/>id category</param>
        public void SetCollection(int idCat)
        {
            this.id = idCat;
        }
        #endregion

        #region --------------------------------------------- Commands -----------------------------------------
        /// <summary>
        /// Determines if the edit the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool CategoryAddCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void CategroyAddCommandExecute(object parameter)
        {
            CategoryDetails details = new CategoryDetails();
            details.Name = this.Name;
            details.Text = this.Text;
            details.Level = this.Amount;
            Category category = new Category(this.id, details, this.WantFileDb);

            this.EventAggregator.GetEvent<AddCategoryDataEvent>().Publish(category);

            this.EventAggregator.GetEvent<IsSavedCategoryEvent>().Publish("gespeichert");
        }
        #endregion
    }
}

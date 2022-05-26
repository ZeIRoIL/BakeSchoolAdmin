namespace BakeSchoolAdmin_Gui.ViewModels
{
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_DatabaseConnection.Services;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.Events.CategoryEvents;
    using BakeSchoolAdmin_Gui.Views.Category;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="CategoryListViewModel" />.
    /// </summary>
    internal class CategoryListViewModel : ViewModelBase
    {
        #region --------------------------------------------- Fields, Constants -----------------------------------------
        /// <summary>
        /// The id of the category..
        /// </summary>
        private int id;
        #endregion

        #region --------------------------------------------- Con-/Destructor, Dispose, Clone ----------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryListViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public CategoryListViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // Gets the category to the list
            this.LoadCategories();

            // action command, if the button is clicked then set the value of the button into the parameter
            this.CategoryViewCommand = new ActionCommand(this.CategroyViewCommandExecute, this.CategoryViewCommandCanExecute);

            // action command if the change button is clicked and the usercontrol (AddCategory) will open.
            this.CategoryAddCommand = new ActionCommand(this.CategroyEditViewCommandExecute, this.CategoryEditViewCommandCanExecute);

            // action command if the delete button is clicked.
            this.CategoryDeleteViewCommand = new ActionCommand(this.CategroyDeleteViewCommandExecute, this.CategoryDeleteViewCommandCanExecute);

            // subscribe new event for the new data save
            this.EventAggregator.GetEvent<AddCategoryDataEvent>().Subscribe(this.AddCategory, ThreadOption.UIThread);

            // subscribe new event for the new data save
            this.EventAggregator.GetEvent<ReloadCategoryDataEvent>().Subscribe(this.EditCategoris, ThreadOption.UIThread);
        }
        #endregion

        #region --------------------------------------------- Propterties, Indexer -----------------------------------------
        /// <summary>
        /// Gets or sets the selected Category from the current button click..
        /// </summary>
        public Category SelectedCategory { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value != -1)
                {
                    this.id = value;
                    this.OnPropertyChanged(nameof(this.Id));
                }
            }
        }

        /// <summary>
        /// Gets the Category  id (Only Test)..
        /// </summary>
        public ICommand CategoryViewCommand { get; private set; }

        /// <summary>
        /// Gets the Category  id (Only Test)..
        /// </summary>
        public ICommand CategoryAddCommand { get; private set; }

        /// <summary>
        /// Gets the CategoryDeleteViewCommand
        /// it is the delete button for the selected category..
        /// </summary>
        public ICommand CategoryDeleteViewCommand { get; private set; }

        /// <summary>
        /// Gets or sets the list with all categories..
        /// </summary>
        public ObservableCollection<Category> Categories { get; set; }
        #endregion

        #region --------------------------------------------- Mini Helpers -----------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="category">Reference to the sent student data.</param>
        public void AddCategory(Category category)
        {
            CategoryService categoryService = new CategoryService();
            if (categoryService.init())
            {
                categoryService.WriteData(category);
            }

            this.LoadCategories();

            this.OnPropertyChanged(nameof(this.Categories));
        }

        /// <summary>
        /// check whether the categories is edited
        /// </summary>
        /// <param name="isEdit">return the value</param>
        public void EditCategoris(bool isEdit)
        {
            if (isEdit)
            {
                this.LoadCategories();
            }
        }

        /// <summary>
        /// Initialize the database connection and is loaded in the Category data.
        /// </summary>
        private void LoadCategories()
        {
            CategoryService categoryService = new CategoryService();
            if (categoryService.init())
            {
                IList<Category> categorydata;
                categorydata = categoryService.ReadData();
                this.Categories = categoryService.GetCategoryObserv(categorydata);
            }
        }
        #endregion

        #region --------------------------------------------- Commands -----------------------------------------
        /// <summary>
        /// Determines if the edit the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool CategoryEditViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void CategroyEditViewCommandExecute(object parameter)
        {
            CategorySave categorySave = new CategorySave();
            CategoryAddViewModel categoryAddViewModel = new CategoryAddViewModel(EventAggregator);
            categorySave.DataContext = categoryAddViewModel;
            
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(categorySave);

            // Send the last id from the Categories
            this.EventAggregator.GetEvent<GetLastCategorieIdDataEvent>().Publish(this.Categories.Count + 1);
        }

        /// <summary>
        /// Determines if the delete the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool CategoryDeleteViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the delete button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void CategroyDeleteViewCommandExecute(object parameter)
        {
            CategoryService categoryService = new CategoryService();
            if (categoryService.init())
            {
                categoryService.DeleteData((int)parameter);
                this.LoadCategories();
                this.OnPropertyChanged(nameof(this.Categories));
            }
        }

        /// <summary>
        /// Determines if the edit the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool CategoryViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void CategroyViewCommandExecute(object parameter)
        {
            CategoryEdit edit = new CategoryEdit();
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel(EventAggregator);
            edit.DataContext = categoryEditViewModel;
            
            // Change the current view Right, if the event is fired.
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(edit);

            foreach (Category category in this.Categories)
            {
                if (category.Id == (int)parameter)
                {
                    this.SelectedCategory = category;

                    this.EventAggregator.GetEvent<SelectedCategoryDataEvent>().Publish(this.SelectedCategory);
                }
            }
            #endregion
        }
    }
}

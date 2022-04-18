using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Gui.Views.Category;
using BakeSchoolAdmin_Models;
using BakeSchoolAdmin_Models.Modals.Category;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    class CategoryListViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 
        /// <summary>
        /// The id of the category
        /// </summary>
        private int id;

        /// <summary>
        /// Create the selected Category form the current button click
        /// </summary>
        public Category selectedCategory;
        #endregion

        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator to communicate with other views
        /// via <see cref="Microsoft.Practices.Prism.Events"/> </param>
        public CategoryListViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // Gets the category to the list
            LoadCategories();

            // action command, if the button is clicked then set the value of the button into the parameter
            this.CategoryViewCommand = new ActionCommand(
                (value) => {
                this.CategroyViewCommandExecute(value);
                },
                this.CategoryViewCommandCanExecute);

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.CategoryAddCommand = new ActionCommand(this.CategroyEditViewCommandExecute, this.CategoryEditViewCommandCanExecute);

            //subscribe new event for the new data save
            this.EventAggregator.GetEvent<AddCategoryDataEvent>().Subscribe(this.AddCategory, ThreadOption.UIThread);
        }
        #endregion

        #region ======================================== Properties/Indexer ================================= 

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if(value != -1)
                {
                    this.id = value;
                    this.OnPropertyChanged(nameof(this.id));
                }
            }
        }

        /// <summary>
        /// Gets the Category  id (Only Test)
        /// </summary>
        public ICommand CategoryViewCommand { get; private set; }

        /// <summary>
        /// Gets the Category  id (Only Test)
        /// </summary>
        public ICommand CategoryAddCommand { get; private set; }

        /// <summary>
        /// Gets or sets the list with all categories
        /// </summary>
        public ObservableCollection<Category> Categories { get; set; }
        #endregion

        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void AddCategory(Category category)
        {
            this.Categories.Add(category);

            this.OnPropertyChanged(nameof(this.Categories));
        }
        #endregion

        #region ======================================== Private Helper ================================= 
        private void LoadCategories()
        {
            //// init collection and add data
            this.Categories = new ObservableCollection<Category>();

            CategoryDetails details = new CategoryDetails();
            details.Name = "TestCate1";
            details.Image = "das ist ein Test";
            details.Text = "Das ist ein Text test";
            details.Level = 2;
            Category category = new Category(100, details);

            CategoryDetails details1 = new CategoryDetails();
            details1.Name = "TestCate2";
            details1.Image = "das ist ein Test";
            details1.Text = "Das ist ein Text Test 2";
            details1.Level = 2;

            Category category1 = new Category(200, details1);



            this.Categories.Add(category);
            this.Categories.Add(category1);
        }
        #endregion

        #region ======================================== Commands ================================= 

        /// <summary>
        /// Determines if the edti the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool CategoryEditViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void CategroyEditViewCommandExecute(object parameter)
        {
            UserControl userControl = new UserControl();
            CategorySave categorySave = new CategorySave();
            CategoryAddViewModel categoryAddViewModel = new CategoryAddViewModel(EventAggregator);
            categorySave.DataContext = categoryAddViewModel;
            userControl = categorySave;
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(userControl);
            // Send the last id from the Categories
            this.EventAggregator.GetEvent<GetLastCategorieIdDataEvent>().Publish(Categories.Count +1 );
        }


        /// <summary>
        /// Determines if the edti the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool CategoryViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void CategroyViewCommandExecute(object parameter)
        {
            UserControl editControl = new UserControl();
            CategoryEdit edit = new CategoryEdit();
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel(EventAggregator);
            edit.DataContext = categoryEditViewModel;
            editControl = edit;

            // Change the current view Right, if the event is fired.
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Publish(editControl);

            foreach (Category category in this.Categories)
            {
                if (category.Id == (int)parameter)
                {
                    int i = category.Id;
                    
                    this.selectedCategory = category;
                    
                    this.EventAggregator.GetEvent<SelectedCategoryDataEvent>().Publish(this.selectedCategory);
                }
            }
        }

        #endregion
    }
}

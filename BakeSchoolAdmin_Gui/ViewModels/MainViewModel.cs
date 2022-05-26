namespace BakeSchoolAdmin_Gui.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_DatabaseConnection.Services;
    using BakeSchoolAdmin_Gui.ViewModels.Recipes;
    using BakeSchoolAdmin_Gui.Views;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;
  
    /// <summary>
    /// Defines the <see cref="MainViewModel" />.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Defines the categoryWindow.
        /// </summary>
        private UserControl categoryWindow;

        /// <summary>
        /// Defines the recipesWindow.
        /// </summary>
        private UserControl recipesWindow;

        /// <summary>
        /// View that is currently bound to the main ContentControl..
        /// </summary>
        private UserControl currentMainView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // show the main site with the buttons
            MainView mainView = new MainView();
            this.CurrentMainView = mainView;

            //// Hookup command to be associated
            this.CategoryViewCommand = new ActionCommand(this.CategoryViewExecute, this.CategoryViewCanExecute);

            //// Hookup command to be associated
            this.MainViewCommand = new ActionCommand(this.MainViewExecute, this.MainViewCanExecute);

            //// Hookup command to be associated
            this.RecipesViewCommand = new ActionCommand(this.RecipesViewExecute, this.RecipesViewCanExecute);
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the main ContentControl..
        /// </summary>
        public UserControl CurrentMainView
        {
            get
            {
                return this.currentMainView;
            }

            set
            {
                if (this.currentMainView != value)
                {
                    this.currentMainView = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentMainView));
                }
            }
        }

        /// <summary>
        /// Gets the MainViewCommand.
        /// </summary>
        public ICommand MainViewCommand { get; private set; }

        /// <summary>
        /// Gets the CategoryViewCommand.
        /// </summary>
        public ICommand CategoryViewCommand { get; private set; }

        /// <summary>
        /// Gets the RecipesViewCommand.
        /// </summary>
        public ICommand RecipesViewCommand { get; private set; }

        /// <summary>
        /// Gets the RecipesViewCommand.
        /// </summary>
        public ICommand TestRecipeCommand { get; private set; }

        /// <summary>
        /// Determines if the loading main view command can be execute.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool MainViewCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets execute when the user clicks the main button.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        private void MainViewExecute(object parameter)
        {
            MainView main = new MainView();
            this.CurrentMainView = main;
        }

        /// <summary>
        /// Determines if the loading category view command can be execute.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool CategoryViewCanExecute(object parameter)
        {
           if (this.IsConnected())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets execute when the user clicks the category button.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        private void CategoryViewExecute(object parameter)
        {
            // Create a new Window with the data from CategoryMainViewModel
            MainCategoryView mainCategoryView = new MainCategoryView();
            this.categoryWindow = mainCategoryView;
            CategoryMainViewModel categoryMainView = new CategoryMainViewModel(EventAggregator);
            mainCategoryView.DataContext = categoryMainView;
            this.CurrentMainView = mainCategoryView;
        }

        /// <summary>
        /// Determines if the loading recipes view command can be execute.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool RecipesViewCanExecute(object parameter)
        {
            if (this.IsConnected())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets execute when the user clicks the recipes button.
        /// </summary>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        private void RecipesViewExecute(object parameter)
        {
            // Create a new Window with the data from MainRecipesViewModel
            MainRecipeView mainRecipesView = new MainRecipeView();
            this.recipesWindow = mainRecipesView;
            MainRecipesViewModel recipesMainView = new MainRecipesViewModel(EventAggregator);
            mainRecipesView.DataContext = recipesMainView;
            this.CurrentMainView = mainRecipesView;
        }

        #region ------------------------------------------------Mini helper ---------------------------------
        /// <summary>
        /// Check whether the database is connected
        /// </summary>
        /// <returns>value of connected state </returns>
        private bool IsConnected()
        {
            // check whether the database is connected
            try
            {
                CategoryService categoryService = new CategoryService();
                if (categoryService.IsConnection())
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return false;
        }

        #endregion
    }
}

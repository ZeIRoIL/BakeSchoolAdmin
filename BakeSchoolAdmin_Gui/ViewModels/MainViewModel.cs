using BakeSchoolAdmin_Commands.Commands;
using Microsoft.Practices.Prism.Events;
using BakeSchoolAdmin_Gui.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BakeSchoolAdmin_Gui.ViewModels.Recipes;
using System.Windows;
using BakeSchoolAdmin_Gui.Views;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 
        UserControl categoryWindow;

        UserControl recipesWindow;

        /// <summary>
        /// View that is currently bound to the main ContentControl
        /// </summary>
        private UserControl currentMainView;

        #endregion

        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator to communicate with other views
        /// via <see cref="Microsoft.Practices.Prism.Events"/> </param>
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
        #endregion

        #region ======================================== Properties, Indexer ================================= 

        /// <summary>
        /// Gets or sets the view that is currently bound to the main ContentControl
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
        /// Gets the MainviewCommand
        /// </summary>
        public ICommand MainViewCommand { get; private set; }

        /// <summary>
        /// Gets the CategoryView
        /// </summary>
        public ICommand CategoryViewCommand { get; private set; }

        /// <summary>
        /// Gets the RecipesView
        /// </summary>
        public ICommand RecipesViewCommand { get; private set; }
        #endregion

        #region ======================================== Commands ================================= 

        /// <summary>
        /// Determines if the loading main view command can be execute
        /// </summary>
        private bool MainViewCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Gets execute when the user clicks the main button
        /// </summary>
        private void MainViewExecute(object parameter)
        {
            MainView main = new MainView();
            this.CurrentMainView = main;
        }

        /// <summary>
        /// Determines if the loading category view command can be execute
        /// </summary>
        private bool CategoryViewCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Gets execute when the user clicks the category button
        /// </summary>
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
        /// Determines if the loading recipes view command can be execute
        /// </summary>
        private bool RecipesViewCanExecute(object parameter)
        {
          
            return true;
        }
        /// <summary>
        /// Gets execute when the user clicks the recipes button
        /// </summary>
        private void RecipesViewExecute(object parameter)
        {
                // Create a new Window with the data from MainRecipesViewModel
                MainRecipeView mainRecipesView = new MainRecipeView();
                this.recipesWindow = mainRecipesView;
                MainRecipesViewModel recipesMainView = new MainRecipesViewModel(EventAggregator);
                mainRecipesView.DataContext = recipesMainView;
                this.CurrentMainView = mainRecipesView;
        }
        #endregion

    }
}


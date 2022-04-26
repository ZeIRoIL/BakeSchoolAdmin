using BakeSchoolAdmin_Commands.Commands;
using Microsoft.Practices.Prism.Events;
using BakeSchoolAdmin_Gui.View;
using BakeSchoolAdmin_Gui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BakeSchoolAdmin_Gui.ViewModels.Recipes;
using System.Windows;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 
        CategoryWindow categoryWindow;

        RecipesWindow recipesWindow;
        #endregion

        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator to communicate with other views
        /// via <see cref="Microsoft.Practices.Prism.Events"/> </param>
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //// Hookup command to be associated
            this.CategoryViewCommand = new ActionCommand(this.CategoryViewExecute, this.CategoryViewCanExecute);

            //// Hookup command to be associated
            this.RecipesViewCommand = new ActionCommand(this.RecipesViewExecute, this.RecipesViewCanExecute);
        }
        #endregion

        #region ======================================== Properties, Indexer ================================= 
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
        /// Determines if the loading category view command can be execute
        /// </summary>
        private bool CategoryViewCanExecute(object parameter)
        {
            if (this.categoryWindow == null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets execute when the user clicks the category button
        /// </summary>
        private void CategoryViewExecute(object parameter)
        {
                // Create a new Window with the data from CategoryMainViewModel
                CategoryWindow window = new CategoryWindow();
                this.categoryWindow = window;
                CategoryMainViewModel categoryMainView = new CategoryMainViewModel(EventAggregator);
                window.DataContext = categoryMainView;
                window.Show();          
        }

        /// <summary>
        /// Determines if the loading recipes view command can be execute
        /// </summary>
        private bool RecipesViewCanExecute(object parameter)
        {
           if(this.recipesWindow == null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets execute when the user clicks the recipes button
        /// </summary>
        private void RecipesViewExecute(object parameter)
        {
                // Create a new Window with the data from MainRecipesViewModel
                RecipesWindow window = new RecipesWindow();
                this.recipesWindow = window;
                MainRecipesViewModel recipesMainView = new MainRecipesViewModel(EventAggregator);
                window.DataContext = recipesMainView;
                window.Show();
        }
        #endregion

    }
}


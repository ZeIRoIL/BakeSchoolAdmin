using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.View;
using BakeSchoolAdmin_Gui.Windows;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 

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

        }
        #endregion

        #region ======================================== Properties, Indexer ================================= 
        /// <summary>
        /// Gets the CategoryView
        /// </summary>
        public ICommand CategoryViewCommand { get; private set; }

        #endregion

        #region ======================================== Commands ================================= 
        /// <summary>
        /// Determines if the loading category view command can be execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CategoryViewCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Gets execute when the user clicks the category button
        /// </summary>
        /// <param name="parameter"></param>
        private void CategoryViewExecute(object parameter)
        {
            // Create a new Window with the data from CategoryMainViewModel
            CategoryWindow window = new CategoryWindow();
            CategoryMainViewModel categoryMainView = new CategoryMainViewModel(EventAggregator);
            window.DataContext = categoryMainView;
            window.Show();
        }
        #endregion

    }
}


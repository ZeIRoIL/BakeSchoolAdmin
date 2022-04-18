using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Models;
using BakeSchoolAdmin_Models.Modals.Category;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    class CategoryAddViewModel : ViewModelBase
    {
        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        public CategoryAddViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.CategoryAddNewCommand = new ActionCommand(this.CategroyAddCommandExecute, this.CategoryAddCommandCanExecute);
        }
        #endregion

        #region ======================================== Properties/Indexer ======================================

        /// <summary>
        /// execute  the Command which add a new category into the list
        /// </summary>
        public ICommand CategoryAddNewCommand { get; private set; }
        #endregion

        #region ======================================== Commands ================================= 

        /// <summary>
        /// Determines if the edti the category view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the Command</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c></returns>
        private bool CategoryAddCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Category button
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        private void CategroyAddCommandExecute(object parameter)
        {
            CategoryDetails details = new CategoryDetails();
            details.Name = "NewTest";
            details.Image = "das ist ein Test";
            details.Text = "Das ist ein neuer Text";
            details.Level = 2;
            Category category = new Category(300, details);

            // add the new Category into the List
            this.EventAggregator.GetEvent<AddCategoryDataEvent>().Publish(category);
        }
        #endregion
    }
}

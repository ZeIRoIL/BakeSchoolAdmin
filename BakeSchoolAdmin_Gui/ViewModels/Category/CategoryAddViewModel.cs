using BakeSchoolAdmin_Commands.Commands;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Models;
using BakeSchoolAdmin_Models.Modals.Category;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    class CategoryAddViewModel : ViewModelBase
    {
        #region ======================================== Fields, Constants, Delegates, Events ============================================ 
        /// <summary>
        /// save the id for the category
        /// </summary>
        private int id;
        /// <summary>
        /// the amount for the slider of difficult
        /// </summary>
        private int amount;
        /// <summary>
        /// the max value for the slider
        /// </summary>
        private int length = 3;
        #endregion
        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        public CategoryAddViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            // action command if the change button is clicked and the usercontrol (AddCategory)will open.
            this.CategoryAddNewCommand = new ActionCommand(this.CategroyAddCommandExecute, this.CategoryAddCommandCanExecute);

            // subscribe to event
            this.EventAggregator.GetEvent<GetLastCategorieIdDataEvent>().Subscribe(this.SetCollection, ThreadOption.UIThread);
        }
        #endregion

        #region ======================================== Properties/Indexer ======================================

        /// <summary>
        /// Gets or sets the amount of the slider
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
        /// get and set the Name of the category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// get and set the Text of the category
        /// </summary>
        public string Text { get; set; }
       
        /// <summary>
        /// execute  the Command which add a new category into the list
        /// </summary>
        public ICommand CategoryAddNewCommand { get; private set; }
        #endregion

        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void SetCollection(int  idCat)
        {
            this.id = idCat;
        }
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
            details.Name = this.Name;
            details.Text = this.Text;
            details.Level = this.Amount;
            Category category = new Category(this.id, details);

            //if (MessageBox.Show("do you want to save the new Categorie?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //{
            // add the new Category into the List

            this.EventAggregator.GetEvent<AddCategoryDataEvent>().Publish(category);
            //}
        }
        #endregion
    }
}

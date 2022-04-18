using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    class CategoryEditViewModel : ViewModelBase
    {
        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        public CategoryEditViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // subscribe to event
            this.EventAggregator.GetEvent<SelectedCategoryDataEvent>().Subscribe(this.SelectedCategory, ThreadOption.UIThread);
        }
        #endregion

        #region ======================================== Properties, Indexer =====================================================
        /// <summary>
        /// Gets or sets the Text of the selected category
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the Text of the selected category
        /// </summary>
        public string Name { get; set; }


        #endregion

        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void SelectedCategory(Category category)
        {
            this.Text = category.Details.Text;
            this.Name = category.Details.Name;

            this.OnPropertyChanged(nameof(this.Text));
            this.OnPropertyChanged(nameof(this.Name));
        }
        #endregion
    }


}

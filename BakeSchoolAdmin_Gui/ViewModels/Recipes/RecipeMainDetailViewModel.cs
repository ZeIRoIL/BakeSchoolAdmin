using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    class RecipeMainDetailViewModel : ViewModelBase
    {
        #region ======================================== Field =======================================================
        /// <summary>
        /// create name field
        /// </summary>
        private string name;
        #endregion
        public RecipeMainDetailViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // subscribe to event
            this.EventAggregator.GetEvent<SelectedRecipeDataEvent>().Subscribe(this.SelectedRecipe, ThreadOption.UIThread);
        }
        #region ======================================== Properties, Indexer =====================================================
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if(value != null)
                {
                    this.name = value;
                    OnPropertyChanged(nameof(this.Name));
                }
            }
        }
        #endregion
        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="recipe">Reference to the sent student data</param>
        public void SelectedRecipe(Recipe recipe)
        {
            this.Name = recipe.Name;
        }
        #endregion
    }
}

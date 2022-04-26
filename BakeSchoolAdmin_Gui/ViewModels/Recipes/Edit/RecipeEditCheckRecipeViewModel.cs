using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    class RecipeEditCheckRecipeViewModel : ViewModelBase
    {
        
        public RecipeEditCheckRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
        #region ======================================== Fields, Constants, Delegates, Events ============================================ 

        private Recipe recipe;
        private Description description;

        #endregion
        #region ======================================== Properties, Indexer =====================================================


        #endregion
        #region ======================================== Command ====================================================

        #endregion

        #region ======================================== Private Helper =================================

        #endregion

    }
}

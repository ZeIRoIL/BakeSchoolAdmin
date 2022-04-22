using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    class RecipeEditAddIngredientViewModel : MainViewModel
    {
        public RecipeEditAddIngredientViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}

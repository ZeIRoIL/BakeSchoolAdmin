using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    /// <summary>
    /// Displays the category data in whole window.
    /// Derives form <see cref="ViewModelBase"/>
    /// </summary>
    class CategoryViewModel : ViewModelBase
    {
        /// <summary>
        /// initialize a new instance of the categoryViewModel class
        /// </summary>
        /// <param name="eventAggregator"></param>
        public CategoryViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}

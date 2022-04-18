using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels
{ 
    /// <summary>
  /// Base class for all ViewModels
  /// Derives from <see cref="NotifyPropertyChanged"/> class
  /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {
        #region ===================================== Con-/Destructor, Dispose, Clone ======================================== 
        /// <summary>
        ///  Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator to communicate with other views
        /// via <see cref="Microsoft.Practices.Prism.Events"/> </param>
        public ViewModelBase(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
        }
        #endregion

        #region ===================================== Properties, Indexer ======================================== 
        /// <summary>
        /// Gets access to the event aggregator for communication.
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
        #endregion
    }
}

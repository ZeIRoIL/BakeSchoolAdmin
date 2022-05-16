namespace BakeSchoolAdmin_Gui.ViewModels
{
    using BakeSchoolAdmin_Commands.NotfiyPropertyChanged;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Base class for all ViewModels
    /// Derives from <see cref="NotifyPropertyChanged"/> class.
    /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public ViewModelBase(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Gets the EventAggregator
        /// Gets access to the event aggregator for communication...
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
    }
}

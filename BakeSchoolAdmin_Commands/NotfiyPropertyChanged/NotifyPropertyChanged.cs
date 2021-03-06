namespace BakeSchoolAdmin_Commands.NotfiyPropertyChanged
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the <see cref="NotifyPropertyChanged" />.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies a listener that a property value has changed.
        /// Name of the property to used to notify the listener.
        /// </summary>
        /// <param name="property"> Name of the property.</param>
        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

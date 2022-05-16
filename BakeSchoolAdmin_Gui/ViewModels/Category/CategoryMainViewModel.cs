namespace BakeSchoolAdmin_Gui.ViewModels
{
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.View;
    using Microsoft.Practices.Prism.Events;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="CategoryMainViewModel" />.
    /// </summary>
    internal class CategoryMainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public CategoryMainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            CategoryView cw = new CategoryView();
            CategoryListViewModel cwm = new CategoryListViewModel(eventAggregator);
            cw.DataContext = cwm;
            this.CurrentViewLeft = cw;



            // subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Subscribe(this.ChangetheCurrentViewRight, ThreadOption.UIThread);
        }

        /// <summary>
        /// Event handler to notice changes in the current categroy data.
        /// </summary>
        /// <param name="userControl">The userControl<see cref="UserControl"/>.</param>
        public void ChangetheCurrentViewRight(UserControl userControl)
        {
            this.currentViewRight = userControl;
            this.OnPropertyChanged(nameof(this.currentViewRight));
        }

        /// <summary>
        /// View that is currently bound to the left ContentControl..
        /// </summary>
        private UserControl currentViewLeft;

        /// <summary>
        /// View that is currently bound to the right ContentControl..
        /// </summary>
        private UserControl currentViewRight;

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl..
        /// </summary>
        public UserControl CurrentViewLeft
        {
            get
            {
                return this.currentViewLeft;
            }

            set
            {
                if (this.currentViewLeft != value)
                {
                    this.currentViewLeft = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentViewLeft));
                }
            }
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the right ContentControl..
        /// </summary>
        public UserControl CurrentViewRight
        {
            get
            {
                return this.currentViewRight;
            }

            set
            {
                if (this.currentViewRight != value)
                {
                    this.currentViewRight = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentViewRight));
                }
            }
        }

        /// <summary>
        /// Defines the changeColor.
        /// </summary>
        internal bool changeColor;

        /// <summary>
        /// Gets the BoxColor.
        /// </summary>
        public Brush BoxColor
        {
            get
            {
                if (ChangeColor)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ChangeColor.
        /// </summary>
        public bool ChangeColor
        {
            get
            {
                return this.changeColor;
            }

            set
            {
                this.changeColor = value;
                this.OnPropertyChanged(nameof(this.BoxColor));
            }
        }
    }
}

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    using System;
    using System.Collections.ObjectModel;
    using BakeSchoolAdmin_Gui.Events;
    using BakeSchoolAdmin_Gui.Views.Recipes;
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;
    using System.Windows.Controls;

    /// <summary>
    /// the view for the list and details user control.
    /// </summary>
    internal class RecipeHomeViewModel : ViewModelBase
    {
        /// <summary>
        /// the current recipe which are  selected..
        /// </summary>
        private Recipe recipe;

        /// <summary>
        /// View that is currently bound to the left ContentControl..
        /// </summary>
        private UserControl currentViewLeft;

        /// <summary>
        /// View that is currently bound to the right ContentControl..
        /// </summary>
        private UserControl currentViewRight;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeHomeViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipes">The recipes<see cref="ObservableCollection{Recipe}"/>.</param>
        public RecipeHomeViewModel(IEventAggregator eventAggregator, ObservableCollection<Recipe> recipes) : base(eventAggregator)
        {
            RecipesMainView view = new RecipesMainView();
            RecipeMainListViewModel model = new RecipeMainListViewModel(EventAggregator, recipes);
            view.DataContext = model;
            this.currentViewLeft = view;

            // subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentMainDataEvent>().Subscribe(this.ChangeCurrentMainView, ThreadOption.UIThread);

            // subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Subscribe(this.ChangetheCurrentViewRight, ThreadOption.UIThread);
        }

        /// <summary>
        /// Event handler to notice changes in the current category data.
        /// </summary>
        /// <param name="userControl">The userControl<see cref="UserControl"/>.</param>
        public void ChangetheCurrentViewRight(UserControl userControl)
        {
            this.currentViewRight = userControl;
            this.OnPropertyChanged(nameof(this.currentViewRight));
        }

        /// <summary>
        /// The ChangeCurrentMainView.
        /// </summary>
        /// <param name="main">The main<see cref="UserControl"/>.</param>
        public void ChangeCurrentMainView(UserControl main) => throw new Exception("change current MainView");
        
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
    }
}

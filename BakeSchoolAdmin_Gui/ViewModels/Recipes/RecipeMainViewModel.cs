using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Gui.Views.Recipes;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{
    class RecipeMainViewModel : ViewModelBase
    {
        public RecipeMainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            RecipesMainView view = new RecipesMainView();
            RecipeMainListViewModel model = new RecipeMainListViewModel(EventAggregator);
            view.DataContext = model;
            this.currentViewLeft = view;

            // subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentRightDataEvent>().Subscribe(this.ChangetheCurrentViewRight, ThreadOption.UIThread);
        }

        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void ChangetheCurrentViewRight(UserControl userControl)
        {
            this.currentViewRight = userControl;
            this.OnPropertyChanged(nameof(this.currentViewRight));
        }

        #endregion

        #region ======================================== Fields, Constants, Delegates, Events =============================
        /// <summary>
        /// View that is currently bound to the left ContentControl
        /// </summary>
        private UserControl currentViewLeft;

        /// <summary>
        /// View that is currently bound to the right ContentControl
        /// </summary>
        private UserControl currentViewRight;


        #endregion
        #region ======================================== Properties, Indexer ============================================
        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl
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
        /// Gets or sets the view that is currently bound to the right ContentControl
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

        #endregion
    }
}

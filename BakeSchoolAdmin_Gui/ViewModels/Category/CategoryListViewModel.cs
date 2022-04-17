using BakeSchoolAdmin_Models;
using BakeSchoolAdmin_Models.Modals.Category;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.ViewModels
{
    class CategoryListViewModel : ViewModelBase
    {
        #region ======================================== Fields ================================= 

        #endregion
        #region ======================================== Con-/Destructor, Dispose, Clone ================================= 
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator"> Event aggregator to communicate with other views
        /// via <see cref="Microsoft.Practices.Prism.Events"/> </param>
        public CategoryListViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // Gets the category to the list
            LoadCategories();
            Firstname = "Alex";
        }
        #endregion

        #region ======================================== Properties/Indexer ================================= 

        /// <summary>
        /// Gets or sets the list with all categories
        /// </summary>
        public ObservableCollection<Category> Categories { get; set; }
        public string Firstname { get; set; }

        #endregion

        #region ======================================== Private Helper ================================= 
        private void LoadCategories()
        {
            //// init collection and add data
            this.Categories = new ObservableCollection<Category>();

            CategoryDetails details = new CategoryDetails();
            details.Name = "TestCate";
            details.Image = "das ist ein Test";
            details.Text = "Das ist ein Text test";
            details.Level = 2;
            Category category = new Category(100, details);

            CategoryDetails details1 = new CategoryDetails();
            details1.Name = "TestCate";
            details1.Image = "das ist ein Test";
            details1.Text = "Das ist ein Text Test 2";
            details1.Level = 2;

            Category category1 = new Category(100, details1);



            this.Categories.Add(category);
            this.Categories.Add(category1);
        }
        #endregion
    }
}

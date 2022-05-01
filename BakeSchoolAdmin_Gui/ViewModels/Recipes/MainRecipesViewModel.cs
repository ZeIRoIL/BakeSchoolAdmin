using BakeSchoolAdmin_DatabaseConnection.Services;
using BakeSchoolAdmin_Gui.Events;
using BakeSchoolAdmin_Gui.Views;
using BakeSchoolAdmin_Gui.Views.Recipes;
using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes
{/// <summary>
/// the main window for the recipes.
/// </summary>
    class MainRecipesViewModel : ViewModelBase
    {
        #region ======================================== Fields, Constants, Delegates, Events =============================
        /// <summary>
        /// View that is currently bound to the main ContentControl
        /// </summary>
        private UserControl currentMainRecipeView;
        /// <summary>
        /// Observable Collection for the recipes
        /// </summary>
        private ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();
        #endregion
        #region ======================================== Properties, Indexer ============================================

        /// <summary>
        /// the current recipe which are  selected 
        /// </summary>
        private Recipe recipe;

        /// <summary>
        /// Gets or sets the view that is currently bound to the main ContentControl
        /// </summary>
        public UserControl CurrentMainRecipeView
        {
            get
            {
                return this.currentMainRecipeView;
            }

            set
            {
                if (this.currentMainRecipeView != value)
                {
                    this.currentMainRecipeView = value;
                    //// takes the property as a string -> OnPropertyChanged(nameof())
                    this.OnPropertyChanged(nameof(this.CurrentMainRecipeView));
                }
            }
        }


          #endregion
        public MainRecipesViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            InitRecipe();

            /// subscribe to event
            this.EventAggregator.GetEvent<ChangeCurrentMainDataEvent>().Subscribe(this.ChangeCurrentMainView, ThreadOption.UIThread);

            // open the view with all recipes and home View from recipes!
            RecipeHomeViewModel recipeMainViewModel = new RecipeHomeViewModel(EventAggregator, recipes);
            RecipeHomeView recipeHomeView = new RecipeHomeView();

            recipeHomeView.DataContext = recipeMainViewModel;
            this.CurrentMainRecipeView = recipeHomeView;
            
            /// subscribe to event
            this.EventAggregator.GetEvent<ChangeRecipeCurrentMainViewEvent>().Subscribe(this.ChangeRecipeCurrentMainView, ThreadOption.UIThread);

        }
        #region ======================================== Events =======================================================
        /// <summary>
        /// Event handler to notice changes in the current categroy data
        /// </summary>
        /// <param name="category">Reference to the sent student data</param>
        public void ChangeRecipeCurrentMainView(UserControl user)
        {
            this.CurrentMainRecipeView = user;
            this.OnPropertyChanged(nameof(CurrentMainRecipeView));
        }

        public void ChangeCurrentMainView(UserControl main)
        {

        }

        #endregion
       
        #region ======================================== Private Helper ================================= 
        /// <summary>
        /// load the current description data into the descritption field
        /// </summary>
        void InitRecipe()
        {
            //Recipe recipe = new Recipe();

            //Description description = new Description();
            //description.Step = 1;
            //description.Text = "Das ist der erste Versuch";

            //Description description1 = new Description();
            //description1.Step = 2;
            //description1.Text = "Das ist der zweite Versuch";

            //Description description2 = new Description();
            //description2.Step = 3;
            //description2.Text = "Das ist der dritte Versuch";

            //List<Description> descriptions = new List<Description>();
            //descriptions.Add(description1);
            //descriptions.Add(description2);


            //List<Ingredient> ingredients = new List<Ingredient>();
            //Ingredient ingredient = new Ingredient();
            //ingredient.Data = "Käse";
            //ingredient.Amount = 100;
            //ingredient.Unit = "stk.";
            //Ingredient ingredient1 = new Ingredient();
            //ingredient1.Data = "Erdbeere";
            //ingredient1.Amount = 100;
            //ingredient1.Unit = "stk.";

            //ingredients.Add(ingredient);
            //ingredients.Add(ingredient1);

            //recipe.Name = "Rezept1";
            //recipe.Number = 1;
            //recipe.Ingredients = ingredients;
            //recipe.Descriptions = descriptions;

            //Recipe recipe1 = new Recipe();

            //Description descriptio1 = new Description();
            //description.Step = 1;
            //description.Text = "Das ist der erste ";

            //Description descriptio2 = new Description();
            //description1.Step = 2;
            //description1.Text = "Das ist der zweite ";

            //Description descriptio3 = new Description();
            //description2.Step = 3;
            //description2.Text = "Das ist der dritte ";

            //List<Description> descriptions1 = new List<Description>();
            //descriptions1.Add(descriptio1);
            //descriptions1.Add(descriptio2);
            //descriptions1.Add(descriptio3);


            //List<Ingredient> ingredients1 = new List<Ingredient>();
            //Ingredient ingredient11 = new Ingredient();
            //ingredient.Data = "Käse";
            //Ingredient ingredient12 = new Ingredient();
            //ingredient1.Data = "Erdbeere";

            //ingredients.Add(ingredient11);
            //ingredients.Add(ingredient12);

            //recipe1.Name = "Rezept2";
            //recipe1.Number = 2;
            //recipe1.Ingredients = ingredients1;
            //recipe1.Descriptions = descriptions1;

            //this.recipes.Add(recipe);
            //this.recipes.Add(recipe1);

#warning need the implementation
            RecipeService recipeService = new RecipeService();
            if (recipeService.init())
            {
                IList<Recipe> recipedata = new List<Recipe>();

                recipedata = recipeService.ReadData();
                this.recipes = recipeService.GetCategoryObserv(recipedata);
            }
        }
        #endregion
      

    }
}

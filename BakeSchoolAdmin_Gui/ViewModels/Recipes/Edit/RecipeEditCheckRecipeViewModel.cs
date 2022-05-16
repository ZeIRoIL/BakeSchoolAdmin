namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using BakeSchoolAdmin_Models;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeEditCheckRecipeViewModel" />.
    /// </summary>
    internal class RecipeEditCheckRecipeViewModel : ViewModelBase
    {
        /// <summary>
        /// Defines the recipe.
        /// </summary>
        private Recipe recipe;

        /// <summary>
        /// Defines the description.
        /// </summary>
        private Description description;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditCheckRecipeViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        public RecipeEditCheckRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }
    }
}

namespace BakeSchoolAdmin_Gui.ViewModels.Recipes.Edit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using BakeSchoolAdmin_Commands.Commands;
    using BakeSchoolAdmin_Gui.Events.RecipeAddEvents;
    using BakeSchoolAdmin_Models;
    using BakeSchoolAdmin_Models.Modals.Recipe;
    using Microsoft.Practices.Prism.Events;

    /// <summary>
    /// Defines the <see cref="RecipeEditAddDescriptionViewModel" />.
    /// </summary>
    internal class RecipeEditAddDescriptionViewModel : ViewModelBase
    {
        #region ---------------------------------------- private fields ---------------------------
        /// <summary>
        /// the text for one description..
        /// </summary>
        private string text;

        /// <summary>
        /// if the button "save image" should obvious.
        /// </summary>
        private string visibiltySaveImage;

        /// <summary>
        /// the text for one description..
        /// </summary>
        private string texthint;

        /// <summary>
        /// the image of description.
        /// </summary>
        private Image image;

        /// <summary>
        /// the path to image 
        /// </summary>
        private string imagepath;

        /// <summary>
        /// the step for one description..
        /// </summary>
        private int step;

        /// <summary>
        /// the step for one hint..
        /// </summary>
        private int stephint;

        /// <summary>
        /// the step counter for the recipe..
        /// </summary>
        private int stepCount;
        #endregion

        #region ---------------------------------------------- constructor --------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipeEditAddDescriptionViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="recipe">The recipeDes<see cref="Recipe"/>.</param>
        public RecipeEditAddDescriptionViewModel(IEventAggregator eventAggregator, Recipe recipe) : base(eventAggregator)
        {
            this.VisibiltySaveImage = "Hidden";
            if (recipe != null)
            {
                // If the Description is always created
                this.Recipe = recipe;
                if (recipe.Descriptions != null)
                {
                    this.Descriptions = new ObservableCollection<Description>(recipe.Descriptions);
                }
                else
                {
                    this.Descriptions = new ObservableCollection<Description>();
                }

                this.OnPropertyChanged(nameof(this.Descriptions));
            }
            else
            {
                // If the Description will init!
                this.Descriptions = new ObservableCollection<Description>();
            }

            #region -------------------------------------------------------------- Description Commands --------------------------------------------------
            this.ShowDescription = new ActionCommand(this.ShowDescriptionCommandExecute, this.ShowDescriptionCommandCanExecute);
            this.AddDescription = new ActionCommand(this.AddDescriptionCommandExecute, this.AddDescriptionCommandCanExecute);
            this.SaveDescription = new ActionCommand(this.SaveDescriptionCommandExecute, this.SaveDescriptionCommandCanExecute);
            this.DeleteDescription = new ActionCommand(this.DeleteDescriptionCommandExecute, this.DeleteDescriptionCommandCanExecute);
            this.AddImageDescription = new ActionCommand(this.AddImageDescriptionCommandExecute, this.AddImageDescriptionCommandCanExecute);
            #endregion
        }
        #endregion

        #region ------------------------------------------------------------ properties, Indexer -----------------------------------------------
        /// <summary>
        /// Gets the Descriptions
        /// save the description of the recipes..
        /// </summary>
        public ObservableCollection<Description> Descriptions { get; private set; }

        /// <summary>
        /// Gets the Descriptions
        /// save the description of the recipes..
        /// </summary>
        public List<Description> DescriptionsList { get; private set; }

        /// <summary>
        /// Gets the Recipe
        /// The recipe which will transfer into the end view if the user clicked on the button "Save"...
        /// </summary>
        public Recipe Recipe { get; private set; }

        /// <summary>
        /// Gets the ShowDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand ShowDescription { get; private set; }

        /// <summary>
        /// Gets the AddDescription
        /// execute the command and add the recent ingredient in the ObservableCollection..
        /// </summary>
        public ICommand AddDescription { get; private set; }

        /// <summary>
        /// Gets the SaveDescription
        /// execute the command and save the recent data into description.
        /// </summary>
        public ICommand SaveDescription { get; private set; }

        /// <summary>
        /// Gets the delete description
        /// execute the command and delete the recent data from the description.
        /// </summary>
        public ICommand DeleteDescription { get; private set; }

        /// <summary>
        /// Gets the delete description
        /// execute the command and open the file dialog to save a picture.
        /// </summary>
        public ICommand AddImageDescription { get; private set; }

        /// <summary>
        /// Gets the show hint
        /// execute the command and add the recent description in the ObservableCollection.
        /// </summary>
        public ICommand ShowHint { get; private set; }

        /// <summary>
        /// Gets the AddDescription
        /// execute the command and add the recent description in the ObservableCollection..
        /// </summary>
        public ICommand AddHint { get; private set; }

        /// <summary>
        /// Gets the delete description
        /// execute the command and delete the recent data from the description.
        /// </summary>
        public ICommand DeleteHint { get; private set; }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.text = value;
                    this.OnPropertyChanged(nameof(this.text));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Texthint
        {
            get
            {
                return this.texthint;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.texthint = value;
                    this.OnPropertyChanged(nameof(this.texthint));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        public int Step
        {
            get
            {
                return this.step;
            }

            set
            {
                if (-1 != value)
                {
                    this.step = value;
                    this.OnPropertyChanged(nameof(this.step));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Step of hint.
        /// </summary>
        public int Stephint
        {
            get
            {
                return this.stephint;
            }

            set
            {
                if (-1 != value)
                {
                    this.stephint = value;
                    this.OnPropertyChanged(nameof(this.stephint));
                }
            }
        }

        /// <summary>
        /// Gets or sets the StepCount.
        /// </summary>
        public int StepCount
        {
            get
            {
                return this.stepCount;
            }

            set
            {
                if (-1 != value)
                {
                    this.stepCount = value;
                    this.OnPropertyChanged(nameof(this.stepCount));
                }
            }
        }

        /// <summary>
        /// Gets or sets the saveImage condition
        /// </summary>
        public string VisibiltySaveImage
        {
            get
            {
                return this.visibiltySaveImage;
            }

            set
            {
                    this.visibiltySaveImage = value;
                    this.OnPropertyChanged(nameof(this.visibiltySaveImage));
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public Image Image
        {
            get
            {
                return this.image;
            }

            set
            {
                if (null != value)
                {
                    this.image = value;
                    this.OnPropertyChanged(nameof(this.image));
                }
            }
        }

        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return this.imagepath;
            }

            set
            {
                if (string.Empty != value)
                {
                    this.imagepath = value;
                    this.OnPropertyChanged(nameof(this.imagepath));
                }
            }
        }

        #endregion

        #region ---------------------------------------------------------- Commands ------------------------------------------------------------------

        #region ---------------------------------------------------------- Description ------------------------------------------------------------------

        #region -------------------------------------Show Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the ingredient is created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool ShowDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and show user the text of the step.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void ShowDescriptionCommandExecute(object parameter)
        {
            if ((int)parameter != 0)
            {
                int stepId = (int)parameter;
                if (this.Descriptions.Any(d => d.Step == stepId))
                {
                    this.Step = stepId;
                    this.Text = this.Descriptions[this.Step - 1].Text;
                    this.ImagePath = this.Descriptions[this.Step - 1].Image;

                    if (this.image != null)
                    {
                        if (this.Descriptions[this.Step - 1].Image != null)
                        {
                            Image.Source = new BitmapImage(new Uri(this.Descriptions[this.Step - 1].Image, UriKind.RelativeOrAbsolute));
                        }
                    }
                    
                    this.OnPropertyChanged(nameof(this.Text));
                    this.OnPropertyChanged(nameof(this.Image));
                }
            }
        }
        #endregion

        #region -------------------------------------Add Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description step can be created.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool AddDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed and add a new Step for the description.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddDescriptionCommandExecute(object parameter)
        {
            if (this.image != null)
            {
                // Delete the image source
                this.Image.Source = null;
            }
            
            // If the description box always contains a text.
            if (this.Text != null)
            {
                int saveStep = this.Step;
                this.Descriptions[saveStep - 1].Text = this.Text;
            }
            
            Description descriptions = new Description();
            int count = this.Descriptions.Count() + 1;
            if (count != 0)
            {
                descriptions.Step = count;
                this.Step = count;
                this.Descriptions.Add(descriptions);
                this.VisibiltySaveImage = "Visible";
                this.OnPropertyChanged(nameof(this.Descriptions));
                this.OnPropertyChanged(nameof(this.VisibiltySaveImage));
            }
        }
        #endregion

        #region -------------------------------------Save Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool SaveDescriptionCommandCanExecute(object parameter)
        {
            if (this.Descriptions == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void SaveDescriptionCommandExecute(object parameter)
        {
            // Save Description Step befor the whole recipe will create
            this.Descriptions[this.Step - 1].Text = this.Text;
            this.Descriptions[this.Step - 1].Image = this.ImagePath;
            this.OnPropertyChanged(nameof(this.Descriptions));

            this.DescriptionsList = new List<Description>();
            this.Recipe = new Recipe();

            foreach (var item in this.Descriptions)
            {
                this.DescriptionsList.Add(item);
            }

            this.Recipe.Descriptions = this.DescriptionsList;
            this.EventAggregator.GetEvent<ReloadCurrentCheckRecipeEventData>().Publish(this.Recipe);
        }
        #endregion

        #region -------------------------------------Delete Description ------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool DeleteDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void DeleteDescriptionCommandExecute(object parameter)
        {
            if (Descriptions.Count != 0)
            {
                Descriptions.Remove(Descriptions.Last());
                this.Step--;
            }

            this.OnPropertyChanged(nameof(this.Descriptions));
            this.OnPropertyChanged(nameof(this.Step));
        }
        #endregion

        #region --------------------------------------------------------------- Add Image Description -----------------------------------------
        /// <summary>
        /// Determines if the data is correct then the description can be saved.
        /// </summary>
        /// <param name="parameter">Data used by the Command.</param>
        /// <returns><c>true</c> if the command can be executed otherwise <c>false</c>.</returns>
        private bool AddImageDescriptionCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Gets executed when the user clicks the Save button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddImageDescriptionCommandExecute(object parameter)
        {
            if (this.ImagePath != string.Empty)
            {
                string targetPathFolder = @".\img\" + this.Recipe.Name;
                string targetPathFile = @".\img\" + this.Recipe.Name + @"\" + this.step + ".jpg";
                string sourcePath;
                try
                {
                    if (!Directory.Exists(targetPathFolder))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(targetPathFolder);
                        Console.WriteLine("The directory was created successfully at {0}.{1}", Directory.GetCreationTime(targetPathFolder), targetPathFolder);
                    }

                    OpenFileDialog openFileDlg = new OpenFileDialog();

                    openFileDlg.Filter = "Image Files | *.jpg; ";
                    openFileDlg.ShowDialog();

                    if (!(openFileDlg.FileName is null || openFileDlg.FileName == string.Empty))
                    {
                        sourcePath = openFileDlg.FileName;
                        System.IO.File.Copy(sourcePath, targetPathFile, true);
                        FileInfo f = new FileInfo(targetPathFile);
                        this.Descriptions[this.step - 1].Image = f.FullName;
                        this.Image = new Image();
                        this.ImagePath = f.FullName;
                        Image.Source = new BitmapImage(new Uri(f.FullName, UriKind.RelativeOrAbsolute));
                        this.OnPropertyChanged(nameof(this.ImagePath));
                        this.OnPropertyChanged(nameof(this.Image));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                this.Image = new Image();
                Image.Source = new BitmapImage(new Uri(this.ImagePath, UriKind.RelativeOrAbsolute));
            }
            #endregion
        }

            #endregion

            #endregion

            #region --------------------------------------------------------------------- mini helper -------------------------------------------
            
            #endregion
        }
}
